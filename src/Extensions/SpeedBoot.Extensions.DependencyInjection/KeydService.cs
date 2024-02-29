// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

public class KeydService<TService> : IKeydService<TService> where TService : IService
{
    private Dictionary<string, TService>? _services;
    private readonly IServiceProvider _serviceProvider;

    public KeydService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TService? GetService(string key)
    {
        _services ??= _serviceProvider.GetServices<TService>().ToDictionary(s => s.Key, s => s);
        _services.TryGetValue(key, out TService? service);
        return service;
    }

    public TService GetRequiredService(string key)
    {
        var service = GetService(key);
        SpeedArgumentException.ThrowIfNull(service);
        return service;
    }
}
