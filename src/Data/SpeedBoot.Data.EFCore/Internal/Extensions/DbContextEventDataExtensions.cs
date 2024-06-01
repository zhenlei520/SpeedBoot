// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NETCOREAPP3_0_OR_GREATER
namespace Microsoft.EntityFrameworkCore;

internal static class DbContextEventDataExtensions
{
    public static TEventData GetEventData<TEventData>(this Microsoft.EntityFrameworkCore.Diagnostics.DbContextEventData dbContextEventData)
        where TEventData : DbContextEventDataBase, new()
    {
        return new TEventData()
        {
            EventId = dbContextEventData.EventId.Id.ToString(),
            EventName = dbContextEventData.EventId.Name,
            ContextId = dbContextEventData.Context.ContextId.ToString(),
            Entites = dbContextEventData.Context.ChangeTracker.GetEntites()
        };
    }
}
#endif
