// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.Abstractions;

public interface IDbTransactionInterceptor
{
    void TransactionCommitting(DbTransaction transaction, TransactionStartingEventData eventData);

    Task TransactionCommittingAsync(DbTransaction transaction, TransactionStartingEventData eventData);

    void TransactionCommitted(DbTransaction transaction, TransactionEndEventData eventData);

    Task TransactionCommittedAsync(DbTransaction transaction, TransactionEndEventData eventData);

    void TransactionRollingBack(DbTransaction transaction, TransactionEventData eventData);

    Task TransactionRollingBackAsync(DbTransaction transaction, TransactionEventData eventData);

    void TransactionRolledBack(DbTransaction transaction,TransactionEndEventData eventData);

    Task TransactionRolledBackAsync(DbTransaction transaction,TransactionEndEventData eventData);
}
