// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql;

public class Repository<TEntity> : Repository<TEntity, IDbContext>, IRepository<TEntity>
    where TEntity : class, IEntity
{
    public Repository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

public class Repository<TEntity, TDbContext> : RepositoryBase<TEntity, TDbContext>
    where TEntity : class, IEntity
    where TDbContext : class, IDbContext
{
    private readonly IDbContextProvider _dbContextProvider;
    private ITableRelationProvider? _tableRelationProvider;

    ITableRelationProvider GetTableRelationProvider()
        => _tableRelationProvider ??= ServiceProvider.GetRequiredService<ITableRelationProvider>();

    protected SpeedDbContext DbContext
        => (DbContextGenerics as SpeedDbContext)!;

    private TDbContext? _dbContext;

    private TDbContext DbContextGenerics => (_dbContext ??= _dbContextProvider.GetDbContext<TDbContext>() as TDbContext)!;

    protected DbSet<TEntity> CurrentDbSet => this.DbContext.Set<TEntity>();

    protected ISelect<TEntity> GetCurrentEntity(Expression<Func<TEntity, bool>>? condition = null) =>
        condition == null ? CurrentDbSet.Where(_ => true) : CurrentDbSet.Where(condition);

    public Repository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _dbContextProvider = serviceProvider.GetRequiredService<IDbContextProvider>();
    }

    public override async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await CurrentDbSet.AddAsync(entity, cancellationToken);

    public override Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        CurrentDbSet.Update(entity);
        return Task.CompletedTask;
    }

    public override async Task<IEnumerable<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
        => await GetCurrentEntity().ToListAsync(cancellationToken);

    public override async Task<IEnumerable<TEntity>> GetListAsync(
        string sortField,
        bool isDescending = true,
        CancellationToken cancellationToken = default)
        => await GetCurrentEntity().OrderBy(sortField, isDescending).ToListAsync(cancellationToken);

    public override async Task<IEnumerable<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> condition,
        CancellationToken cancellationToken = default)
        => await GetCurrentEntity(condition).ToListAsync(cancellationToken);

    public override async Task<IEnumerable<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> condition,
        string sortField,
        bool isDescending = true,
        CancellationToken cancellationToken = default)
        => await GetCurrentEntity(condition).OrderBy(sortField, isDescending).ToListAsync(cancellationToken);

    public override Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        => GetCurrentEntity().CountAsync(cancellationToken);

    public override Task<long> GetCountAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        => GetCurrentEntity(condition).CountAsync(cancellationToken);

    public override Task<List<TEntity>> GetPaginatedListAsync(
        int skip,
        int take,
        string sortField,
        bool isDescending = true,
        CancellationToken cancellationToken = default)
        => GetCurrentEntity().OrderBy(sortField, isDescending).Skip(skip).Take(take).ToListAsync(cancellationToken);

    public override Task<List<TEntity>> GetPaginatedListAsync(
        int skip,
        int take,
        Dictionary<string, bool>? sorting = null,
        CancellationToken cancellationToken = default)
        => GetPaginatedListAsync(_ => true, skip, take, sorting, cancellationToken);

    public override Task<List<TEntity>> GetPaginatedListAsync(
        Expression<Func<TEntity, bool>> condition,
        int skip,
        int take,
        string sortField,
        bool isDescending = true,
        CancellationToken cancellationToken = default)
        => CurrentDbSet.Where(condition).OrderBy(sortField, isDescending).Skip(skip).Take(take).ToListAsync(cancellationToken);

    public override Task<List<TEntity>> GetPaginatedListAsync(
        Expression<Func<TEntity, bool>> condition,
        int skip,
        int take,
        Dictionary<string, bool>? sorting = null,
        CancellationToken cancellationToken = default)
    {
        var currentEntity = GetCurrentEntity(condition);
        if (sorting != null)
        {
            currentEntity = sorting.Aggregate(currentEntity, (current, item) => current.OrderByPropertyName(item.Key, !item.Value));
        }

        return currentEntity.Skip(skip).Take(take).ToListAsync(cancellationToken);
    }

    public override Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        CurrentDbSet.Remove(entity);
        return Task.FromResult(entity);
    }

    public override Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        CurrentDbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public override async Task RemoveAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
    {
        var entities = await GetListAsync(condition, cancellationToken);
        CurrentDbSet.RemoveRange(entities);
    }

    public override Task<TEntity?> FindAsync(IEnumerable<object> keys, CancellationToken cancellationToken = default)
    {
        var primaryKeys = GetTableRelationProvider().GetKeys<TEntity>(DbContextGenerics.GetType());
        var keyValues = new Dictionary<string, object>();
        // SpeedArgumentException.ThrowIf(keys.Count() != primaryKeys.Length, "Primary key key error");
        for (var index = 0; index < primaryKeys.Length; index++)
        {
            keyValues.Add(primaryKeys[index], keys.Skip(index).Take(1));
        }

        return FirstOrDefaultAsync(keyValues, cancellationToken);
    }

    public override async Task<TEntity?> FirstOrDefaultAsync(
        IEnumerable<KeyValuePair<string, object>> keyValues,
        CancellationToken cancellationToken = default)
    {
        var currentEntity = GetCurrentEntity();
        return await currentEntity.WhereDynamic(keyValues.ToDictionary()).FirstAsync(cancellationToken);
    }

    public override Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> condition,
        CancellationToken cancellationToken = default)
        => GetCurrentEntity(condition).FirstAsync(cancellationToken);
}
