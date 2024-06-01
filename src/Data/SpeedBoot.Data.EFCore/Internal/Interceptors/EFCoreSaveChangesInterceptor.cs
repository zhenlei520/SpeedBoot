// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET5_0_OR_GREATER

using Microsoft.EntityFrameworkCore.Diagnostics;
using DbContextErrorEventData = Microsoft.EntityFrameworkCore.Diagnostics.DbContextErrorEventData;
using DbContextEventData = Microsoft.EntityFrameworkCore.Diagnostics.DbContextEventData;
using ISaveChangesInterceptor = SpeedBoot.Data.Abstractions.ISaveChangesInterceptor;
using SaveChangesCompletedEventData = Microsoft.EntityFrameworkCore.Diagnostics.SaveChangesCompletedEventData;

namespace SpeedBoot.Data.EFCore;

internal class EFCoreSaveChangesInterceptor : Microsoft.EntityFrameworkCore.Diagnostics.ISaveChangesInterceptor
{
    private readonly IEnumerable<ISaveChangesInterceptor> _saveChangesInterceptors;
    private readonly IEnumerable<IDbContextInterceptor> _dbContextInterceptors;

    public EFCoreSaveChangesInterceptor(IServiceProvider serviceProvider)
    {
        _saveChangesInterceptors = serviceProvider.GetServices<ISaveChangesInterceptor>().OrderBy(i => i.Order);
        _dbContextInterceptors = serviceProvider.GetServices<IDbContextInterceptor>().OrderBy(i => i.Order);
    }

    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (_saveChangesInterceptors.Any())
        {
            var dbContextEventData = eventData.GetEventData<SpeedBoot.Data.Abstractions.DbContextEventData>();

            dbContextEventData.Result = result.HasResult ? result.Result : null;
            foreach (var saveChangesInterceptor in _saveChangesInterceptors)
                saveChangesInterceptor.SavingChanges(dbContextEventData);
        }

        return result;
    }

    public int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        if (_saveChangesInterceptors.Any())
        {
            var saveChangesCompletedEventData = eventData.GetEventData<Abstractions.SaveChangesCompletedEventData>();
            saveChangesCompletedEventData.Result = result;

            foreach (var saveChangesInterceptor in _saveChangesInterceptors)
            {
                saveChangesInterceptor.SavedChanges(saveChangesCompletedEventData);
            }
        }

        if (_dbContextInterceptors.Any() && eventData.Context is { Database.CurrentTransaction: null })
        {
            var saveSucceedDbContextEventData = eventData.GetEventData<Abstractions.SaveSucceedDbContextEventData>();

            foreach (var dbContextInterceptor in _dbContextInterceptors)
            {
                dbContextInterceptor.SaveSucceed(saveSucceedDbContextEventData);
            }
        }

        return result;
    }

    public void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        if (_saveChangesInterceptors.Any())
        {
            var dbContextErrorEventData = eventData.GetEventData<Abstractions.DbContextErrorEventData>();
            dbContextErrorEventData.Exception = eventData.Exception;
            foreach (var saveChangesInterceptor in _saveChangesInterceptors)
            {
                saveChangesInterceptor.SaveChangesFailed(dbContextErrorEventData);
            }
        }

        if (_dbContextInterceptors.Any() && eventData.Context is { Database.CurrentTransaction: null })
        {
            var saveFailedDbContextEventData = eventData.GetEventData<Abstractions.SaveFailedDbContextEventData>();
            saveFailedDbContextEventData.Exception = eventData.Exception;
            foreach (var dbContextInterceptor in _dbContextInterceptors)
            {
                dbContextInterceptor.SaveFailed(saveFailedDbContextEventData);
            }
        }
    }

    public async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (_saveChangesInterceptors.Any())
        {
            var dbContextEventData = eventData.GetEventData<Abstractions.DbContextEventData>();
            dbContextEventData.Result = result.HasResult ? result.Result : null;
            foreach (var saveChangesInterceptor in _saveChangesInterceptors)
            {
                await saveChangesInterceptor.SavingChangesAsync(dbContextEventData, cancellationToken);
            }
        }

        return result;
    }

    public async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (_saveChangesInterceptors.Any())
        {
            var saveChangesCompletedEventData = eventData.GetEventData<Abstractions.SaveChangesCompletedEventData>();
            saveChangesCompletedEventData.Result = result;
            foreach (var saveChangesInterceptor in _saveChangesInterceptors)
            {
                await saveChangesInterceptor.SavedChangesAsync(saveChangesCompletedEventData, cancellationToken);
            }
        }

        if (_dbContextInterceptors.Any() && eventData.Context is { Database.CurrentTransaction: null })
        {
            var saveSucceedDbContextEventData = eventData.GetEventData<Abstractions.SaveSucceedDbContextEventData>();

            foreach (var dbContextInterceptor in _dbContextInterceptors)
            {
                await dbContextInterceptor.SaveSucceedAsync(saveSucceedDbContextEventData, cancellationToken);
            }
        }

        return result;
    }

    public async Task SaveChangesFailedAsync(
        DbContextErrorEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (_saveChangesInterceptors.Any())
        {
            var dbContextErrorEventData = eventData.GetEventData<Abstractions.DbContextErrorEventData>();
            dbContextErrorEventData.Exception = eventData.Exception;
            foreach (var saveChangesInterceptor in _saveChangesInterceptors)
            {
                await saveChangesInterceptor.SaveChangesFailedAsync(dbContextErrorEventData, cancellationToken);
            }
        }

        if (_dbContextInterceptors.Any() && eventData.Context is { Database.CurrentTransaction: null })
        {
            var saveFailedDbContextEventData = eventData.GetEventData<Abstractions.SaveFailedDbContextEventData>();
            saveFailedDbContextEventData.Exception = eventData.Exception;
            foreach (var dbContextInterceptor in _dbContextInterceptors)
            {
                await dbContextInterceptor.SaveFailedAsync(saveFailedDbContextEventData, cancellationToken);
            }
        }
    }
}

#endif
