// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.EFCore;

public abstract class SpeedDbContext<TDbContext> : SpeedDbContext
    where TDbContext : SpeedDbContext
{
    /// <summary>
    /// Supports modifying the default behavior of Filter
    /// </summary>
    protected override bool IsSoftDeleteFilterEnabled =>
        DataFilter?.IsEnabled<ISoftDelete>() ??
        (CurrentServiceProvider ?? App.Instance.RootServiceProvider).GetRequiredService<FilterOptions<TDbContext>>().EnableSoftDelete;

    /// <summary>
    /// The interceptor may not take effect when currentServiceProvider is null
    /// </summary>
    /// <param name="currentServiceProvider"></param>
    public SpeedDbContext(IServiceProvider? currentServiceProvider = null)
        : base(currentServiceProvider)
    {
    }

    /// <summary>
    /// The interceptor may not take effect when currentServiceProvider is null
    /// </summary>
    public SpeedDbContext(DbContextOptions options, IServiceProvider? currentServiceProvider = null)
        : base(options, currentServiceProvider)
    {
    }

    /// <summary>
    /// The interceptor may not take effect when currentServiceProvider is null
    /// </summary>
    public SpeedDbContext(DbContextOptions<SpeedDbContext> options, IServiceProvider? currentServiceProvider = null)
        : base(options, currentServiceProvider)
    {
    }
}
