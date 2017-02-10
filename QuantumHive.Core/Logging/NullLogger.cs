namespace QuantumHive.Core.Logging
{
    public class NullLogger : ILogger
    {
        public void Log(LogEntry entry) { }
    }
}
