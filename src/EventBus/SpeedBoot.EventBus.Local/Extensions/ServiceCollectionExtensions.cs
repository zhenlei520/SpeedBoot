// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLocalEventBus(this IServiceCollection services, Action<LocalEventBusOptions>? action = null)
    {
        if (!ServiceCollectionUtils.TryAdd<LocalEventBusProvider>(services))
            return services;

        var localEventBusOptions = new LocalEventBusOptions();
        action?.Invoke(localEventBusOptions);
        SpeedArgumentException.ThrowIfGreaterThan(localEventBusOptions.HandlerInstanceLifetime, localEventBusOptions.EventBusLifetime);

        services.AddSingleton<ILocalEventBusMesh>(sp => new LocalEventBusMesh(localEventBusOptions, sp.GetService<ILogger>()));
        services.TryRegisterHandlerInstanceType(localEventBusOptions.HandlerInstanceLifetime);
        services.TryRegisterEventBusActionFilter(localEventBusOptions.GetAssemblies(), localEventBusOptions.HandlerInstanceLifetime);
        services.Add(new ServiceDescriptor(typeof(ILocalEventBus), typeof(LocalEventBus), localEventBusOptions.EventBusLifetime));
        services.TryAdd(new ServiceDescriptor(typeof(IEventBus), typeof(EventBus), localEventBusOptions.EventBusLifetime));
        services.AddScoped<LocalEventExecuteContext>();
        services.AddScoped<IStrategyExecutor, StrategyExecutor>();
        return services;
    }

    private static void TryRegisterHandlerInstanceType(this IServiceCollection services, ServiceLifetime handlerInstanceLifetime)
    {
        var serviceProvider = App.Instance.RebuildRootServiceProvider?.Invoke(services) ??
            throw new ArgumentNullException("App.Instance.RebuildRootServiceProvider");
        var localEventBusMesh = serviceProvider.GetRequiredService<ILocalEventBusMesh>();
        var instanceTypes = localEventBusMesh.MeshData.SelectMany(item =>
        {
            return item.Value.Handlers.Select(handler => handler.InstanceType)
                .Concat(item.Value.CancelHandlers.Select(cancelHandler => cancelHandler.InstanceType));
        });
        foreach (var instanceType in instanceTypes)
        {
            services.Add(new ServiceDescriptor(instanceType, instanceType, handlerInstanceLifetime));
        }
    }

    private static void TryRegisterEventBusActionFilter(this IServiceCollection services, Assembly[] assemblies, ServiceLifetime handlerInstanceLifetime)
    {
        var eventBusInterceptorTypes = GetEventBusInterceptorTypes().ToList();
        foreach (var providerType in eventBusInterceptorTypes)
        {
            services.Add(new ServiceDescriptor(providerType, providerType, handlerInstanceLifetime));
        }

        IEnumerable<Type> GetEventBusInterceptorTypes()
            => from type in assemblies.SelectMany(assembly => assembly.GetTypes())
                where type.IsClass && !type.IsAbstract && typeof(IEventBusActionFilterProvider).IsAssignableFrom(type)
                select type;
    }

    private sealed class LocalEventBusProvider
    {
    }
}
