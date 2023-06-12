// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Extensions.DependencyInjection.Tests.Repositories;

public interface IRepository<TEntity> : IScopedDependency
    where TEntity : class
{
}
