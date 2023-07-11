﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public class SpeedBootApplicationExternal
{
    private readonly SpeedBootApplication _speedBootApplication;
    public IServiceCollection Services => _speedBootApplication.Services;
    public IReadOnlyList<Assembly>? Assemblies { get; }
    public IServiceProvider? RootServiceProvider => _speedBootApplication.RootServiceProvider;

    public string? Environment { get; private set; }

    internal SpeedBootApplicationExternal(SpeedBootApplication speedBootApplication, Assembly[]? assemblies, string? environment)
    {
        _speedBootApplication = speedBootApplication;
        Assemblies = assemblies?.ToList();
        Environment = environment;
    }

    public void Initialize() => _speedBootApplication.InitializeComponents();

    public void SetServiceProvider(IServiceProvider rootServiceProvider)
    {
        _speedBootApplication.SetServiceProvider(rootServiceProvider);
    }
}
