using System.Data;

namespace Sample.Data.Repositories.Interfaces;

public interface IRepository
{
    /// <summary>
    /// Create and open a Connection to the DB
    /// </summary>
    /// <param name="requireWrite">Do you require read only or read/write access to the DB</param>
    /// <param name="cancellationToken">Optional cancellation token to abort the Open</param>
    /// <returns>Opened connection to the DB</returns>
    Task<IDbConnection> CreateConnectionAsync(bool requireWrite = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Begin a transaction against an existing open Connection created using <see cref="Repository.CreateConnectionAsync"/>
    /// </summary>
    /// <param name="withConnection">Connection we will begin a transaction against</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <param name="isolationLevel">Optional isolation level (Default: <see cref="IsolationLevel.Snapshot"/>)</param>
    /// <returns>Transaction created against the passed open DB Connection</returns>
    Task<IDbTransaction> BeginTransactionAsync(
        IDbConnection withConnection,
        CancellationToken cancellationToken = default,
        IsolationLevel isolationLevel = IsolationLevel.Snapshot);
}