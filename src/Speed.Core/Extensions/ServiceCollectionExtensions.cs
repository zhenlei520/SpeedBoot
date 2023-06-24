// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpeed(this IServiceCollection services, Action<SpeedOptions>? configure = null)
    {
        var speedOptions = new SpeedOptions();
        configure?.Invoke(speedOptions);
        AddDefaultServiceComponents();
        InitializedStartup();

        IServiceCollection AddDefaultServiceComponents()
        {
            if (!speedOptions.EnabledServiceComponent)
                return services;

            Expression<Func<string, bool>> condition = name => true;
            condition = condition.And(
                !speedOptions.AssemblyName.IsNullOrWhiteSpace(),
                name => Regex.Match(name, speedOptions.AssemblyName).Success);

            return services.AddServiceComponents(AssemblyUtils.GetAllAssembly(condition));
        }

        void InitializedStartup()
        {
            foreach (var appStartup in InternalApp.AppStartupContextList.OrderBy(context => context.Order))
            {
                appStartup.Initialized();
            }
        }

        return services;
    }

    /// <summary>
    /// Add service components
    /// </summary>
    /// <param name="services">collection of services</param>
    /// <param name="assemblies">assembly collection</param>
    /// <returns></returns>
    public static IServiceCollection AddServiceComponents(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (services.Any(service => service.ImplementationType == typeof(ServiceComponentProvider)))
            return services;

        services.AddSingleton<ServiceComponentProvider>();

        InternalApp.AppStartupContextList.Add(new ServiceCollectionStartup(services, assemblies, null));
        return services;
    }

    private class ServiceComponentProvider
    {
    }
}
