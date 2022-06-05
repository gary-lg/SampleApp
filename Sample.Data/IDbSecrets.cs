namespace Sample.Data;

/// <summary>
/// Represents secrets we use to connect to the database 
/// </summary>
public interface IDbSecrets
{
    /// <summary>Connection string which gives read-only access to the database</summary>
    string ReadOnlyConnectionString { get; }

    /// <summary>Connection string which gives read-write access to the database</summary>
    string ReadWriteConnectionString { get; }
    
    /// <summary>Connection string for the root admin of the database</summary>
    string AdminInitConnectionString { get; }
}