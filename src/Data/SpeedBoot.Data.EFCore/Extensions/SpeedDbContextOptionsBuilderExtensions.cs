// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

public static class SpeedDbContextOptionsBuilderExtensions
{
    public static SpeedDbContextOptionsBuilder UseFilter<TDbContext>(
        this SpeedDbContextOptionsBuilder<TDbContext> speedDbContextOptionsBuilder,
        Action<FilterOptions<TDbContext>>? options = null)
        where TDbContext : SpeedDbContext
    {
        speedDbContextOptionsBuilder.Services.TryAddSingleton<FilterOptions<TDbContext>>(sp =>
        {
            var filterOptions = new FilterOptions<TDbContext>();
            options?.Invoke(filterOptions);
            return filterOptions;
        });

        speedDbContextOptionsBuilder.Services.TryAddScoped<IDataFilter, DataFilter>();
        speedDbContextOptionsBuilder.Services.TryAddScoped(typeof(DataFilter<>));
        return speedDbContextOptionsBuilder;
    }
}
