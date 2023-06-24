// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System;

internal static class ServiceCollectionUtils
{
    public static bool TryAdd<TService>(IServiceCollection services) where TService : class
    {
        if (services.Any(service => service.ImplementationType == typeof(TService)))
            return false;

        services.AddSingleton<TService>();
        return true;
    }
}
