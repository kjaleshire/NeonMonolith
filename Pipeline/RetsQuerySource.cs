using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeonMonolith.Contexts;
using NeonMonolith.Models;
using RetsExchange;

namespace NeonMonolith.Pipeline
{
    internal class RetsQuerySource<RetsType> : ISourceBlock<ListingMessage<RetsType>>
    where RetsType : IAborMatrixListing, new()
    {
        private readonly ILogger<SyncService> _logger;
        private readonly IOptions<AppConfig> _appConfig;
        private readonly RetsClient _retsClient;
        private readonly long _searchId;

        private readonly IPropagatorBlock<ListingMessage<RetsType>, ListingMessage<RetsType>> _source;

        public Task Completion => _source.Completion;

        public RetsQuerySource(ILogger<SyncService> logger, IOptions<AppConfig> appConfig, DbContextOptions<ApplicationDbContext> dbContextOptions, RetsClient retsClient, string destinationPath, long searchId)
        {
            _logger = logger;
            _retsClient = retsClient;
            _searchId = searchId;
            _appConfig = appConfig;

            _source = new BroadcastBlock<ListingMessage<RetsType>>(null);

            var databaseOutput = new DatabaseOutput<RetsType>(_logger, _appConfig, dbContextOptions);
            this.LinkTo(databaseOutput, new DataflowLinkOptions { PropagateCompletion = true });

            var csvTransform = new CsvTransform<RetsType>(_logger, _appConfig);
            this.LinkTo(csvTransform, new DataflowLinkOptions { PropagateCompletion = true });

            var diskOutput = new DiskOutput(_logger, _appConfig, destinationPath);
            csvTransform.LinkTo(diskOutput, new DataflowLinkOptions { PropagateCompletion = true });

            var dropboxOutput = new DropboxOutput(_logger, _appConfig, destinationPath);
            csvTransform.LinkTo(dropboxOutput, new DataflowLinkOptions { PropagateCompletion = true });
        }

        internal async Task<int> StreamSearchAsync(DateTime timeTo, DateTime? timeFrom, int? offset, IEnumerable<string>? status)
        {
            var retsSearchRequest = createRequest(timeTo, timeFrom, offset, status);
            var retsResultSet = await _retsClient.SearchAsync(retsSearchRequest, retries: 2);
            if (retsResultSet.TotalCount is int totalCount)
                _logger.LogInformation($"RETS: Total records for query: {totalCount}");

            await streamRecords(retsResultSet);

            return retsResultSet.Count;
        }

        private async Task streamRecords(RetsSearchResponse<RetsType> retsResultSet) =>
            await _source.SendAsync(new ListingMessage<RetsType>(retsResultSet.RecordStream)
            {
                SearchId = _searchId,
            });

        private RetsSearchRequest<RetsType> createRequest(DateTime timeTo, DateTime? timeFrom = null, int? offset = null, IEnumerable<string>? status = null)
        {
            var query = new List<string> {
                $"(MatrixModifiedDT={timeTo.ToString("s")}-)",
            };
            if (timeFrom is DateTime timeFromValue)
                query.Add($"(MatrixModifiedDT={timeFromValue.ToString("s")}+)");

            if (status != null && status.Any())
                query.Add($"(Status={String.Join(",", status)})");

            return new RetsSearchRequest<RetsType>()
            {
                Query = String.Join(",", query),
                Offset = offset,
            };
        }

        public ListingMessage<RetsType> ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<ListingMessage<RetsType>> target, out bool messageConsumed) =>
            _source.ConsumeMessage(messageHeader, target, out messageConsumed);

        public IDisposable LinkTo(ITargetBlock<ListingMessage<RetsType>> target, DataflowLinkOptions linkOptions) =>
            _source.LinkTo(target, linkOptions);

        public void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<ListingMessage<RetsType>> target) =>
            _source.ReleaseReservation(messageHeader, target);

        public bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<ListingMessage<RetsType>> target) =>
            _source.ReserveMessage(messageHeader, target);

        public void Complete() => _source.Complete();

        public void Fault(Exception exception) => _source.Fault(exception);
    }
}
