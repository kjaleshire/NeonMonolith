using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NeonMonolith.Pipeline
{
    internal class DiskOutput : PipelineOutput<CsvMessage>
    {
        private readonly ILogger<SyncService> _logger;
        private readonly IOptions<AppConfig> _appConfig;
        private readonly string _destinationPath;

        internal DiskOutput(ILogger<SyncService> logger, IOptions<AppConfig> appConfig, string destinationPath)
        {
            _logger = logger;
            _appConfig = appConfig;
            _destinationPath = destinationPath;

            _block = new ActionBlock<CsvMessage>(HandleMessage);
        }

        private async Task HandleMessage(CsvMessage csvMessage)
        {
            if (String.IsNullOrEmpty(_appConfig.Value.LOCAL_DESTINATION))
            {
                _logger.LogInformation("Disk: destination wasn't set, skipping");
                return;
            }

            var fileName = String.Format(_appConfig.Value.CSV_FILENAME_FORMAT, csvMessage.SearchId,
                                         csvMessage.MinRecordId, csvMessage.MaxRecordId);

            var destinationDir = Path.Combine(_appConfig.Value.LOCAL_DESTINATION, _destinationPath);
            Directory.CreateDirectory(destinationDir);
            var filePath = Path.Combine(destinationDir, fileName);

            _logger.LogInformation($"Disk: writing CSV to {filePath}");

            using var sourceStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write,
                FileShare.None, bufferSize: 4096, useAsync: true);

            var csvRecord = csvMessage.CsvStringWriter.ToString()!;
            var encodedText = Encoding.UTF8.GetBytes(csvRecord);

            await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
        }

        public override void Complete()
        {
            _logger.LogInformation("Disk: got Complete");
            base.Complete();
        }
    }

}
