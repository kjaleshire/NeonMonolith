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

namespace NeonMonolith.Pipeline
{
    internal class DatabaseOutput<RetsType> : PipelineOutput<ListingMessage<RetsType>>
    {
        private readonly ILogger<SyncService> _logger;
        private readonly IOptions<AppConfig> _appConfig;
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        internal DatabaseOutput(ILogger<SyncService> logger, IOptions<AppConfig> appConfig, DbContextOptions<ApplicationDbContext> dbContextOptions)
        {
            _logger = logger;
            _appConfig = appConfig;
            _dbContextOptions = dbContextOptions;

            _block = new ActionBlock<ListingMessage<RetsType>>(HandleMessage);
        }

        private async Task HandleMessage(ListingMessage<RetsType> listingMessage)
        {
            if (!(listingMessage.Records is IEnumerable<ResidentialProperty> recordList))
                return;

            _logger.LogInformation("DB: Started saving records");
            using var dbContext = new ApplicationDbContext(_dbContextOptions);

            foreach (var record in recordList)
            {
                var existingRecord = await dbContext.ResidentialProperties.FindAsync(record.Matrix_Unique_ID);

                if (existingRecord == null)
                {
                    await dbContext.ResidentialProperties.AddAsync(record);
                }
                else
                {
                    dbContext.Entry(record).CurrentValues.SetValues(record);
                }
            }

            await saveWithConcurrencyHandling(dbContext);
            _logger.LogInformation("DB: Finished saving records");
        }

        private async Task saveWithConcurrencyHandling(ApplicationDbContext dbContext)
        {
            var saved = false;
            while (!saved)
            {
                try
                {
                    // Attempt to save changes to the database
                    await dbContext.SaveChangesAsync();
                    saved = true;
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogInformation("Got a database conflict!");
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is ResidentialProperty)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = entry.GetDatabaseValues();

                            var proposedUpdatedAt = proposedValues.GetValue<DateTime>("MatrixModifiedDT");
                            var databaseUpdatedAt = databaseValues.GetValue<DateTime>("MatrixModifiedDT");

                            if (proposedUpdatedAt > databaseUpdatedAt)
                            {
                                entry.OriginalValues.SetValues(proposedValues);
                            }
                            else
                            {
                                entry.OriginalValues.SetValues(databaseValues);
                            }
                        }
                        else
                        {
                            throw new Exception(
                                $"Don't know how to handle concurrency conflicts for {entry.Metadata.Name}"
                            );
                        }
                    }
                }
            }
        }

        public override void Complete()
        {
            _logger.LogInformation("DB: got Complete");
            base.Complete();
        }
    }
}
