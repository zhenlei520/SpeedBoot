// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.Abstractions;

public interface ISaveChangesInterceptor : IOrder
{
    void SavingChanges(DbContextEventData dbContextEventData);

    Task SavingChangesAsync(DbContextEventData dbContextEventData, CancellationToken cancellationToken = default);

    void SavedChanges(SaveChangesCompletedEventData eventData);

    Task SavedChangesAsync(SaveChangesCompletedEventData eventData, CancellationToken cancellationToken = default);

    void SaveChangesFailed(DbContextErrorEventData eventData);

    Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default);

    void SaveChangesCanceled(DbContextEventData dbContextEventData);

    Task SaveChangesCanceledAsync(DbContextEventData dbContextEventData, CancellationToken cancellationToken = default);
}
