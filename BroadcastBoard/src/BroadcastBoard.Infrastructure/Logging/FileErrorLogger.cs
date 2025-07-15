using BroadcastBoard.Application.Common.Interfaces;
using Microsoft.Extensions.Options;

namespace BroadcastBoard.Infrastructure.Logging
{
    public class FileErrorLogger : IErrorLogger
    {
        private readonly string _logFilePath;
        private readonly object _lock = new();

        public FileErrorLogger(IOptions<LoggingOptions> options)
        {
            _logFilePath = options.Value.ErrorLogPath;
            var directory = Path.GetDirectoryName(_logFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void Log(string message)
        {
            try
            {
                var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR: {message}";
                lock (_lock)
                {
                    File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to log error: {ex.Message}");
            }
        }
    }
}
