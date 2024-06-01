// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET5_0_OR_GREATER

using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TransactionEndEventData = SpeedBoot.Data.Abstractions.TransactionEndEventData;
using TransactionErrorEventData = Microsoft.EntityFrameworkCore.Diagnostics.TransactionErrorEventData;
using TransactionEventData = SpeedBoot.Data.Abstractions.TransactionEventData;
using TransactionStartingEventData = SpeedBoot.Data.Abstractions.TransactionStartingEventData;

namespace SpeedBoot.Data.EFCore;

internal class EFCoreDbTransactionInterceptor : Microsoft.EntityFrameworkCore.Diagnostics.IDbTransactionInterceptor
{
    private readonly IEnumerable<Abstractions.IDbTransactionInterceptor> _dbTransactionInterceptors;

    public EFCoreDbTransactionInterceptor(IServiceProvider serviceProvider)
    {
        _dbTransactionInterceptors = serviceProvider.GetServices<Abstractions.IDbTransactionInterceptor>().OrderBy(i=> i.Order);
    }

    public void CreatedSavepoint(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData)
    {
    }

    public Task CreatedSavepointAsync(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public InterceptionResult CreatingSavepoint(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        InterceptionResult result)
    {
        return result;
    }

    public ValueTask<InterceptionResult> CreatingSavepointAsync(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        InterceptionResult result,
        CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(result);
    }

    public void ReleasedSavepoint(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData)
    {
    }

    public Task ReleasedSavepointAsync(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public InterceptionResult ReleasingSavepoint(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        InterceptionResult result)
    {
        return result;
    }

    public ValueTask<InterceptionResult> ReleasingSavepointAsync(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        InterceptionResult result,
        CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(result);
    }

    public void RolledBackToSavepoint(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData)
    {

    }

    public Task RolledBackToSavepointAsync(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public InterceptionResult RollingBackToSavepoint(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        InterceptionResult result)
    {
        return result;
    }

    public ValueTask<InterceptionResult> RollingBackToSavepointAsync(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        InterceptionResult result,
        CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(result);
    }

    public void TransactionCommitted(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEndEventData eventData)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            dbTransactionInterceptor.TransactionCommitted(transaction, new TransactionEndEventData());
        }
    }

    public async Task TransactionCommittedAsync(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            await dbTransactionInterceptor.TransactionCommittedAsync(transaction, new TransactionEndEventData(), cancellationToken);
        }
    }

    public InterceptionResult TransactionCommitting(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        InterceptionResult result)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            dbTransactionInterceptor.TransactionCommitting(transaction, new TransactionStartingEventData());
        }

        return result;
    }

    public async ValueTask<InterceptionResult> TransactionCommittingAsync(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        InterceptionResult result,
        CancellationToken cancellationToken = default)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            await dbTransactionInterceptor.TransactionCommittingAsync(transaction, new TransactionStartingEventData(), cancellationToken);
        }

        return result;
    }

    public void TransactionFailed(
        DbTransaction transaction,
        TransactionErrorEventData eventData)
    {
    }

    public Task TransactionFailedAsync(
        DbTransaction transaction,
        TransactionErrorEventData eventData,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public void TransactionRolledBack(DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEndEventData eventData)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            dbTransactionInterceptor.TransactionRolledBack(transaction, new TransactionEndEventData());
        }
    }

    public async Task TransactionRolledBackAsync(DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            await dbTransactionInterceptor.TransactionRolledBackAsync(transaction, new TransactionEndEventData(), cancellationToken);
        }
    }

    public InterceptionResult TransactionRollingBack(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        InterceptionResult result)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            dbTransactionInterceptor.TransactionRollingBack(transaction, new TransactionEventData());
        }

        return result;
    }

    public async ValueTask<InterceptionResult> TransactionRollingBackAsync(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        InterceptionResult result,
        CancellationToken cancellationToken = default)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            await dbTransactionInterceptor.TransactionRollingBackAsync(transaction, new TransactionEventData(), cancellationToken);
        }

        return result;
    }

    public DbTransaction TransactionStarted(
        DbConnection connection,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEndEventData eventData,
        DbTransaction result)
    {
        return result;
    }

    public ValueTask<DbTransaction> TransactionStartedAsync(
        DbConnection connection,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEndEventData eventData,
        DbTransaction result,
        CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(result);
    }

    public InterceptionResult<DbTransaction> TransactionStarting(
        DbConnection connection,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionStartingEventData eventData,
        InterceptionResult<DbTransaction> result)
    {
        return result;
    }

    public ValueTask<InterceptionResult<DbTransaction>> TransactionStartingAsync(
        DbConnection connection,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionStartingEventData eventData,
        InterceptionResult<DbTransaction> result,
        CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(result);
    }

    public DbTransaction TransactionUsed(
        DbConnection connection,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        DbTransaction result)
    {
        return result;
    }

    public ValueTask<DbTransaction> TransactionUsedAsync(
        DbConnection connection,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        DbTransaction result,
        CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(result);
    }
}


