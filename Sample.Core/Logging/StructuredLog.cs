using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;

namespace Sample.Core.Logging;

public class StructuredLog
{
    /// <summary>Underlying Serilog Logger, used to initiate the .Net logging system</summary>
    public ILogger? Logger { get; protected set; }

    /// <summary>Switch we will use to control the output level of the log</summary>
    public LoggingLevelSwitch LevelSwitch { get; set; }

    /// <summary>
    /// Restrict the maximum length of strings in characters as we destructure objects into the log.
    /// Default: 50KB
    /// </summary>
    private const int MaximumStringLengthCharacters = 1024 * 50;

    /// <summary>
    /// Create a new Structured Log. <b>You must call <see cref="Init"/> prior to use</b>
    /// </summary>
    public StructuredLog()
    {
        LevelSwitch = new LoggingLevelSwitch();
    }

    /// <summary>
    /// Initialise the log making it ready to accept log events and able to be used in
    /// the .Net logging framework.
    /// </summary>
    /// <param name="logLevel">Default level of log messages</param>
    /// <param name="environmentName">The environment the application is running in</param>
    /// <param name="enrichers">Any additional enrichers which will be applied to each log event</param>
    /// <returns>A StructuredLog which can be used to initialise the .Net logging system</returns>
    public void Init(LogEventLevel logLevel, EnvironmentName environmentName, ILogEventEnricher[]? enrichers = null)
    {
        var log = new StructuredLog();
        var config = log.GetLoggerConfiguration(logLevel, environmentName);

        if (enrichers != null && enrichers.Any())
        {
            config.Enrich.With(enrichers);
        }

        log.Logger = config.CreateLogger();

        // Set the static logger to avoid disposal problems
        Log.Logger = log.Logger;
    }

    /// <summary>
    /// Create a default logger configuration. The default enriches with thread, process, machine,
    /// and adds exception specific properties for better debugging of stack traces. You may override
    /// this method to alter the enrichment of the underlying logger.
    /// </summary>
    /// <returns>Logger configuration which will be used to create the underlying log</returns>
    protected virtual LoggerConfiguration GetLoggerConfiguration(LogEventLevel defaultLogLevel, EnvironmentName environmentName = EnvironmentName.Production)
    {
        LevelSwitch = new EnvironmentVariableLoggingLevelSwitch(defaultLogLevel);

        var config = new LoggerConfiguration()
            .Enrich.WithThreadId()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithDemystifiedStackTraces()

            // Destructure limits stop us going OOM or getting into a cyclic doom spiral
            .Destructure.ToMaximumCollectionCount(25)
            .Destructure.ToMaximumDepth(3)
            .Destructure.ToMaximumStringLength(MaximumStringLengthCharacters)

            // Set the switch and drop MS diagnostics
            .MinimumLevel.ControlledBy(LevelSwitch);

        // Make the console log output pretty for dev use
        if (environmentName == EnvironmentName.Dev)
        {
            config.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u4}] {ThreadId,2} - {Message:lj}{NewLine}{Properties}{NewLine}{Exception}");
        }
        else
        {
            config
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .Enrich.WithMachineName()
                .WriteTo.Console(new CompactJsonFormatter());
        }

        return config;
    }
}