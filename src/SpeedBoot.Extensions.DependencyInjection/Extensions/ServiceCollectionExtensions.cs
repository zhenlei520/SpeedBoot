﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoInject(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        => services.AddAutoInjectCore(assemblies);

    public static IServiceCollection AddAutoInject(this IServiceCollection services, params Assembly[] assemblies)
        => services.AddAutoInjectCore(assemblies);

    private static IServiceCollection AddAutoInjectCore(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        if (!ServiceCollectionUtils.TryAdd<DependencyInjectionService>(services))
            return services;

        var autoInjectProvider = new DefaultAutoInjectProvider(assemblies);
        var serviceDescriptors = autoInjectProvider.GetServiceDescriptors();
        foreach (var serviceDescriptor in serviceDescriptors)
        {
            services.Add(new ServiceDescriptor(
                serviceDescriptor.ServiceType,
                serviceDescriptor.ImplementationType,
                serviceDescriptor.Lifetime));
        }

        return services;
    }

    private sealed class DependencyInjectionService
    {
    }
}