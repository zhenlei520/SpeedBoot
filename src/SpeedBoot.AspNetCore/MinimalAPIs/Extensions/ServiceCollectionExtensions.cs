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
        services.TryRegisterEndpointFilters(assemblies, globalServiceRouteOptions.DefaultEndpointFilterServiceLifetime);
        services.RegisterServices(assemblies, globalServiceRouteOptions);
        return App.Instance.GetRequiredSingletonService<WebApplication>();
    }

    private static void TryRegisterEndpointFilters(this IServiceCollection services, IEnumerable<Assembly> assemblies,  ServiceLifetime endpointFilterServiceLifetime)
    {
#if NET7_0_OR_GREATER
        services.RegisterEndpointFilters(assemblies, endpointFilterServiceLifetime);
#endif
    }

#if NET7_0_OR_GREATER
    private static void RegisterEndpointFilters(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies,
        ServiceLifetime endpointFilterServiceLifetime)
    {
        var endpointFilterProviderTypes = AssemblyHelper.GetEndpointFilterProviderTypes(assemblies).ToList();
        foreach (var providerType in endpointFilterProviderTypes)
        {
            services.Add(new ServiceDescriptor(providerType, providerType, endpointFilterServiceLifetime));
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
        var serviceProvider=services.BuildServiceProvider();
        var app = serviceProvider.GetRequiredService<WebApplication>();
        foreach (var serviceType in serviceTypes)
        {
            var service = app.Services.GetRequiredService(serviceType);

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
