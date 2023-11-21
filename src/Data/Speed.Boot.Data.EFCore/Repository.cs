// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.EFCore;

public class Repository<TEntity> : Repository<TEntity, IDbContext>, IRepository<TEntity>
    where TEntity : class, IEntity
{
    public Repository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

public class Repository<TEntity, TDbContext> : RepositoryBase<TEntity, TDbContext>
    where TEntity : class, IEntity
    where TDbContext : IDbContext
{
    private readonly IDbContextProvider _dbContextProvider;

    private DbContext? _dbContext;

    protected DbContext DbContext()
        => (_dbContext ??= _dbContextProvider.GetDbContext<TDbContext>() as DbContext)!;

    protected DbSet<TEntity> CurrentDbSet => DbContext().Set<TEntity>();

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
        => await CurrentDbSet.ToListAsync(cancellationToken);

    public override async Task<IEnumerable<TEntity>> GetListAsync(
        string sortField,
        bool isDescending = true,
        CancellationToken cancellationToken = default)
        => await CurrentDbSet.OrderBy(sortField, isDescending).ToListAsync(cancellationToken);

    public override async Task<IEnumerable<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> condition,
        CancellationToken cancellationToken = default)
        => await CurrentDbSet.Where(condition).ToListAsync(cancellationToken);

    public override async Task<IEnumerable<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> condition,
        string sortField,
        bool isDescending = true,
        CancellationToken cancellationToken = default)
        => await CurrentDbSet.Where(condition).OrderBy(sortField, isDescending).ToListAsync(cancellationToken);

    public override Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        => CurrentDbSet.LongCountAsync(cancellationToken);

    public override Task<long> GetCountAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        => CurrentDbSet.LongCountAsync(condition, cancellationToken);

    public override Task<List<TEntity>> GetPaginatedListAsync(
        int skip,
        int take,
        string sortField,
        bool isDescending = true,
        CancellationToken cancellationToken = default)
        => CurrentDbSet.OrderBy(sortField, isDescending).Skip(skip).Take(take).ToListAsync(cancellationToken);

    public override Task<List<TEntity>> GetPaginatedListAsync(
        int skip,
        int take,
        Dictionary<string, bool>? sorting = null,
        CancellationToken cancellationToken = default)
        => CurrentDbSet.OrderBy(sorting).Skip(skip).Take(take).ToListAsync(cancellationToken);

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
        => CurrentDbSet.Where(condition).OrderBy(sorting).Skip(skip).Take(take).ToListAsync(cancellationToken);

    public override Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = CurrentDbSet.Remove(entity);
        return Task.FromResult(entry.Entity);
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

    public override async Task<TEntity?> FindAsync(IEnumerable<object> keys, CancellationToken cancellationToken = default)
        => await Task.Run(async () => await CurrentDbSet.FindAsync(keys.ToArray()), cancellationToken);

    public override Task<TEntity?> FirstOrDefaultAsync(
        IEnumerable<KeyValuePair<string, object>> fields,
        CancellationToken cancellationToken = default)
    {
        return CurrentDbSet.GetQueryable(SpeedBoot.System.EnumerableExtensions.ToDictionary(fields)).FirstOrDefaultAsync(cancellationToken);
    }

    public override Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> condition,
        CancellationToken cancellationToken = default)
        => CurrentDbSet.FirstOrDefaultAsync(condition, cancellationToken);
}
