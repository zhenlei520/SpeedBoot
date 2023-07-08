// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public static class ServiceCollectionExtensions
{
    public static SpeedBootApplicationExternal AddSpeed(this IServiceCollection services, Action<SpeedOptions>? configure = null)
    {
        if (!ServiceCollectionUtils.TryAdd<SpeedProvider>(services))
            return services.GetRequiredSingletonInstance<SpeedBootApplicationExternal>();

        var speedOptions = new SpeedOptions();
        configure?.Invoke(speedOptions);

        var speedBootApplication = new SpeedBootApplication(services);
        var assemblies = GetValidAssemblies();
        speedBootApplication.AddServiceRegisterComponents(assemblies);
        var speedBootApplicationExternal = new SpeedBootApplicationExternal(speedBootApplication, assemblies);
        services.AddSingleton(speedBootApplicationExternal);
        services.AddSingleton(speedBootApplication);
        services.AddSingleton<ISpeedBootApplication>(_ => speedBootApplication);

        App.SetApplicationExternal(speedBootApplicationExternal);
        return speedBootApplicationExternal;

        Assembly[] GetValidAssemblies()
        {
            Expression<Func<string, bool>> condition = name => true;
            var includeAssemblyRules = speedOptions.GetIncludeAssemblyRules();
            var excludeAssemblyRules = speedOptions.GetExcludeAssemblyRules();
            condition = condition.And(
                includeAssemblyRules.Count > 0 || excludeAssemblyRules.Count > 0,
                assemblyName =>
                    includeAssemblyRules.Any(n => Regex.Match(assemblyName, n).Success) &&
                    !excludeAssemblyRules.Any(n => Regex.Match(assemblyName, n).Success));
            return AssemblyUtils.GetAllAssembly(condition);
        }
    }

    public static TInstance? GetSingletonInstance<TInstance>(this IServiceCollection services) where TInstance : class
        => services.Where(d => d.ServiceType == typeof(TInstance)).Select(d => d.ImplementationInstance as TInstance).FirstOrDefault();

    public static TInstance GetRequiredSingletonInstance<TInstance>(this IServiceCollection services) where TInstance : class
        => services.GetSingletonInstance<TInstance>() ??
            throw new SpeedException($"Could not find an object of {typeof(TInstance).AssemblyQualifiedName} in services");

    // ReSharper disable once ClassNeverInstantiated.Local
    private sealed class SpeedProvider
    {

    }
}
