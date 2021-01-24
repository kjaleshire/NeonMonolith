using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeonMonolith.Contexts;
using NeonMonolith.Models;
using NeonMonolith.Pipeline;

namespace NeonMonolith
{
    internal class SyncService : IHostedService
    {
        private readonly ILogger<SyncService> _logger;
        private readonly IOptions<AppConfig> _appConfig;
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;
        private readonly System.Timers.Timer _timer;

        public SyncService(ILogger<SyncService> logger, IOptions<AppConfig> appConfig, DbContextOptions<ApplicationDbContext> dbContextOptions)
        {
            _logger = logger;
            _appConfig = appConfig;
            _dbContextOptions = dbContextOptions;
            _timer = new System.Timers.Timer();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var period = _appConfig.Value.RUN_PERIOD_HOURS;
            _logger.LogInformation($"Running as service with period {period} hour{(period > 1 ? "s" : "")}");

            _timer.Interval = period * 60 * 60 * 1000;

            _timer.Elapsed += async (s, e) => await Run(new TimeSpan(_appConfig.Value.PATCH_RANGE_HOURS, 0, 0));
            _timer.AutoReset = true;
            // Initial run for service start
            await Run(new TimeSpan(_appConfig.Value.PATCH_RANGE_HOURS, 0, 0));
            _timer.Enabled = true;
        }

        private async Task Run(TimeSpan patchRange)
        {
            var runStarted = DateTime.Now;
            _logger.LogInformation($"Started query process at {runStarted}");

            var nowInTimeZone = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, GetLocalTimeZone());

            // "Older" listings are those older than 2 years
            var twoYearsAgo = nowInTimeZone.AddYears(-2);

            using var retsScraper = new RetsScraper(_logger, _appConfig, _dbContextOptions);

            if (_appConfig.Value.DO_OLDER_LISTINGS_BACKFILL)
            {
                // "Older" listings backfill:
                // Older than 2 years of "Expired" , "Withdrawn" and "Sold"
                var status = new string[] { "X", "W", "S" };
                await retsScraper.RunListingStream<ResidentialProperty>(_appConfig.Value.OLDER_LISTING_PATH, twoYearsAgo, null, status);
            }
            if (_appConfig.Value.DO_CURRENT_LISTINGS_BACKFILL)
            {
                // "Current" listings backfill:
                // "Active", "Pending", "Pending Taking Back-ups", and
                // last two years of "Expired" , "Withdrawn" and "Sold"
                var allStatus = new string[] { "A", "P", "PO" };
                await retsScraper.RunListingStream<ResidentialProperty>(_appConfig.Value.CURRENT_LISTING_PATH, nowInTimeZone, null, allStatus);
                var twoYearsOfStatus = new string[] { "X", "W", "S" };
                await retsScraper.RunListingStream<ResidentialProperty>(_appConfig.Value.CURRENT_LISTING_PATH, nowInTimeZone, twoYearsAgo, twoYearsOfStatus);
            }
            if (_appConfig.Value.DO_OLDER_LISTINGS_PATCH)
            {
                // Every hour updating items that fell into older than two years
                // older than 2 years of "Expired" , "Withdrawn" and "Sold"
                var status = new string[] { "X", "W", "S" };
                var timeFrom = twoYearsAgo.Subtract(patchRange);
                await retsScraper.RunListingStream<ResidentialProperty>(_appConfig.Value.OLDER_LISTING_PATH, twoYearsAgo, timeFrom, status);
            }
            if (_appConfig.Value.DO_CURRENT_LISTINGS_PATCH)
            {
                // Every hour updating with the last 2 hours' listings
                // "Active", "Pending", "Pending Taking Back-ups",
                // and last two years of "Expired" , "Withdrawn" and "Sold"
                var status = new string[] { "A", "P", "PO", "X", "W", "S" };
                var timeFrom = nowInTimeZone.Subtract(patchRange);
                await retsScraper.RunListingStream<ResidentialProperty>(_appConfig.Value.CURRENT_LISTING_PATH, nowInTimeZone, timeFrom, status);
            }
            if (_appConfig.Value.DO_ENTIRE_BACKFILL)
            {
                // If for whatever reason we want to do a full backfill
                await retsScraper.RunListingStream<ResidentialProperty>(_appConfig.Value.CURRENT_LISTING_PATH, nowInTimeZone);
            }

            _logger.LogInformation($"Service finished query process at {DateTime.Now}");
            _logger.LogInformation($"Service invocation took {DateTime.Now - runStarted}");
        }

        private TimeZoneInfo GetLocalTimeZone()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return TimeZoneInfo.FindSystemTimeZoneById("America/Chicago");
            }
            else
            {
                throw new Exception("Unsupported OS");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
