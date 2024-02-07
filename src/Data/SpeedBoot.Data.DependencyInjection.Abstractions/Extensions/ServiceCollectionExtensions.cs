// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

[assembly: InternalsVisibleTo("SpeedBoot.Data.EFCore")]
[assembly: InternalsVisibleTo("SpeedBoot.Data.FreeSql")]

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpeedDbContextCore(this IServiceCollection services)
    {
        services.TryAddScoped<IDbContextProvider, DefaultDbContextProvider>();
        return services;
    }
}
