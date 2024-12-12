// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

public static class SpeedDbContextOptionsBuilderExtensions
{
    public static SpeedDbContextOptionsBuilder UseFilter(
        this SpeedDbContextOptionsBuilder speedDbContextOptionsBuilder,
        Action<FilterOptions>? options = null)
    {
        speedDbContextOptionsBuilder.Services.Configure<FilterOptions>(opt => options?.Invoke(opt));
        speedDbContextOptionsBuilder.Services.TryAddScoped<IDataFilter, DataFilter>();
        speedDbContextOptionsBuilder.Services.TryAddScoped(typeof(DataFilter<>));
        return speedDbContextOptionsBuilder;
    }
}
