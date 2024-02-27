// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER

using SpeedBoot.AspNetCore.Internal;
using SpeedBoot.AspNetCore.Options;

namespace SpeedBoot.AspNetCore;

public class MinimalAPIsStartup : AppStartupBase
{
    private readonly IServiceCollection _services;
    private readonly List<Type> _allServiceTypes;
    private readonly GlobalServiceRouteOptions _globalServiceRouteOptions;
    public override int Order => int.MaxValue;

    public override string Name => "MinimalAPIsRegisterComponent";

    public MinimalAPIsStartup(
        IServiceCollection services,
        IEnumerable<Assembly> assemblies,
        GlobalServiceRouteOptions globalServiceRouteOptions,
        ILogger? logger = null,
        LogLevel? logLevel = null)
        : base(logger, logLevel)
    {
        _globalServiceRouteOptions = globalServiceRouteOptions;
        _services = services;
        _allServiceTypes = AssemblyHelper.GetServiceTypes(assemblies).ToList();
    }

    protected override void Load()
    {
        foreach (var serviceType in _allServiceTypes)
        {
            var service = _services.BuildServiceProvider().GetRequiredService(serviceType);

            var serviceBase = (ServiceBase)service;
            if (serviceBase.RouteOptions.DisableAutoMapRoute ?? _globalServiceRouteOptions.DisableAutoMapRoute ?? false)
                continue;

            serviceBase.AutoMapRoute(_globalServiceRouteOptions);
        }
    }
}

#endif
