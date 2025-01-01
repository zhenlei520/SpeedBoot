﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.EFCore;

public class SpeedDbContextOptionsBuilder<TDbContext> : SpeedDbContextOptionsBuilder
    where TDbContext : SpeedDbContext
{
    public SpeedDbContextOptionsBuilder(IServiceCollection services) : base(services, typeof(TDbContext))
    {
    }
}
