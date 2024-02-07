// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Data.FreeSql;

internal interface ITableRelationProvider
{
    TableInfo GetTableByEntity<TEntity>(Type dbContextType)
        where TEntity : IEntity;

    string[] GetKeys<TEntity>(Type dbContextType)
        where TEntity : IEntity;
}
