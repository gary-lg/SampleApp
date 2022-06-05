using System.Data;
using System.Reflection;
using Dapper;
using DbUp;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Sample.Data.Migrator;

public class DbMigrator : IDbMigrator
{
    private readonly IDbSecrets _dbSecrets;
    private readonly ILogger _log;

    /// <summary>Name of the embedded resource containing the initialisation script</summary>
    private const string InitSqlResourceName = "Sample.Data.Migrator.Scripts.000000000000-InitDb.sql";

    public DbMigrator(
        IDbSecrets dbSecrets,
        ILogger log)
    {
        _dbSecrets = dbSecrets;
        _log = log;
    }
    
    /// <summary>
    /// Perform the initial DB initialisation
    /// </summary>
    public void InitialiseDb()
    {
        using var scriptStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(InitSqlResourceName);
        if (scriptStream == null)
        {
            _log.LogError("Fatal error: failed to find initialisation script {InitSqlResourceName}. Is it embedded as a resource?", InitSqlResourceName);
            throw new DbMigratorException($"Fatal error: failed to find initialisation script {InitSqlResourceName}. Is it embedded as a resource?");
        }

        using var scriptReader = new StreamReader(scriptStream);

        _log.LogInformation("Initialising SampleApp database");

        using var conn = new NpgsqlConnection(_dbSecrets.AdminInitConnectionString);
        conn.Open();

        var dbExists = conn.ExecuteScalar<bool>("SELECT EXISTS(SELECT datname FROM pg_catalog.pg_database WHERE lower(datname) = lower('sampleappdb'));");
        if (dbExists)
        {
            _log.LogInformation("DB Already exists. Skipping init");
            return;
        }
			        
        var cmd = conn.CreateCommand();
        cmd.CommandText = scriptReader.ReadToEnd();
        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
        _log.LogInformation("Database initialisation complete");
    }

    public void ApplyMigrations()
    {
        // Run all scripts with the exception of our init script.
        var upgrader =
            DeployChanges.To
                .PostgresqlDatabase(_dbSecrets.ReadWriteConnectionString)
                .WithScriptsEmbeddedInAssembly(typeof(DbMigrator).Assembly, scriptName => scriptName != InitSqlResourceName)
                .LogTo(new UpgradeLogger(_log))
                .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            throw new DbMigratorException("An error occurred whilst applying upgrade", result.Error);
        }
    }
}