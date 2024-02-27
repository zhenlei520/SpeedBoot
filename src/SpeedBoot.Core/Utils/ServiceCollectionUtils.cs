// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Core;

public static class ServiceCollectionUtils
{
    public static bool TryAdd<TService>(IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Singleton) where TService : class
    {
        if (services.Any(service => service.ImplementationType == typeof(TService)))
            return false;

        services.Add(ServiceDescriptor.Describe(typeof(TService),typeof(TService),lifetime));
        return true;
    }
}
