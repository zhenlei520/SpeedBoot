// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace FreeSql.Aop;

internal static class EntityChangeTypeExtensions
{
    public static EntityState? GetEntityState(this DbContext.EntityChangeType entityChangeType)
    {
        return entityChangeType switch
        {
            DbContext.EntityChangeType.Insert => EntityState.Added,
            DbContext.EntityChangeType.Update => EntityState.Modified,
            DbContext.EntityChangeType.Delete => EntityState.Deleted,
            DbContext.EntityChangeType.SqlRaw => null,
            _ => null
        };
    }
}
