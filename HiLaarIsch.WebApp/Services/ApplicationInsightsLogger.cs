using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using QuantumHive.Core;
using QuantumHive.Core.Logging;

namespace HiLaarIsch.Services
{
    public class ApplicationInsightsLogger : ILogger
    {
        private readonly TelemetryClient telemetry;

        public ApplicationInsightsLogger(TelemetryClient telemetry)
        {
            this.telemetry = telemetry;
        }

        public void Log(LogEntry entry)
        {
            switch(entry.Severity)
            {
                case LoggingEventType.Debug:
                    break;
                case LoggingEventType.Information:
                    break;
                case LoggingEventType.Warning:
                    break;
                case LoggingEventType.Error:
                case LoggingEventType.Fatal:
                    this.telemetry.TrackException(entry.Exception, this.LogEntryToDictionary(entry));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IDictionary<string, string> LogEntryToDictionary(LogEntry entry)
        {
            return new Dictionary<string, string>
            {
                { nameof(LogEntry.Message), entry.Message },
                { nameof(LogEntry.Severity), entry.Severity.ToString() },
            };
        }
    }
}