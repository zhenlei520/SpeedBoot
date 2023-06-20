// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed;

public static class ServiceCollectionExtensions
{
    private static readonly List<object> AppStartupContextList = new();

    public static IServiceCollection AddSpeed(this IServiceCollection services)
    {
        services.AddServiceComponent();
        foreach (var appStartup in AppStartupContextList)
        {
            if (appStartup is IAppStartup serviceStartup)
            {
                serviceStartup.Initialized();
            }
        }
        return services;
    }

    private static IServiceCollection AddServiceComponent(this IServiceCollection services)
    {
        var assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
        var assemblies = assemblyNames.Select(Assembly.Load).ToArray();
        return services.AddServiceComponent(assemblies);
    }

    public static IServiceCollection AddServiceComponent(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (services.Any(service => service.ImplementationType == typeof(ServiceComponentProvider)))
            return services;

        services.AddSingleton<ServiceComponentProvider>();
        AppStartupContextList.Add(new ServiceCollectionStartup(services, assemblies, null));
        return services;
    }

    private class ServiceComponentProvider
    {
    }
}
