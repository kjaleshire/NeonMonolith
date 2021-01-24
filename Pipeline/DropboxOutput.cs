using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NeonMonolith.Pipeline
{
    internal class DropboxOutput : PipelineOutput<CsvMessage>
    {
        // Chunk size is 1 MiB
        private const int _chunkSize = 1 << 20;
        private readonly ILogger<SyncService> _logger;
        private readonly IOptions<AppConfig> _appConfig;
        private readonly string _destinationPath;

        internal DropboxOutput(ILogger<SyncService> logger, IOptions<AppConfig> appConfig, string destinationPath)
        {
            _logger = logger;
            _appConfig = appConfig;
            _destinationPath = destinationPath;

            _block = new ActionBlock<CsvMessage>(HandleMessage);
        }

        public async Task HandleMessage(CsvMessage csvMessage)
        {
            if (String.IsNullOrWhiteSpace(_appConfig.Value.DROPBOX_ROOT))
            {
                _logger.LogInformation("Dropbox: destination wasn't set, skipping");
                return;
            }

            using var httpClient = new HttpClient() { Timeout = new TimeSpan(0, 10, 0) };
            var dropboxConfig = new DropboxClientConfig() { HttpClient = httpClient };
            using var dropboxClient = new DropboxClient(_appConfig.Value.DROPBOX_TOKEN, dropboxConfig);

            var fileName = String.Format(_appConfig.Value.CSV_FILENAME_FORMAT, csvMessage.SearchId, csvMessage.MinRecordId, csvMessage.MaxRecordId);

            var path = Path.Combine(_appConfig.Value.DROPBOX_ROOT, _destinationPath, fileName);

            var account = await dropboxClient.Users.GetCurrentAccountAsync();
            _logger.LogInformation($"Dropbox: uploading as {account.Email} to {path}");

            var csvRecord = csvMessage.CsvStringWriter.ToString()!;
            using var csvFileStream = new MemoryStream(Encoding.UTF8.GetBytes(csvRecord));

            var commitInfo = new CommitInfo(path, mode: WriteMode.Overwrite.Instance);
            await dropboxClient.Files.UploadAsync(commitInfo, csvFileStream);

            _logger.LogInformation($"Dropbox: finished upload as {account.Email} to {path}");
        }

        public override void Complete()
        {
            _logger.LogInformation("Dropbox: got Complete");
            base.Complete();
        }
    }
}
