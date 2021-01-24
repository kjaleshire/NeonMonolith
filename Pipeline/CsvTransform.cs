using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using CsvHelper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeonMonolith.Models;
using RetsExchange;

namespace NeonMonolith.Pipeline
{
    internal class CsvTransform<RetsType> : IPropagatorBlock<ListingMessage<RetsType>, CsvMessage>
    where RetsType : IAborMatrixListing
    {
        private readonly ILogger<SyncService> _logger;
        private readonly IOptions<AppConfig> _appConfig;
        private readonly ITargetBlock<ListingMessage<RetsType>> _target;
        private readonly IReceivableSourceBlock<CsvMessage> _source;

        public Task Completion => _target.Completion;

        internal CsvTransform(ILogger<SyncService> logger, IOptions<AppConfig> appConfig)
        {
            _logger = logger;
            _appConfig = appConfig;

            _source = new BroadcastBlock<CsvMessage>(null);
            _target = new ActionBlock<ListingMessage<RetsType>>(HandleMessage);
        }

        private async Task HandleMessage(ListingMessage<RetsType> listingMessage)
        {
            var csvMessage = new CsvMessage
            {
                SearchId = listingMessage.SearchId,
                MinRecordId = listingMessage.Records.Min(r => r.Matrix_Unique_ID),
                MaxRecordId = listingMessage.Records.Max(r => r.Matrix_Unique_ID),
            };

            _logger.LogInformation($"CSV: writing search ID {listingMessage.SearchId} with record IDs {csvMessage.MinRecordId}-{csvMessage.MaxRecordId}");

            using var csvWriter = new CsvWriter(csvMessage.CsvStringWriter);

            csvWriter.WriteRecords(listingMessage.Records);

            var source = (BroadcastBlock<CsvMessage>)_source;
            await source.SendAsync(csvMessage);
        }

        public CsvMessage ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<CsvMessage> target, out bool messageConsumed) =>
            _source.ConsumeMessage(messageHeader, target, out messageConsumed);

        public IDisposable LinkTo(ITargetBlock<CsvMessage> target, DataflowLinkOptions linkOptions) =>
            _source.LinkTo(target, linkOptions);

        public IDisposable LinkTo(ITargetBlock<CsvMessage> target) =>
            _source.LinkTo(target);

        public void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<CsvMessage> target) =>
            _source.ReleaseReservation(messageHeader, target);

        public bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<CsvMessage> target) =>
            _source.ReserveMessage(messageHeader, target);
        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, ListingMessage<RetsType> messageValue, ISourceBlock<ListingMessage<RetsType>> source, bool consumeToAccept) =>
            _target.OfferMessage(messageHeader, messageValue, source, consumeToAccept);

        public void Complete()
        {
            _logger.LogInformation("CSV: got Complete");
            _target.Complete();
            _source.Complete();
        }

        public void Fault(Exception exception) => _target.Fault(exception);
    }
}
