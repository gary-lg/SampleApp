namespace Sample.Data.Migrator;

public interface IDbMigrator
{
    /// <summary>
    /// Perform the initial DB initialisation
    /// </summary>
    void InitialiseDb();

    void ApplyMigrations();
}