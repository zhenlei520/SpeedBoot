// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NETCOREAPP3_0_OR_GREATER

namespace Microsoft.EntityFrameworkCore;

internal static class TransactionEventDataExtensions
{
    public static TTransactionEventData GetTransactionEventData<TTransactionEventData>(this Microsoft.EntityFrameworkCore.Diagnostics.TransactionEventData eventData)
        where TTransactionEventData : TransactionEventDataBase, new()
    {
        var transactionEventData = eventData.GetEventData<TTransactionEventData>();
        transactionEventData.ConnectionId = eventData.ConnectionId;
        return transactionEventData;
    }
}

#endif
