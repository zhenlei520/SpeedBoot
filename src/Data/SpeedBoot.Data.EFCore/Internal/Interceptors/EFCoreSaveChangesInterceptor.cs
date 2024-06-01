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

    public EFCoreSaveChangesInterceptor(IServiceProvider serviceProvider)
    {
        _saveChangesInterceptors = serviceProvider.GetServices<ISaveChangesInterceptor>().OrderBy(i => i.Order);
    }

    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        foreach (var saveChangesInterceptor in _saveChangesInterceptors)
            saveChangesInterceptor.SavingChanges(new Abstractions.DbContextEventData());
        return result;
    }

    public int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        foreach (var saveChangesInterceptor in _saveChangesInterceptors)
        {
            saveChangesInterceptor.SavedChanges(new Abstractions.SaveChangesCompletedEventData());
        }

        return result;
    }

    public void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        foreach (var saveChangesInterceptor in _saveChangesInterceptors)
        {
            saveChangesInterceptor.SaveChangesFailed(new Abstractions.DbContextErrorEventData());
        }
    }

    public async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        foreach (var saveChangesInterceptor in _saveChangesInterceptors)
        {
            await saveChangesInterceptor.SavingChangesAsync(new Abstractions.DbContextEventData(), cancellationToken);
        }

        return result;
    }

    public async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        foreach (var saveChangesInterceptor in _saveChangesInterceptors)
        {
            await saveChangesInterceptor.SavedChangesAsync(new Abstractions.SaveChangesCompletedEventData(), cancellationToken);
        }
        return result;
    }

    public async Task SaveChangesFailedAsync(
        DbContextErrorEventData eventData,
        CancellationToken cancellationToken = default)
    {
        foreach (var saveChangesInterceptor in _saveChangesInterceptors)
        {
            await saveChangesInterceptor.SaveChangesFailedAsync(new Abstractions.DbContextErrorEventData(), cancellationToken);
        }
    }
}

#endif
