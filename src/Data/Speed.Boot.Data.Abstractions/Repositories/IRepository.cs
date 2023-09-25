// ReSharper disable once CheckNamespace

namespace Speed.Boot.Data.Abstractions;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
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

    Task<TEntity?> FindAsync(object keyValue, CancellationToken cancellationToken = default);

    Task<TEntity?> FindAsync(IEnumerable<object> keyValues, CancellationToken cancellationToken = default);

    Task<TEntity?> FirstOrDefaultAsync(IEnumerable<KeyValuePair<string, object>> keyValues, CancellationToken cancellationToken = default);

    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

    #endregion
}
