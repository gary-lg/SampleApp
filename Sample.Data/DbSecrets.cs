namespace Sample.Data;

/// <inheritdoc />
public class DbSecrets : IDbSecrets
{
    /// <inheritdoc />
    public string ReadOnlyConnectionString { get; }

    /// <inheritdoc />
    public string ReadWriteConnectionString { get; }

    /// <inheritdoc />
    public string AdminInitConnectionString { get; }

    public DbSecrets(
        string readOnlyConnectionString, 
        string readWriteConnectionString,
        string adminInitConnectionString)
    {
        ReadOnlyConnectionString = readOnlyConnectionString;
        ReadWriteConnectionString = readWriteConnectionString;
        AdminInitConnectionString = adminInitConnectionString;
    }
}