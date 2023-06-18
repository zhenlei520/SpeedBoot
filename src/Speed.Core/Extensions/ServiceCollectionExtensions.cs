// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceComponent(this IServiceCollection services)
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
        var serviceComponentTypes = AssemblyUtils.GetServiceComponentTypes(assemblies);
        foreach (var serviceComponentType in serviceComponentTypes)
        {
            var serviceComponent = Activator.CreateInstance(serviceComponentType);
            if (serviceComponent == null)
                continue;

            if (serviceComponent is ServiceComponentBase serviceComponentBase)
            {
                if (serviceComponentBase.DependComponentTypes.Count == 0)
                {

                }
            }
            else
            {

            }
        }

        return services;
    }

    public static IServiceCollection AddServiceComponent<TServiceComponent>(this IServiceCollection services)
        where TServiceComponent : IServiceComponent, new()
    {

        return services;
    }

    private class ServiceComponentProvider
    {

    }
}
