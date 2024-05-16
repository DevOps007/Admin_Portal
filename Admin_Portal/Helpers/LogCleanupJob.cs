using BusinessLayer.Interface;

namespace Bank_Portal.Helpers
{
    public class LogCleanupJob : ILogCleanupJob
    {
        private readonly ILogger<LogCleanupJob> _logger;
        private readonly string _logDirectory;
        private readonly int _daysToKeepLogs;

        public LogCleanupJob(ILogger<LogCleanupJob> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logDirectory = configuration["LogDirectory"];
            _daysToKeepLogs = 7;
        }

        public void CleanUpLogs()
        {
            try
            {
                DateTime deleteThreshold = DateTime.Now.AddDays(-_daysToKeepLogs);

                foreach (var logFile in Directory.GetFiles(_logDirectory))
                {
                    var fileInfo = new FileInfo(logFile);

                    if (fileInfo.CreationTime < deleteThreshold)
                    {
                        fileInfo.Delete();
                        _logger.LogInformation($"Deleted log file: {fileInfo.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while cleaning up logs.");
            }
        }
    }
}
