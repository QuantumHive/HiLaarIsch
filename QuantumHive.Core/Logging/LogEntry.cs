using System;

namespace QuantumHive.Core.Logging
{
    public class LogEntry
    {
        public LogEntry(LoggingEventType severity, string message, Exception exception = null)
        {
            Severity = severity;
            Message = message;
            Exception = exception;
        }

        public LoggingEventType Severity { get; }

        public string Message { get; }

        public Exception Exception { get; }
    }

    public enum LoggingEventType : byte
    {
        Debug,
        Information,
        Warning,
        Error,
        Fatal,
    }
}
