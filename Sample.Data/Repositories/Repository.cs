using System.Data;
using Dapper.Contrib.Extensions;
using Npgsql;
using Sample.Data.Repositories.Interfaces;

namespace Sample.Data.Repositories;

public abstract class Repository : IRepository
{
    private readonly string _readOnlyConnectionString;
    private readonly string _readWriteConnectionString;

    static Repository()
    {
        // Dapper - Don't mess with model names
        SqlMapperExtensions.TableNameMapper = type => type.Name;
    }

    protected Repository(IDbSecrets dbSecrets)
    {
        _readOnlyConnectionString = dbSecrets.ReadOnlyConnectionString;
        _readWriteConnectionString = dbSecrets.ReadWriteConnectionString;
    }

    /// <summary>
    /// Create and open a Connection to the DB
    /// </summary>
    /// <param name="requireWrite">Do you require read only or read/write access to the DB</param>
    /// <param name="cancellationToken">Optional cancellation token to abort the Open</param>
    /// <returns>Opened connection to the DB</returns>
    public async Task<IDbConnection> CreateConnectionAsync(bool requireWrite = false, CancellationToken cancellationToken = default)
    {
        var conn = new NpgsqlConnection(requireWrite ? _readWriteConnectionString : _readOnlyConnectionString);
        await conn.OpenAsync(cancellationToken);
        return conn;
    }

    /// <summary>
    /// Begin a transaction against an existing open Connection created using <see cref="CreateConnectionAsync"/>
    /// </summary>
    /// <param name="withConnection">Connection we will begin a transaction against</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <param name="isolationLevel">Optional isolation level (Default: <see cref="IsolationLevel.Snapshot"/>)</param>
    /// <returns>Transaction created against the passed open DB Connection</returns>
    public async Task<IDbTransaction> BeginTransactionAsync(
        IDbConnection withConnection,
        CancellationToken cancellationToken = default,
        IsolationLevel isolationLevel = IsolationLevel.Snapshot)
    {
        var conn = (NpgsqlConnection)withConnection;
        return await conn.BeginTransactionAsync(isolationLevel, cancellationToken);
    }
}