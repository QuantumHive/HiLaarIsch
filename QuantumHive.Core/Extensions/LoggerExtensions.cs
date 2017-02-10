using System;
using QuantumHive.Core.Logging;

namespace QuantumHive.Core.Extensions
{
    public static class LoggerExtensions
    {
        public static void Log(this ILogger logger, LoggingEventType severity, Exception exception)
        {
            var entry = new LogEntry(severity, null, exception);
            logger.Log(entry);
        }
    }
}
