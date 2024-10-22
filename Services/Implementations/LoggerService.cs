using DeliveryApp.Services.Interfaces;

namespace DeliveryApp.Services.Implementations
{
    public class LoggerService : ILoggerService
    {
        private readonly string _logFilePath;
        public LoggerService(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void Log(string message)
        {
            File.AppendAllText(_logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
        }
    }
}
