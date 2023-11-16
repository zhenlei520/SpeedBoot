// ReSharper disable once CheckNamespace

namespace Speed.Boot.Data.Abstractions;

public interface IRepository<TEntity> : IRepository<TEntity, IDbContext>
    where TEntity : class, IEntity
{

}

public interface IRepository<TEntity, TDbContext>
    where TEntity : class, IEntity
    where TDbContext : IDbContext
{
    #region Add

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    #endregion

    #region Update

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    #endregion

    #region Remove

    Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task RemoveAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

    #endregion

    #region Find

    /// <summary>
    /// Get entity information based on primary key
    /// </summary>
    /// <param name="keys">A collection of primary key Key values</param>
    /// <returns></returns>
    Task<TEntity?> FindAsync(params object[] keys);

    /// <summary>
    /// Get entity information based on primary key
    /// </summary>
    /// <param name="key">A collection of primary key Key values</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity?> FindAsync(object key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get entity information based on primary key
    /// </summary>
    /// <param name="keys">A collection of primary key Key values</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity?> FindAsync(IEnumerable<object> keys, CancellationToken cancellationToken = default);

    Task<TEntity?> FirstOrDefaultAsync(IEnumerable<KeyValuePair<string, object>> fields, CancellationToken cancellationToken = default);

    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

    #endregion

}
