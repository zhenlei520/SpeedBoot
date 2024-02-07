// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Extensions.IdGenerator;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNormalIdGenerator(this IServiceCollection services, string? key = null)
    {
        if (!ServiceCollectionUtils.TryAdd<NormalIdGeneratorProvider>(services))
            return services;

        services.AddSingleton<IIdGenerator>(_ => new NormalIdGenerator(key));
        return services;
    }

    private sealed class NormalIdGeneratorProvider
    {
    }
}
