// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace FreeSql.Aop;

internal static class CurdTypeExtensions
{
    public static EntityState GetEntityState(this CurdType curdType)
    {
        return curdType switch
        {
            CurdType.Insert => EntityState.Added,
            CurdType.Update or CurdType.InsertOrUpdate => EntityState.Modified,
            CurdType.Delete => EntityState.Deleted,
            _ => throw new NotSupportedException()
        };
    }
}
