// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed.Boot.Data.FreeSql;

internal class DefaultTableRelationProvider : ITableRelationProvider
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IDbContextProvider _dbContextProvider;

    private readonly CustomConcurrentDictionary<Type, TableInfo> _data = new();

    public DefaultTableRelationProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _dbContextProvider = serviceProvider.GetRequiredService<IDbContextProvider>();
    }

    SpeedDbContext GetDbContext(Type dbContextType)
    {
        var speedDbContext = _dbContextProvider.GetDbContext(dbContextType) as SpeedDbContext;
        SpeedArgumentException.ThrowIfNull(speedDbContext);
        return speedDbContext;
    }

    public TableInfo GetTableByEntity<TEntity>(Type dbContextType)
        where TEntity : IEntity
    {
        return _data.GetOrAdd(typeof(TEntity), (type =>
        {
            var codeFirst = GetDbContext(dbContextType).FreeSql.CodeFirst;
            return codeFirst.GetTableByEntity(type);
        }));
    }

    public string[] GetKeys<TEntity>(Type dbContextType)
        where TEntity : IEntity
    {
        return GetTableByEntity<TEntity>(dbContextType).Primarys.Select(p => p.CsName).ToArray();
    }
}
