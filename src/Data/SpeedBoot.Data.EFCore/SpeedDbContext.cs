// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.EFCore;

public abstract class SpeedDbContext : DbContext, IDbContext
{
    public SpeedDbContext()
    {
    }

    public SpeedDbContext(DbContextOptions options) : base(options)
    {
    }

    public SpeedDbContext(DbContextOptions<SpeedDbContext> options) : base(options)
    {
    }
}