#elif NETCOREAPP3_0_OR_GREATER

using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TransactionEndEventData = Microsoft.EntityFrameworkCore.Diagnostics.TransactionEndEventData;
using TransactionErrorEventData = Microsoft.EntityFrameworkCore.Diagnostics.TransactionErrorEventData;
using TransactionEventData = Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData;
using TransactionStartingEventData = Microsoft.EntityFrameworkCore.Diagnostics.TransactionStartingEventData;

internal class EFCoreDbTransactionInterceptor : Microsoft.EntityFrameworkCore.Diagnostics.IDbTransactionInterceptor
{
    private readonly IEnumerable<SpeedBoot.Data.Abstractions.IDbTransactionInterceptor> _dbTransactionInterceptors;

    public EFCoreDbTransactionInterceptor(IServiceProvider serviceProvider)
    {
        _dbTransactionInterceptors = serviceProvider.GetServices<SpeedBoot.Data.Abstractions.IDbTransactionInterceptor>().OrderBy(i=> i.Order);
    }

    public InterceptionResult<DbTransaction> TransactionStarting(
        DbConnection connection,
        TransactionStartingEventData eventData,
        InterceptionResult<DbTransaction> result)
    {
        return result;
    }

    public DbTransaction TransactionStarted(
        DbConnection connection,
        TransactionEndEventData eventData,
        DbTransaction result)
    {
        return result;
    }

    public Task<InterceptionResult<DbTransaction>> TransactionStartingAsync(
        DbConnection connection,
        TransactionStartingEventData eventData,
        InterceptionResult<DbTransaction> result,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(result);
    }

    public Task<DbTransaction> TransactionStartedAsync(
        DbConnection connection,
        TransactionEndEventData eventData,
        DbTransaction result,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(result);
    }

    public DbTransaction TransactionUsed(
        DbConnection connection,
        TransactionEventData eventData,
        DbTransaction result)
    {
        return result;
    }

    public Task<DbTransaction> TransactionUsedAsync(
        DbConnection connection,
        TransactionEventData eventData,
        DbTransaction result,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(result);
    }

    public InterceptionResult TransactionCommitting(
        DbTransaction transaction,
        TransactionEventData eventData,
        InterceptionResult result)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            dbTransactionInterceptor.TransactionCommitting(transaction, new SpeedBoot.Data.Abstractions.TransactionStartingEventData());
        }

        return result;
    }

    public void TransactionCommitted(DbTransaction transaction, TransactionEndEventData eventData)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            dbTransactionInterceptor.TransactionCommitted(transaction, new SpeedBoot.Data.Abstractions.TransactionEndEventData());
        }
    }

    public Task<InterceptionResult> TransactionCommittingAsync(
        DbTransaction transaction,
        TransactionEventData eventData,
        InterceptionResult result,
        CancellationToken cancellationToken = default)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            dbTransactionInterceptor.TransactionCommittingAsync(transaction, new SpeedBoot.Data.Abstractions.TransactionStartingEventData(), cancellationToken);
        }

        return Task.FromResult(result);
    }

    public Task TransactionCommittedAsync(
        DbTransaction transaction,
        TransactionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            dbTransactionInterceptor.TransactionCommittedAsync(transaction, new SpeedBoot.Data.Abstractions.TransactionEndEventData(), cancellationToken);
        }

        return Task.CompletedTask;
    }

    public InterceptionResult TransactionRollingBack(
        DbTransaction transaction,
        TransactionEventData eventData,
        InterceptionResult result)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            dbTransactionInterceptor.TransactionRollingBack(transaction, new SpeedBoot.Data.Abstractions.TransactionEventData());
        }

        return result;
    }

    public void TransactionRolledBack(DbTransaction transaction, TransactionEndEventData eventData)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            dbTransactionInterceptor.TransactionRolledBack(transaction, new SpeedBoot.Data.Abstractions.TransactionEndEventData());
        }
    }

    public Task<InterceptionResult> TransactionRollingBackAsync(
        DbTransaction transaction,
        TransactionEventData eventData,
        InterceptionResult result,
        CancellationToken cancellationToken = default)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            dbTransactionInterceptor.TransactionRollingBackAsync(transaction, new SpeedBoot.Data.Abstractions.TransactionEventData(), cancellationToken);
        }

        return Task.FromResult(result);
    }

    public Task TransactionRolledBackAsync(
        DbTransaction transaction,
        TransactionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
        {
            dbTransactionInterceptor.TransactionRolledBackAsync(transaction, new SpeedBoot.Data.Abstractions.TransactionEndEventData(), cancellationToken);
        }

        return Task.CompletedTask;
    }

    public void TransactionFailed(DbTransaction transaction, TransactionErrorEventData eventData)
    {
    }

    public Task TransactionFailedAsync(DbTransaction transaction, TransactionErrorEventData eventData,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
#endif
