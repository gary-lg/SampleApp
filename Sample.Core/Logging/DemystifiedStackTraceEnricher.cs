using System.Diagnostics;
using Serilog.Core;
using Serilog.Events;

namespace Sample.Core.Logging;

/// <summary>
/// Enricher which will demystify exceptions in the log - this rewrites anonymous classes
/// etc to be a little more more human readable
/// </summary>
public class DemystifiedStackTraceEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.Exception?.Demystify();
    }
}