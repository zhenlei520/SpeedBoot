// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.Abstractions;

public interface IDbTransactionInterceptor: IOrder
{
    void TransactionCommitting(DbTransaction transaction, TransactionStartingEventData eventData);

    Task TransactionCommittingAsync(DbTransaction transaction, TransactionStartingEventData eventData, CancellationToken cancellationToken = default);

    void TransactionCommitted(DbTransaction transaction, TransactionEndEventData eventData);

    Task TransactionCommittedAsync(DbTransaction transaction, TransactionEndEventData eventData, CancellationToken cancellationToken = default);

    void TransactionRollingBack(DbTransaction transaction, TransactionEventData eventData);

    Task TransactionRollingBackAsync(DbTransaction transaction, TransactionEventData eventData, CancellationToken cancellationToken = default);

    void TransactionRolledBack(DbTransaction transaction,TransactionEndEventData eventData);

    Task TransactionRolledBackAsync(DbTransaction transaction,TransactionEndEventData eventData, CancellationToken cancellationToken = default);
}
