// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.EFCore;

public class Repository<TEntity, TDbContextProvider> : RepositoryBase<TEntity>
    where TEntity : class, IEntity
    where TDbContextProvider : IDbContextProvider
{
    private readonly IDbContextProvider _dbContextProvider;
    private readonly ConcurrentDictionary<DbOperateTypes, Lazy<DbContext>> _data = new();
    protected DbContext WriteDbContext => GetDbContext(DbOperateTypes.Write);
    protected DbContext ReadDbContext => GetDbContext(DbOperateTypes.Read);
    protected DbSet<TEntity> CurrentWriteDbSet => WriteDbContext.Set<TEntity>();
    protected DbSet<TEntity> CurrentReadDbSet => ReadDbContext.Set<TEntity>();

    public Repository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _dbContextProvider = serviceProvider.GetRequiredService<TDbContextProvider>();
    }

    protected DbContext GetDbContext(DbOperateTypes dbOperateType)
        => _data.GetOrAdd(
            dbOperateType,
            new Lazy<DbContext>(() => _dbContextProvider.GetDbContext<TEntity>(dbOperateType) as DbContext,
                LazyThreadSafetyMode.ExecutionAndPublication)
        ).Value;

    public override async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await CurrentWriteDbSet.AddAsync(entity, cancellationToken);

    public override Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        CurrentWriteDbSet.Update(entity);
        return Task.CompletedTask;
    }

    public override async Task<IEnumerable<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
        => await CurrentReadDbSet.ToListAsync(cancellationToken);

    public override async Task<IEnumerable<TEntity>> GetListAsync(
        string sortField,
        bool isDescending = true,
        CancellationToken cancellationToken = default)
        => await CurrentReadDbSet.OrderBy(sortField, isDescending).ToListAsync(cancellationToken);

    public override async Task<IEnumerable<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> condition,
        CancellationToken cancellationToken = default)
        => await CurrentReadDbSet.Where(condition).ToListAsync(cancellationToken);

    public override async Task<IEnumerable<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> condition,
        string sortField,
        bool isDescending = true,
        CancellationToken cancellationToken = default)
        => await CurrentReadDbSet.Where(condition).OrderBy(sortField, isDescending).ToListAsync(cancellationToken);

    public override Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        => CurrentReadDbSet.LongCountAsync(cancellationToken);

    public override Task<long> GetCountAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        => CurrentReadDbSet.LongCountAsync(condition, cancellationToken);

    public override Task<List<TEntity>> GetPaginatedListAsync(
        int skip,
        int take,
        string sortField,
        bool isDescending = true,
        CancellationToken cancellationToken = default)
        => CurrentReadDbSet.OrderBy(sortField, isDescending).Skip(skip).Take(take).ToListAsync(cancellationToken);

    public override Task<List<TEntity>> GetPaginatedListAsync(
        int skip,
        int take,
        Dictionary<string, bool>? sorting = null,
        CancellationToken cancellationToken = default)
        => CurrentReadDbSet.OrderBy(sorting).Skip(skip).Take(take).ToListAsync(cancellationToken);

    public override Task<List<TEntity>> GetPaginatedListAsync(
        Expression<Func<TEntity, bool>> condition,
        int skip,
        int take,
        string sortField,
        bool isDescending = true,
        CancellationToken cancellationToken = default)
        => CurrentReadDbSet.Where(condition).OrderBy(sortField, isDescending).Skip(skip).Take(take).ToListAsync(cancellationToken);

    public override Task<List<TEntity>> GetPaginatedListAsync(
        Expression<Func<TEntity, bool>> condition,
        int skip,
        int take,
        Dictionary<string, bool>? sorting = null,
        CancellationToken cancellationToken = default)
        => CurrentReadDbSet.Where(condition).OrderBy(sorting).Skip(skip).Take(take).ToListAsync(cancellationToken);

    public override Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = CurrentWriteDbSet.Remove(entity);
        return Task.FromResult(entry.Entity);
    }

    public override Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        CurrentWriteDbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public override async Task RemoveAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
    {
        var entities = await GetListAsync(condition, cancellationToken);
        CurrentWriteDbSet.RemoveRange(entities);
    }

    public override async Task<TEntity?> FindAsync(object keyValue, CancellationToken cancellationToken = default)
        => await CurrentReadDbSet.FindAsync(keyValue, cancellationToken);

    public override async Task<TEntity?> FindAsync(IEnumerable<object> keyValues, CancellationToken cancellationToken = default)
        => await CurrentReadDbSet.FindAsync(keyValues, cancellationToken);

    public override Task<TEntity?> FirstOrDefaultAsync(
        IEnumerable<KeyValuePair<string, object>> keyValues,
        CancellationToken cancellationToken = default)
    {
        Dictionary<string, object> fields = keyValues.ToDictionary();
        return CurrentReadDbSet.GetQueryable(fields).FirstOrDefaultAsync(cancellationToken);
    }

    public override Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> condition,
        CancellationToken cancellationToken = default)
        => CurrentReadDbSet.FirstOrDefaultAsync(condition, cancellationToken);
}
