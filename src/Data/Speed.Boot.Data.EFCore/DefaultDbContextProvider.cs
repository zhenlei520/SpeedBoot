// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.EFCore;

public class DefaultDbContextProvider: IDbContextProvider
{
    private readonly IServiceProvider _serviceProvider;

    public DefaultDbContextProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IDbContext GetDbContext<TEntity>(DbOperateTypes operateType) where TEntity : class, IEntity
    {
        //todo: 得到对应的数据库上下文
        return null;
    }
}
