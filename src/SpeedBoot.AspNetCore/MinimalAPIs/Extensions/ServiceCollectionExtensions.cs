// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER
using Microsoft.AspNetCore.Builder;
using SpeedBoot.AspNetCore.Internal;

namespace SpeedBoot.AspNetCore;

public static class ServiceCollectionExtensions
{
    private static List<string> exceptNames = new List<string>()
    {
        "Microsoft.",
        "System."
    };

    public static WebApplication AddMinimalAPIs(
        this IServiceCollection services,
        WebApplicationBuilder builder,
        Assembly[]? assemblies = null)
    {
        return services.AddMinimalAPIs(builder, options =>
        {
            options.AdditionalAssemblies = assemblies;
        });
    }

    public static WebApplication AddMinimalAPIs(
        this IServiceCollection services,
        WebApplicationBuilder builder,
        Action<GlobalServiceRouteOptions> action)
    {
        if (!ServiceCollectionUtils.TryAdd<MinimalAPIsProvider>(services))
            return App.Instance.GetRequiredSingletonService<WebApplication>();

        services
            .AddHttpContextAccessor()
            .AddSingleton(new Lazy<WebApplication>(builder.Build, LazyThreadSafetyMode.ExecutionAndPublication))
            .AddSingleton(serviceProvider => serviceProvider.GetRequiredService<Lazy<WebApplication>>().Value);

        var globalServiceRouteOptions = new GlobalServiceRouteOptions();
        action.Invoke(globalServiceRouteOptions);
        var assemblies = globalServiceRouteOptions.AdditionalAssemblies?.ToList() ?? GlobalConfig.DefaultAssemblies.ToList();
        if (!globalServiceRouteOptions.DisableContainsAppDomainAssemblies)
        {
            assemblies = assemblies.Union(AppDomain.CurrentDomain.GetAssemblies()).ToList();
        }
        services.TryRegisterActionFilters(assemblies);
        services.RegisterServices(assemblies, globalServiceRouteOptions);
        return App.Instance.GetRequiredSingletonService<WebApplication>();
    }

    private static void TryRegisterActionFilters(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
#if NET7_0_OR_GREATER
        services.RegisterActionFilters(assemblies);
#endif
    }

#if NET7_0_OR_GREATER
    private static void RegisterActionFilters(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies)
    {
        var actionFilterProviderTypes = AssemblyHelper.GetActionFilterProviders(assemblies).ToList();
        foreach (var providerType in actionFilterProviderTypes)
        {
            services.AddSingleton(providerType);
        }
    }
#endif

    private static void RegisterServices(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies,
        GlobalServiceRouteOptions globalServiceRouteOptions)
    {
        services.TryAdd(ServiceDescriptor.Singleton<IEnglishPluralizationService, EnglishPluralizationService>());
        var serviceTypes = AssemblyHelper.GetServiceTypes(assemblies).ToList();

        foreach (var serviceType in serviceTypes)
        {
            services.AddSingleton(serviceType);
        }
        foreach (var serviceType in serviceTypes)
        {
            var service = services.BuildServiceProvider().GetRequiredService(serviceType);

            var serviceBase = (ServiceBase)service;
            if (serviceBase.RouteOptions.DisableAutoMapRoute ?? globalServiceRouteOptions.DisableAutoMapRoute ?? false)
                continue;

            serviceBase.AutoMapRoute(globalServiceRouteOptions);
        }
    }

    private sealed class MinimalAPIsProvider
    {
    }
}
#endif
