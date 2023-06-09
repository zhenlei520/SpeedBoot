// Copyright (c) zhenlei520 All rights reserved.
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
        if (services.Any<DependencyInjectionService>())
            return services;

        services.AddSingleton<DependencyInjectionService>();

        var allType = assemblies.GetTypes();


        // services.TryAddSingleton<IServiceRegister, DefaultServiceRegister>();
        // services.TryAddSingleton<ITypeProvider, DefaultTypeProvider>();
        // var typeProvider = services.GetInstance<ITypeProvider>();
        // var serviceDescriptors = typeProvider.GetServiceDescriptors(typeProvider.GetAllTypes(assemblies));
        //
        // var registrar = services.GetInstance<IServiceRegister>();
        // foreach (var descriptor in serviceDescriptors)
        //     registrar.Add(services, descriptor.ServiceType, descriptor.ImplementationType, descriptor.Lifetime);

        // if (!serviceDescriptors.Any(d => d.AutoFire))
        //     return services;
        //
        // var serviceProvider = services.BuildServiceProvider();
        // foreach (var descriptor in serviceDescriptors.Where(d => d.AutoFire))
        //     serviceProvider.GetService(descriptor.ServiceType);

        return services;
    }

    private sealed class DependencyInjectionService
    {

    }
}
