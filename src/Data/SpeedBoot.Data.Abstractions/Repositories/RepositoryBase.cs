// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Data.Abstractions;

public abstract class RepositoryBase<TEntity, TDbContext> :
    IRepository<TEntity, TDbContext>
    where TEntity : class, IEntity
    where TDbContext : IDbContext
{
    protected IServiceProvider ServiceProvider { get; }

    protected RepositoryBase(IServiceProvider serviceProvider)
        => ServiceProvider = serviceProvider;

    #region IRepository<TEntity>

    public abstract Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            await AddAsync(entity, cancellationToken);
        }
    }

    public virtual Task<TEntity?> FindAsync(params object[] keys)
        => FindAsync(keys, CancellationToken.None);

    public virtual Task<TEntity?> FindAsync(
        object key,
        CancellationToken cancellationToken = default)
        => FindAsync(new List<object>() { key }, CancellationToken.None);

    public abstract Task<TEntity?> FindAsync(
        IEnumerable<object> keys,
        CancellationToken cancellationToken = default);

    public abstract Task<TEntity?> FirstOrDefaultAsync(
        IEnumerable<KeyValuePair<string, object>> fields,
        CancellationToken cancellationToken = default);

    public abstract Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> condition,
        CancellationToken cancellationToken = default);

    public abstract Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

    public abstract Task RemoveAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

    public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            await RemoveAsync(entity, cancellationToken);
        }
    }

    public abstract Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            await UpdateAsync(entity, cancellationToken);
        }
    }

    public abstract Task<IEnumerable<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

    public abstract Task<IEnumerable<TEntity>> GetListAsync(
        string sortField,
        bool isDescending = true,
        CancellationToken cancellationToken = default);

    public abstract Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition,
        CancellationToken cancellationToken = default);

    public abstract Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition, string sortField,
        bool isDescending = true, CancellationToken cancellationToken = default);

    public abstract Task<long> GetCountAsync(CancellationToken cancellationToken = default);

    public abstract Task<long> GetCountAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a paginated list after sorting according to skip and take
    /// </summary>
    /// <param name="skip">The number of elements to skip before returning the remaining elements</param>
    /// <param name="take">The number of elements to return</param>
    /// <param name="sortField">Sort field name</param>
    /// <param name="isDescending">true descending order, false ascending order, default: true</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<List<TEntity>> GetPaginatedListAsync(int skip, int take, string sortField, bool isDescending = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a paginated list after sorting according to skip and take
    /// </summary>
    /// <param name="skip">The number of elements to skip before returning the remaining elements</param>
    /// <param name="take">The number of elements to return</param>
    /// <param name="sorting">Key: sort field name, Value: true descending order, false ascending order</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<List<TEntity>> GetPaginatedListAsync(int skip, int take, Dictionary<string, bool>? sorting = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a paginated list after sorting by condition
    /// </summary>
    /// <param name="condition"> A function to test each element for a condition</param>
    /// <param name="skip">The number of elements to skip before returning the remaining elements</param>
    /// <param name="take">The number of elements to return</param>
    /// <param name="sortField">Sort field name</param>
    /// <param name="isDescending">true descending order, false ascending order, default: true</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<List<TEntity>> GetPaginatedListAsync(Expression<Func<TEntity, bool>> condition, int skip, int take,
        string sortField,
        bool isDescending = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a paginated list after sorting by condition
    /// </summary>
    /// <param name="condition"> A function to test each element for a condition</param>
    /// <param name="skip">The number of elements to skip before returning the remaining elements</param>
    /// <param name="take">The number of elements to return</param>
    /// <param name="sorting">Key: sort field name, Value: true descending order, false ascending order</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<List<TEntity>> GetPaginatedListAsync(Expression<Func<TEntity, bool>> condition, int skip, int take,
        Dictionary<string, bool>? sorting = null, CancellationToken cancellationToken = default);

    public virtual async Task<PaginatedList<TEntity>> GetPaginatedListAsync(
        PaginatedOptions options,
        CancellationToken cancellationToken = default)
    {
        var result = await GetPaginatedListAsync(
            (options.Page - 1) * options.PageSize,
            options.PageSize <= 0 ? int.MaxValue : options.PageSize,
            options.Sorting,
            cancellationToken
        );

        var total = await GetCountAsync(cancellationToken);

        return new PaginatedList<TEntity>()
        {
            Total = total,
            Result = result,
            TotalPages = (int)Math.Ceiling(total / (decimal)options.PageSize)
        };
    }

    public virtual async Task<PaginatedList<TEntity>> GetPaginatedListAsync(
        Expression<Func<TEntity, bool>> condition,
        PaginatedOptions options,
        CancellationToken cancellationToken = default)
    {
        var result = await GetPaginatedListAsync(
            condition,
            (options.Page - 1) * options.PageSize,
            options.PageSize <= 0 ? int.MaxValue : options.PageSize,
            options.Sorting,
            cancellationToken
        );

        var total = await GetCountAsync(condition, cancellationToken);

        return new PaginatedList<TEntity>()
        {
            Total = total,
            Result = result,
            TotalPages = (int)Math.Ceiling(total / (decimal)options.PageSize)
        };
    }

    #endregion
}
