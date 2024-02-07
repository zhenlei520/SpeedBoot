// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public class SpeedBootApplication : ISpeedBootApplication
{
    public IServiceCollection Services { get; }

    public IServiceProvider? RootServiceProvider { get; private set; }

    public IList<IAppStartup> Startups { get; }

    public SpeedBootApplication(IServiceCollection services)
    {
        Services = services;
        Startups = new List<IAppStartup>();
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
