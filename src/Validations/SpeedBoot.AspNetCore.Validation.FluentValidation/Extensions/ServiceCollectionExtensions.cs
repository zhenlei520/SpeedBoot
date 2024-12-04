// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFluentAutoValidation(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        => services.AddFluentAutoValidation(AppDomain.CurrentDomain.GetAssemblies(), lifetime);

    public static IServiceCollection AddFluentAutoValidation(this IServiceCollection services, Assembly[] assemblies, ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        if (!ServiceCollectionUtils.TryAdd<FluentAutoValidationProvider>(services))
            return services;

        services.AddSingleton<ValidatorEntityTypeContext>();
        services.Configure<GlobalServiceRouteOptions>(options =>
        {
            var additionalAssemblies = options.AdditionalAssemblies?.ToList() ?? new List<Assembly>();
            if (!additionalAssemblies.Contains(typeof(AutoValidationAttribute).Assembly))
                additionalAssemblies.Add(typeof(AutoValidationAttribute).Assembly);
            options.AdditionalAssemblies = additionalAssemblies;
        });
        return services.AddValidatorsFromAssemblies(assemblies, lifetime);
    }

    private sealed class FluentAutoValidationProvider
    {
    }
}
