// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public class App
{
    public static App Instance = new();

    private SpeedBootApplication _speedBootApplication;

    public IServiceProvider? RootServiceProvider => _speedBootApplication.RootServiceProvider;

    public IServiceCollection Services => _speedBootApplication.Services;

    public Func<IServiceCollection, IServiceProvider>? RebuildRootServiceProvider { get; set; }

    public void SetSpeedBootApplication(SpeedBootApplication speedBootApplication)
    {
        _speedBootApplication = speedBootApplication;
    }

    public void SetRootServiceProvider(IServiceProvider rootServiceProvider)
    {
        _speedBootApplication.SetServiceProvider(rootServiceProvider);
    }

    public SpeedBootApplication GetSpeedBootApplication() => _speedBootApplication;
}
