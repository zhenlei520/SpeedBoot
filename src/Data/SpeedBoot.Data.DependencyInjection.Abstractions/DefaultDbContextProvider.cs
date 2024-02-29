// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Data;

public class DefaultDbContextProvider : IDbContextProvider
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CustomConcurrentDictionary<Type, object?> _contextDictionary;

    public DefaultDbContextProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _contextDictionary = new();
    }

    public IDbContext? GetDbContext<TDbContext>() where TDbContext : IDbContext
        => GetDbContext(typeof(TDbContext));

    public IDbContext? GetDbContext(Type dbContextType)
    {
        var dbContext = _contextDictionary.GetOrAdd(dbContextType, (type) => _serviceProvider.GetRequiredService(type));
        return dbContext as IDbContext;
    }
}
