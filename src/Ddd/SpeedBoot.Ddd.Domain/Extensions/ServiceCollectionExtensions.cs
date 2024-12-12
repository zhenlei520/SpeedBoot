// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainEventBus(
        this IServiceCollection services)
    {
        if (!ServiceCollectionUtils.TryAdd<DomainEventBusProvider>(services))
            return services;

        services.TryAddScoped<IDomainService, DomainService>();
        services.TryAddScoped<IDomainEventBus, DomainEventBus>();

        return services;
    }
    private sealed class DomainEventBusProvider
    {
    }
}
