// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.FreeSql;

public abstract class SpeedDbContext : DbContext, IDbContext
{
    public IFreeSql FreeSql { get; private set; }

    // public SpeedDbContext()
    // {
    //
    // }

    public SpeedDbContext(IFreeSql freeSql, DbContextOptions dbContextOptions) : base(freeSql, dbContextOptions)
    {
        FreeSql = freeSql;
    }

    // /// <summary>
    // /// 设置上下文
    // /// </summary>
    // /// <param name="freeSql"></param>
    // protected virtual void SetDbContext(IFreeSql freeSql)
    // {
    //     FreeSql = freeSql;
    // }
    //
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    // {
    //     options.UseFreeSql(FreeSql);
    //     base.OnConfiguring(options);
    // }
    //
    // internal void SetOnConfiguring()
    // {
    //     var dbContextOptionsBuilder = new DbContextOptionsBuilder();
    //     OnConfiguring(dbContextOptionsBuilder);
    // }
}
