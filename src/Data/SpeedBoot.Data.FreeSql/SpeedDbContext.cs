// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql;

public abstract class SpeedDbContext : DbContext, IDbContext
{
    /// <summary>
    /// 使用此构造函数，则必须重写 OnConfiguring 方法
    /// </summary>
    protected SpeedDbContext() : base()
    {
    }

    protected SpeedDbContext(IFreeSql freeSql) : base(freeSql, new DbContextOptions())
    {

    }

    protected SpeedDbContext(IFreeSql freeSql, DbContextOptions dbContextOptions) : base(freeSql, dbContextOptions)
    {
    }
}
