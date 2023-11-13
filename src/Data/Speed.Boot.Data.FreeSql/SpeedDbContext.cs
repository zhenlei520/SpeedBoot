// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.FreeSql;

public abstract class SpeedDbContext : DbContext, IDbContext
{
    private IFreeSql _freeSql;

    /// <summary>
    /// 设置上下文
    /// </summary>
    /// <param name="freeSql"></param>
    protected virtual void SetDbContext(IFreeSql freeSql)
    {
        _freeSql = freeSql;
    }
}
