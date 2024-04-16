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
        services.AddSingleton<ILocalEventBusMesh>(sp => new LocalEventBusMesh(localEventBusOptions, sp.GetService<ILogger>()));
        services.TryRegisterHandlerInstanceType(localEventBusOptions.HandlerInstanceLifetime);
        services.Add(new ServiceDescriptor(typeof(ILocalEventBus), typeof(LocalEventBus), localEventBusOptions.EventbusLifetime));

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

    private sealed class LocalEventBusProvider
    {
    }
}
