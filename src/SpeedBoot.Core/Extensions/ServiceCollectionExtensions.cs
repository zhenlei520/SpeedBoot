// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public static class ServiceCollectionExtensions
{
    public static SpeedBootApplicationBuilder AddSpeed(this IServiceCollection services, Action<SpeedOptions>? configure = null)
    {
        if (!ServiceCollectionUtils.TryAdd<SpeedProvider>(services))
            return services.GetRequiredSingletonInstance<SpeedBootApplicationBuilder>();

        var speedOptions = new SpeedOptions();
        configure?.Invoke(speedOptions);

        var speedBootApplication = new SpeedBootApplication(services);
        if (speedOptions.EnabledServiceRegisterComponent)
        {
            speedBootApplication.AddServiceRegisterComponents(speedOptions.Assemblies);
        }

        var speedBootApplicationExternal = new SpeedBootApplicationBuilder(speedBootApplication, speedOptions.Environment);
        services.AddSingleton(speedBootApplicationExternal);
        services.AddSingleton(speedBootApplication);
        services.AddSingleton<ISpeedBootApplication>(_ => speedBootApplication);

        App.SetApplicationExternal(speedBootApplicationExternal);
        return speedBootApplicationExternal;
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
