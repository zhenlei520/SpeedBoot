// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpeed(this IServiceCollection services, Action<SpeedOptions>? configure = null)
    {
        if (!ServiceCollectionUtils.TryAdd<SpeedProvider>(services))
            return services;

        var speedOptions = new SpeedOptions();
        configure?.Invoke(speedOptions);

        var assemblies = GetValidAssemblies();

        Initialized();
        AddDefaultServiceComponents();
        InitializedStartup();

        void Initialized()
        {
            InternalApp.ConfigureServices(services);
            InternalApp.ConfigureAssemblies(assemblies);
            InternalApp.ConfigureEnvironment(speedOptions.Environment);
        }

        Assembly[] GetValidAssemblies()
        {
            Expression<Func<string, bool>> condition = name => true;
            var assemblyNames = speedOptions.GetEffectAssemblyNames();
            condition = condition.And(
                assemblyNames.Count > 0,
                name => assemblyNames.Any(n => Regex.Match(name, n).Success));
            return AssemblyUtils.GetAllAssembly(condition);
        }

        void AddDefaultServiceComponents()
        {
            if (!speedOptions.EnabledServiceComponent)
                return;

            services.AddServiceComponents(assemblies);
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
        if (!ServiceCollectionUtils.TryAdd<ServiceComponentProvider>(services))
            return services;

        InternalApp.AppStartupContextList.Add(new ServiceCollectionStartup(services, assemblies, null));
        return services;
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private sealed class ServiceComponentProvider
    {
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private sealed class SpeedProvider
    {

    }
}
