// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.FreeSql;

public class UnitOfWorkWrapper
{
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWorkWrapper(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task SaveChangesAsync(Type dbContextType, CancellationToken cancellationToken)
    {
        var dbContext = GetTDbContext(dbContextType);
        return dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task CommitAsync(Type dbContextType, CancellationToken cancellationToken)
    {
        var dbContext = GetTDbContext(dbContextType);
        dbContext.UnitOfWork.Commit();
        return Task.CompletedTask;
    }

    public Task RollbackAsync(Type dbContextType, CancellationToken cancellationToken)
    {
        var dbContext = GetTDbContext(dbContextType);
        dbContext.UnitOfWork.Rollback();
        return Task.CompletedTask;
    }

    private SpeedDbContext GetTDbContext(Type dbContextType)
    {
        var dbContext = _serviceProvider.GetService(dbContextType) as SpeedDbContext;
        SpeedArgumentException.ThrowIfNull(dbContext);
        return dbContext;
    }
}
