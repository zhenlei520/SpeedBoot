// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.EFCore;

public class SpeedDbContextOptionsBuilder
{
    public Action<IServiceProvider, DbContextOptionsBuilder>? OptionsAction { get; set; }

    internal IServiceCollection Services { get; }

    public Type DbContextType { get; }

    public SpeedDbContextOptionsBuilder(IServiceCollection services, Type dbContextType)
    {
        Services = services;
        DbContextType = dbContextType;
    }
}
