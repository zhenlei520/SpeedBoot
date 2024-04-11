// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLocalEventBus(this IServiceCollection services)
    {
        if (!ServiceCollectionUtils.TryAdd<LocalEventBusProvider>(services))
            return services;

        return services;
    }

    private sealed class LocalEventBusProvider
    {
    }
}
