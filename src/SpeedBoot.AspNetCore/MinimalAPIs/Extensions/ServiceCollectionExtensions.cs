// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER
using Microsoft.AspNetCore.Builder;
using SpeedBoot.AspNetCore.Internal;
using SpeedBoot.AspNetCore.Options;

namespace SpeedBoot.AspNetCore;

public static class ServiceCollectionExtensions
{
    public static WebApplication AddMinimalAPIs(
        this SpeedBootApplication application,
        WebApplicationBuilder builder,
        Action<GlobalServiceRouteOptions> action)
    {
        if (!ServiceCollectionUtils.TryAdd<ConfigurationProvider>(application.Services))
            return App.Instance.GetRequiredSingletonService<WebApplication>();

        application.Services
            .AddHttpContextAccessor()
            .AddSingleton(new Lazy<WebApplication>(builder.Build, LazyThreadSafetyMode.ExecutionAndPublication))
            .AddSingleton(serviceProvider => serviceProvider.GetRequiredService<Lazy<WebApplication>>().Value);

        var globalServiceRouteOptions = new GlobalServiceRouteOptions();
        action.Invoke(globalServiceRouteOptions);
        var assemblies = globalServiceRouteOptions.AdditionalAssemblies?.ToList() ?? new List<Assembly>();
        if (!globalServiceRouteOptions.DisableContainsAppDomainAssemblies)
        {
            assemblies = assemblies.Union(AppDomain.CurrentDomain.GetAssemblies()).ToList();
        }

        application.AddMinimalRegisterComponents(assemblies);
        return App.Instance.GetRequiredSingletonService<WebApplication>();
    }

    private static SpeedBootApplication AddMinimalRegisterComponents(this SpeedBootApplication application, IEnumerable<Assembly>? assemblies)
    {
        var serviceRegisterStartup = new ServiceRegisterStartup(application.Services, assemblies ?? GlobalConfig.DefaultAssemblies, null);
        application.Startups.Add(serviceRegisterStartup);
        return application;
    }

    private sealed class MinimalAPIsProvider
    {
    }
}
#endif
