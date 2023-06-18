// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.System.Tests.Repositories;

public interface IRepository<TEntity>
    where TEntity : class
{
}

public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
}
