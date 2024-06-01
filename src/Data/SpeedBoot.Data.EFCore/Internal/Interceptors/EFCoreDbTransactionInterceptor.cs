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
    private readonly IEnumerable<IDbContextInterceptor> _dbContextInterceptors;

    public EFCoreDbTransactionInterceptor(IServiceProvider serviceProvider)
    {
        _dbTransactionInterceptors = serviceProvider.GetServices<Abstractions.IDbTransactionInterceptor>().OrderBy(i => i.Order);
        _dbContextInterceptors = serviceProvider.GetServices<IDbContextInterceptor>().OrderBy(i => i.Order);
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
        if (_dbTransactionInterceptors.Any())
        {
            var transactionEndEventData = eventData.GetTransactionEventData<TransactionEndEventData>();
            transactionEndEventData.Duration = eventData.Duration;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                dbTransactionInterceptor.TransactionCommitted(transaction, transactionEndEventData);
            }
        }

        if (_dbContextInterceptors.Any())
        {
            var saveSucceedDbContextEventData = eventData.GetEventData<Abstractions.SaveSucceedDbContextEventData>();
            saveSucceedDbContextEventData.DbTransaction = transaction;
            saveSucceedDbContextEventData.ConnectionId = eventData.ConnectionId;
            foreach (var dbContextInterceptor in _dbContextInterceptors)
            {
                dbContextInterceptor.SaveSucceed(saveSucceedDbContextEventData);
            }
        }
    }

    public async Task TransactionCommittedAsync(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionEndEventData = eventData.GetTransactionEventData<TransactionEndEventData>();
            transactionEndEventData.Duration = eventData.Duration;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                await dbTransactionInterceptor.TransactionCommittedAsync(transaction, transactionEndEventData, cancellationToken);
            }
        }

        if (_dbContextInterceptors.Any())
        {
            var saveSucceedDbContextEventData = eventData.GetEventData<Abstractions.SaveSucceedDbContextEventData>();
            saveSucceedDbContextEventData.DbTransaction = transaction;
            saveSucceedDbContextEventData.ConnectionId = eventData.ConnectionId;
            foreach (var dbContextInterceptor in _dbContextInterceptors)
            {
                await dbContextInterceptor.SaveSucceedAsync(saveSucceedDbContextEventData, cancellationToken);
            }
        }
    }

    public InterceptionResult TransactionCommitting(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        InterceptionResult result)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionStartingEventData = eventData.GetTransactionEventData<TransactionStartingEventData>();
            transactionStartingEventData.IsSuppressed = result.IsSuppressed;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                dbTransactionInterceptor.TransactionCommitting(transaction, transactionStartingEventData);
            }
        }

        return result;
    }

    public async ValueTask<InterceptionResult> TransactionCommittingAsync(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        InterceptionResult result,
        CancellationToken cancellationToken = default)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionStartingEventData = eventData.GetTransactionEventData<TransactionStartingEventData>();
            transactionStartingEventData.IsSuppressed = result.IsSuppressed;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                await dbTransactionInterceptor.TransactionCommittingAsync(transaction, transactionStartingEventData, cancellationToken);
            }
        }

        return result;
    }

    public void TransactionFailed(
        DbTransaction transaction,
        TransactionErrorEventData eventData)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionErrorEventData = eventData.GetTransactionEventData<Abstractions.TransactionErrorEventData>();
            transactionErrorEventData.Exception = eventData.Exception;
            transactionErrorEventData.Duration = eventData.Duration;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                dbTransactionInterceptor.TransactionFailed(transaction, transactionErrorEventData);
            }
        }

        if (_dbContextInterceptors.Any())
        {
            var saveFailedDbContextEventData = eventData.GetEventData<Abstractions.SaveFailedDbContextEventData>();
            saveFailedDbContextEventData.DbTransaction = transaction;
            saveFailedDbContextEventData.ConnectionId = eventData.ConnectionId;
            saveFailedDbContextEventData.Exception = eventData.Exception;
            foreach (var dbContextInterceptor in _dbContextInterceptors)
            {
                dbContextInterceptor.SaveFailed(saveFailedDbContextEventData);
            }
        }
    }

    public async Task TransactionFailedAsync(
        DbTransaction transaction,
        TransactionErrorEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionErrorEventData = eventData.GetTransactionEventData<Abstractions.TransactionErrorEventData>();
            transactionErrorEventData.Exception = eventData.Exception;
            transactionErrorEventData.Duration = eventData.Duration;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                await dbTransactionInterceptor.TransactionFailedAsync(transaction, transactionErrorEventData, cancellationToken);
            }
        }

        if (_dbContextInterceptors.Any())
        {
            var saveFailedDbContextEventData = eventData.GetEventData<Abstractions.SaveFailedDbContextEventData>();
            saveFailedDbContextEventData.DbTransaction = transaction;
            saveFailedDbContextEventData.ConnectionId = eventData.ConnectionId;
            saveFailedDbContextEventData.Exception = eventData.Exception;
            foreach (var dbContextInterceptor in _dbContextInterceptors)
            {
                await dbContextInterceptor.SaveFailedAsync(saveFailedDbContextEventData, cancellationToken);
            }
        }
    }

    public void TransactionRolledBack(DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEndEventData eventData)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionEndEventData = eventData.GetTransactionEventData<TransactionEndEventData>();
            transactionEndEventData.Duration = eventData.Duration;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                dbTransactionInterceptor.TransactionRolledBack(transaction, transactionEndEventData);
            }
        }
    }

    public async Task TransactionRolledBackAsync(DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionEndEventData = eventData.GetTransactionEventData<TransactionEndEventData>();
            transactionEndEventData.Duration = eventData.Duration;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                await dbTransactionInterceptor.TransactionRolledBackAsync(transaction, transactionEndEventData, cancellationToken);
            }
        }
    }

    public InterceptionResult TransactionRollingBack(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        InterceptionResult result)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionEndEventData = eventData.GetTransactionEventData<TransactionEventData>();
            transactionEndEventData.IsSuppressed = result.IsSuppressed;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                dbTransactionInterceptor.TransactionRollingBack(transaction, transactionEndEventData);
            }
        }

        return result;
    }

    public async ValueTask<InterceptionResult> TransactionRollingBackAsync(
        DbTransaction transaction,
        Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData,
        InterceptionResult result,
        CancellationToken cancellationToken = default)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionEndEventData = eventData.GetTransactionEventData<TransactionEventData>();
            transactionEndEventData.IsSuppressed = result.IsSuppressed;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                await dbTransactionInterceptor.TransactionRollingBackAsync(transaction, transactionEndEventData, cancellationToken);
            }
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
    private readonly IEnumerable<IDbContextInterceptor> _dbContextInterceptors;

    public EFCoreDbTransactionInterceptor(IServiceProvider serviceProvider)
    {
        _dbTransactionInterceptors =
 serviceProvider.GetServices<SpeedBoot.Data.Abstractions.IDbTransactionInterceptor>().OrderBy(i=> i.Order);
        _dbContextInterceptors = serviceProvider.GetServices<IDbContextInterceptor>().OrderBy(i => i.Order);
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
        if (_dbTransactionInterceptors.Any())
        {
            var transactionStartingEventData =
                eventData.GetTransactionEventData<SpeedBoot.Data.Abstractions.TransactionStartingEventData>();
            transactionStartingEventData.IsSuppressed = result.IsSuppressed;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                dbTransactionInterceptor.TransactionCommitting(transaction, transactionStartingEventData);
            }
        }

        return result;
    }

    public void TransactionCommitted(DbTransaction transaction, TransactionEndEventData eventData)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionEndEventData = eventData.GetTransactionEventData<SpeedBoot.Data.Abstractions.TransactionEndEventData>();
            transactionEndEventData.Duration = eventData.Duration;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                dbTransactionInterceptor.TransactionCommitted(transaction, transactionEndEventData);
            }
        }

        if (_dbContextInterceptors.Any())
        {
            var saveSucceedDbContextEventData = eventData.GetEventData<SpeedBoot.Data.Abstractions.SaveSucceedDbContextEventData>();
            saveSucceedDbContextEventData.DbTransaction = transaction;
            saveSucceedDbContextEventData.ConnectionId = eventData.ConnectionId;
            foreach (var dbContextInterceptor in _dbContextInterceptors)
            {
                dbContextInterceptor.SaveSucceed(saveSucceedDbContextEventData);
            }
        }
    }

    public async Task<InterceptionResult> TransactionCommittingAsync(
        DbTransaction transaction,
        TransactionEventData eventData,
        InterceptionResult result,
        CancellationToken cancellationToken = default)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionStartingEventData =
                eventData.GetTransactionEventData<SpeedBoot.Data.Abstractions.TransactionStartingEventData>();
            transactionStartingEventData.IsSuppressed = result.IsSuppressed;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                await dbTransactionInterceptor.TransactionCommittingAsync(transaction, transactionStartingEventData, cancellationToken);
            }
        }

        return result;
    }

    public async Task TransactionCommittedAsync(
        DbTransaction transaction,
        TransactionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionEndEventData = eventData.GetTransactionEventData<SpeedBoot.Data.Abstractions.TransactionEndEventData>();
            transactionEndEventData.Duration = eventData.Duration;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                await dbTransactionInterceptor.TransactionCommittedAsync(transaction, transactionEndEventData, cancellationToken);
            }
        }

        if (_dbContextInterceptors.Any())
        {
            var saveSucceedDbContextEventData = eventData.GetEventData<SpeedBoot.Data.Abstractions.SaveSucceedDbContextEventData>();
            saveSucceedDbContextEventData.DbTransaction = transaction;
            saveSucceedDbContextEventData.ConnectionId = eventData.ConnectionId;
            foreach (var dbContextInterceptor in _dbContextInterceptors)
            {
                await dbContextInterceptor.SaveSucceedAsync(saveSucceedDbContextEventData, cancellationToken);
            }
        }
    }

    public InterceptionResult TransactionRollingBack(
        DbTransaction transaction,
        TransactionEventData eventData,
        InterceptionResult result)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionEndEventData = eventData.GetTransactionEventData<SpeedBoot.Data.Abstractions.TransactionEventData>();
            transactionEndEventData.IsSuppressed = result.IsSuppressed;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                dbTransactionInterceptor.TransactionRollingBack(transaction, transactionEndEventData);
            }
        }

        return result;
    }

    public void TransactionRolledBack(DbTransaction transaction, TransactionEndEventData eventData)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionEndEventData = eventData.GetTransactionEventData<SpeedBoot.Data.Abstractions.TransactionEndEventData>();
            transactionEndEventData.Duration = eventData.Duration;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                dbTransactionInterceptor.TransactionRolledBack(transaction, transactionEndEventData);
            }
        }
    }

    public Task<InterceptionResult> TransactionRollingBackAsync(
        DbTransaction transaction,
        TransactionEventData eventData,
        InterceptionResult result,
        CancellationToken cancellationToken = default)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionEndEventData = eventData.GetTransactionEventData<SpeedBoot.Data.Abstractions.TransactionEventData>();
            transactionEndEventData.IsSuppressed = result.IsSuppressed;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                dbTransactionInterceptor.TransactionRollingBackAsync(transaction, transactionEndEventData, cancellationToken);
            }
        }

        return Task.FromResult(result);
    }

    public Task TransactionRolledBackAsync(
        DbTransaction transaction,
        TransactionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionEndEventData = eventData.GetTransactionEventData<SpeedBoot.Data.Abstractions.TransactionEndEventData>();
            transactionEndEventData.Duration = eventData.Duration;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                dbTransactionInterceptor.TransactionRolledBackAsync(transaction, transactionEndEventData, cancellationToken);
            }
        }

        return Task.CompletedTask;
    }

    public void TransactionFailed(DbTransaction transaction, TransactionErrorEventData eventData)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionErrorEventData = eventData.GetTransactionEventData<SpeedBoot.Data.Abstractions.TransactionErrorEventData>();
            transactionErrorEventData.Exception = eventData.Exception;
            transactionErrorEventData.Duration = eventData.Duration;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                dbTransactionInterceptor.TransactionFailed(transaction, transactionErrorEventData);
            }
        }

        if (_dbContextInterceptors.Any())
        {
            var saveFailedDbContextEventData = eventData.GetEventData<SpeedBoot.Data.Abstractions.SaveFailedDbContextEventData>();
            saveFailedDbContextEventData.DbTransaction = transaction;
            saveFailedDbContextEventData.ConnectionId = eventData.ConnectionId;
            saveFailedDbContextEventData.Exception = eventData.Exception;
            foreach (var dbContextInterceptor in _dbContextInterceptors)
            {
                dbContextInterceptor.SaveFailed(saveFailedDbContextEventData);
            }
        }
    }

    public async Task TransactionFailedAsync(DbTransaction transaction, TransactionErrorEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (_dbTransactionInterceptors.Any())
        {
            var transactionErrorEventData = eventData.GetTransactionEventData<SpeedBoot.Data.Abstractions.TransactionErrorEventData>();
            transactionErrorEventData.Exception = eventData.Exception;
            transactionErrorEventData.Duration = eventData.Duration;
            foreach (var dbTransactionInterceptor in _dbTransactionInterceptors)
            {
                await dbTransactionInterceptor.TransactionFailedAsync(transaction, transactionErrorEventData, cancellationToken);
            }
        }

        if (_dbContextInterceptors.Any())
        {
            var saveFailedDbContextEventData = eventData.GetEventData<SpeedBoot.Data.Abstractions.SaveFailedDbContextEventData>();
            saveFailedDbContextEventData.DbTransaction = transaction;
            saveFailedDbContextEventData.ConnectionId = eventData.ConnectionId;
            saveFailedDbContextEventData.Exception = eventData.Exception;
            foreach (var dbContextInterceptor in _dbContextInterceptors)
            {
                await dbContextInterceptor.SaveFailedAsync(saveFailedDbContextEventData, cancellationToken);
            }
        }
    }
}
#endif
