using Microsoft.Extensions.DependencyInjection.Extensions;
using Sample.Data.Migrator;

namespace GeoApi;

public static class MigrateDbExtensions
{
    /// <summary>
    /// Attempts to run any DB migrations after all IoC registrations have been completed
    /// but before the service begins accepting requests. 
    /// </summary>
    /// <param name="services"></param>
    public static void MigrateDb(this IServiceCollection services)
    {
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IStartupFilter, MigrateDbStartupFilter>());
    }
}

/// <summary>
/// Startup filter which runs after IoC registrations but prior to accepting web connections. This
/// filter attempts to run all DB migrations against the DB.
/// </summary>
public class MigrateDbStartupFilter : IStartupFilter
{
    private readonly ILogger _log;
    private readonly IDbMigrator _migrator;

    public MigrateDbStartupFilter(
        ILogger log,
        IDbMigrator migrator)
    {
        _log = log;
        _migrator = migrator;
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        try
        {
            _log.LogInformation("Preparing to perform DB migration");
            _migrator.InitialiseDb();
            _migrator.ApplyMigrations();
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Error occurred whilst performing DB migration");
            throw;
        }

        _log.LogInformation("DB migration complete");
        return next;
    }
}