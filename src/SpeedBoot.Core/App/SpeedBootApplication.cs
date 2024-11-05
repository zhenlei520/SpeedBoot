// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public class SpeedBootApplication : ISpeedBootApplication
{
    public IServiceCollection Services { get; }

    public IConfiguration? Configuration { get; }

    public string EnvironmentName { get; private set; }

    public IServiceProvider? RootServiceProvider { get; private set; }

    public IList<IAppStartup> Startups { get; }

    public Assembly[] Assemblies { get; }

    public SpeedBootApplication(IServiceCollection services, Assembly[] assemblies, IConfiguration? configuration, string? environmentName)
    {
        Services = services;
        Configuration = configuration;
        EnvironmentName = environmentName ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        Startups = new List<IAppStartup>();
        Assemblies = assemblies;
    }

    public void Shutdown()
    {
    }

    public void SetServiceProvider(IServiceProvider rootServiceProvider)
    {
        RootServiceProvider = rootServiceProvider;
    }

    public void InitializeComponents()
    {
        foreach (var appStartup in Startups.OrderBy(context => context.Order))
        {
            appStartup.Initialize();
        }
    }
}
