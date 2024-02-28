// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLazyService(
        this IServiceCollection services,
        Type serviceType,
        Type implementationInstanceType,
        ServiceLifetime lifetime,
        LazyThreadSafetyMode mode = LazyThreadSafetyMode.ExecutionAndPublication)
    {
        var constructor = implementationInstanceType.GetConstructors().OrderByDescending(c => c.GetParameters().Length).FirstOrDefault()!;
        SpeedArgumentException.ThrowIfNull(constructor);
        var parameterTypes = constructor.GetParameters().Select(p => p.ParameterType).ToList();
        return services.AddLazyService(serviceType, serviceProvider =>
        {
            if (parameterTypes.Count > 0)
            {
                Activator.CreateInstance(implementationInstanceType, parameterTypes.Select(serviceProvider.GetService).ToArray());
            }

            return Activator.CreateInstance(implementationInstanceType)!;
        }, lifetime, mode);
    }

    public static IServiceCollection AddLazyService<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, object> implementationFactory,
        ServiceLifetime lifetime,
        LazyThreadSafetyMode mode = LazyThreadSafetyMode.ExecutionAndPublication)
    {
        return services.AddLazyService(typeof(TService), implementationFactory, lifetime, mode);
    }

    public static IServiceCollection AddLazyService(
        this IServiceCollection services,
        Type serviceType,
        Func<IServiceProvider, object> implementationFactory,
        ServiceLifetime lifetime,
        LazyThreadSafetyMode mode = LazyThreadSafetyMode.ExecutionAndPublication)
    {
        var lazyServiceType = typeof(Lazy<>).MakeGenericType(serviceType);
        var lazyImplementationFactory = new Lazy<Func<IServiceProvider, object>>(() => implementationFactory, mode);
        var serviceDescriptor = new ServiceDescriptor(lazyServiceType,
            serviceProvider => lazyImplementationFactory.Value.Invoke(serviceProvider), lifetime);
        services.Add(serviceDescriptor);
        return services;
    }

    public static IServiceCollection AddLazySingletonService<TService>(
        this IServiceCollection services,
        TService implementationInstance,
        LazyThreadSafetyMode mode = LazyThreadSafetyMode.ExecutionAndPublication)
    {
        var serviceDescriptor = ServiceDescriptor.Singleton(new Lazy<TService>(() => implementationInstance, mode));
        services.Add(serviceDescriptor);
        return services;
    }

}
