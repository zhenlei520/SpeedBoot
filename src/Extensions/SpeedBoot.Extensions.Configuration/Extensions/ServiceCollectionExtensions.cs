// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register Configuration Services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        if (!ServiceCollectionUtils.TryAdd<ConfigurationProvider>(services))
            return services;

        services.TryAddSingleton(configuration);

        return services;
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private sealed class ConfigurationProvider
    {

    }
}
