﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public class ServiceRegisterStartup : AppStartupBase
{
    private readonly IServiceCollection _services;
    private readonly List<Type> _allServiceComponentTypes;

    public override string Name => "ServiceRegisterComponent";

    public ServiceRegisterStartup(
        IServiceCollection services,
        IEnumerable<Assembly> assemblies,
        ILogger? logger = null,
        LogLevel? logLevel = null) : base(logger, logLevel)
    {
        _services = services;
        _allServiceComponentTypes = assemblies.GetTypes(type => type is { IsClass: true, IsGenericType: false, IsAbstract: false } && typeof(IServiceComponent).IsAssignableFrom(type));
    }

    protected override void Load()
    {
        var componentTypes = ServiceComponentStartupHelp.GetComponentTypesByOrdered(_allServiceComponentTypes);
        foreach (var componentInstance in componentTypes.Select(componentType => Activator.CreateInstance(componentType) as IServiceComponent))
        {
            SpeedArgumentException.ThrowIfNull(componentInstance);
            componentInstance!.ConfigureServices(_services);
        }
    }
}
