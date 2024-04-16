// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoInject(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies,
        LazyThreadSafetyMode mode = LazyThreadSafetyMode.ExecutionAndPublication)
        => services.AddAutoInjectCore(assemblies, mode);

    public static IServiceCollection AddAutoInject(this IServiceCollection services, params Assembly[] assemblies)
        => services.AddAutoInjectCore(assemblies, LazyThreadSafetyMode.ExecutionAndPublication);

    private static IServiceCollection AddAutoInjectCore(this IServiceCollection services,
        IEnumerable<Assembly> assemblies,
        LazyThreadSafetyMode mode)
    {
        if (!ServiceCollectionUtils.TryAdd<DependencyInjectionService>(services))
            return services;

        App.Instance.RebuildRootServiceProvider ??= s => s.BuildServiceProvider();

        var autoInjectProvider = new DefaultAutoInjectProvider(assemblies);
        var serviceDescriptors = autoInjectProvider.GetServiceDescriptors();
        foreach (var serviceDescriptor in serviceDescriptors)
        {
            services.AddLazyService(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationType, serviceDescriptor.Lifetime, mode);
            services.Add(new ServiceDescriptor(
                serviceDescriptor.ServiceType,
                serviceDescriptor.ImplementationType,
                serviceDescriptor.Lifetime));
        }

        services.TryAddSingleton(typeof(IKeydSingletonService<>), typeof(KeydSingletonService<>));
        services.TryAddTransient(typeof(IKeydScopedService<>), typeof(KeydScopedService<>));
        services.TryAddTransient(typeof(IKeydService<>), typeof(KeydService<>));
        return services;
    }

    private sealed class DependencyInjectionService
    {
    }
}
