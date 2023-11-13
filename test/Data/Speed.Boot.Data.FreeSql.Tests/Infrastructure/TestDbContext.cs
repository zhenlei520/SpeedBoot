// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.FreeSql.Tests.Infrastructure;

public class TestDbContext : SpeedDbContext
{
    public DbSet<User> User { get; set; }
}
