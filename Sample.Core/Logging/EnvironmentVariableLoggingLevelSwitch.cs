using System.Timers;
using Serilog.Core;
using Serilog.Events;
using Timer = System.Timers.Timer;

namespace Sample.Core.Logging;

/// <summary>
/// Creates a switch which allows changing of log level based on an environment variable.
/// </summary>
public class EnvironmentVariableLoggingLevelSwitch : LoggingLevelSwitch, IDisposable
{
    /// <summary>
    /// Name of the environment variable we will look for to control log level. Value of the
    /// environment variable should be set to one of: Verbose, Debug, Information, Warning,
    /// Error or Fatal. The value is not case sensitive.
    /// </summary>
    public string EnvironmentVariableName { get; set; } = "SAMPLE_LOGLEVEL";

    /// <summary>Level we will set if no env var is found</summary>
    private readonly LogEventLevel _defaultLevel;

    /// <summary>
    /// Refresh interval, when this amount of time passes we will inspect the env var for changes
    /// Default: one minute. 
    /// </summary>
    public static int RefreshIntervalMs { get; set; } = 60 * 1000;

    /// <summary>
    /// Refreshes the log state from the environment variable at the given
    /// <see cref="RefreshIntervalMs"/>
    /// </summary>
    private readonly Timer _logLevelRefresher;

    public EnvironmentVariableLoggingLevelSwitch(LogEventLevel defaultLevel)
    {
        _defaultLevel = defaultLevel;
        MinimumLevel = defaultLevel;
        _logLevelRefresher = new Timer(RefreshIntervalMs);
        _logLevelRefresher.Elapsed += RefreshLogLevel;
        _logLevelRefresher.Start();
    }

    private void RefreshLogLevel(object? sender, ElapsedEventArgs e)
    {
        try
        {
            _logLevelRefresher.Stop();
            var ev = Environment.GetEnvironmentVariable(EnvironmentVariableName);
            if (Enum.TryParse<LogEventLevel>(ev, true, out var newLevel))
            {
                this.MinimumLevel = newLevel;
                return;
            }
        }
        catch (Exception)
        {
            // Ignore
        }
        finally
        {
            _logLevelRefresher.Start();
        }

        MinimumLevel = _defaultLevel;
    }

    public void Dispose()
    {
        _logLevelRefresher.Dispose();
    }
}