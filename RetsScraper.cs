using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeonMonolith.Contexts;
using NeonMonolith.Models;
using NeonMonolith.Pipeline;
using RetsExchange;

namespace NeonMonolith
{
    internal class RetsScraper : IDisposable
    {
        private const string _dateTimeFormat = "yyyyMMddHHmmssfff";
        private readonly ILogger<SyncService> _logger;
        private readonly IOptions<AppConfig> _appConfig;
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;
        private readonly RetsClient _retsClient;

        public RetsScraper(ILogger<SyncService> logger, IOptions<AppConfig> appConfig, DbContextOptions<ApplicationDbContext> dbContextOptions)
        {
            _logger = logger;
            _appConfig = appConfig;
            _dbContextOptions = dbContextOptions;
            _retsClient = new RetsClient(_appConfig.Value.RETS_ENDPOINT,
                                         _appConfig.Value.RETS_USERNAME,
                                         _appConfig.Value.RETS_PASSWORD);
            checkLogin().Wait();
        }

        internal async Task RunListingStream<RetsType>(string destinationPath, DateTime timeTo, DateTime? timeFrom = null, IEnumerable<string>? status = null)
            where RetsType : IAborMatrixListing, new()
        {
            checkDateTimeArgs(timeTo, timeFrom);
            await checkLogin();

            var searchId = Int64.Parse(DateTime.UtcNow.ToString(_dateTimeFormat));

            _logger.LogInformation($"RETS: Starting listing query {searchId} {destinationPath}");

            var recordCount = 0;
            var offset = 0;
            do
            {
                var querySource = new RetsQuerySource<RetsType>(_logger, _appConfig, _dbContextOptions, _retsClient, destinationPath, searchId);

                var backToString = "";
                if (timeFrom is DateTime timeFromValue)
                    backToString = $" back to {timeFromValue}";

                _logger.LogInformation($"RETS: Search {searchId} {destinationPath} from {timeTo}{backToString}");
                recordCount = await querySource.StreamSearchAsync(timeTo, timeFrom, offset, status);

                _logger.LogInformation($"RETS: Search {searchId} {destinationPath} received batch count {recordCount}");

                offset += AppConfig.MaxRecords;
            } while (recordCount == AppConfig.MaxRecords);

            _logger.LogInformation($"RETS: Finished listing query {searchId} {destinationPath}");
        }

        private static void checkDateTimeArgs(DateTime timeTo, DateTime? timeFrom)
        {
            if (timeFrom is DateTime timeFromValue && timeFromValue > timeTo)
                throw new ArgumentException("End time must come after begin time");
        }

        private async Task checkLogin()
        {
            try
            {
                if (!_retsClient.IsLoggedIn)
                {
                    await _retsClient.LoginAsync();
                    _logger.LogInformation("RETS: Logged in");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"RETS: Error logging into ABOR: {ex}");
                throw;
            }
        }

        public void Dispose()
        {
            _retsClient.LogoutAsync().Wait();
            _logger.LogInformation("RETS: Logged out");
        }
    }
}
