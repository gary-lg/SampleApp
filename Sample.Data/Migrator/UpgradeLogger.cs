using DbUp.Engine.Output;
using Microsoft.Extensions.Logging;

namespace Sample.Data.Migrator;

/// <summary>
/// Simple logger which we hand off to DbUp in the Migrator class
/// </summary>
internal class UpgradeLogger : IUpgradeLog
{
    private readonly ILogger _log;

    internal UpgradeLogger(ILogger log)
    {
        _log = log;
    }

    public void WriteInformation(string format, params object[] args)
    {
        _log.LogInformation(format, args);
    }

    public void WriteError(string format, params object[] args)
    {
        _log.LogError(format, args);
    }

    public void WriteWarning(string format, params object[] args)
    {
        _log.LogWarning(format, args);
    }
}