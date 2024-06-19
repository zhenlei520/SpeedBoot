// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql.Tests.Components.Interceptors;

public class SaveChangesInterceptor : ISaveChangesInterceptor
{
    public int Order { get; } = 999;

    public void SavingChanges(DbContextEventData dbContextEventData)
    {
    }

    public Task SavingChangesAsync(DbContextEventData dbContextEventData, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public void SavedChanges(SaveChangesCompletedEventData eventData)
    {
    }

    public Task SavedChangesAsync(SaveChangesCompletedEventData eventData, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public void SaveChangesFailed(DbContextErrorEventData eventData)
    {
    }

    public Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
