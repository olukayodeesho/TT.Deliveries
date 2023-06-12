using Microsoft.Extensions.Logging;

namespace TT.Deliveries.Web.Api.Logging
{
    public class LoggerAdapter<T> : ILoggerAdapter<T>
    {
        private readonly ILogger<T> logger;

        public LoggerAdapter(ILogger<T> logger)
        {
            this.logger = logger;
        }

        public void LogInformation(string message)
        {
            logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            logger.LogWarning(message);
        }

        public void LogError(string message)
        {
            logger.LogError(message);
        }
    }

    public interface ILoggerAdapter<T>
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}
