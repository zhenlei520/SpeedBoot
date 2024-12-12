// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql;

public class DefaultUnitOfWork : Abstractions.IUnitOfWork
{
    private readonly UnitOfWorkWrapper _unitOfWorkWrapper;

    public DefaultUnitOfWork(UnitOfWorkWrapper unitOfWorkWrapper)
    {
        _unitOfWorkWrapper = unitOfWorkWrapper;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var dbContextType in GlobalDataConfig.DbContextTypes)
        {
            await _unitOfWorkWrapper.SaveChangesAsync(dbContextType, cancellationToken);
        }
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        foreach (var dbContextType in GlobalDataConfig.DbContextTypes)
        {
            await _unitOfWorkWrapper.CommitAsync(dbContextType, cancellationToken);
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        foreach (var dbContextType in GlobalDataConfig.DbContextTypes)
        {
            await _unitOfWorkWrapper.RollbackAsync(dbContextType, cancellationToken);
        }
    }
}
