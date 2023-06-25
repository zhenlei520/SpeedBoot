// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Tests;

[TestClass]
public class TypeExtensionsTest
{
    [TestMethod]
    public void TestIsInherit()
    {

    }
}

public interface IRepository<TEntity> where TEntity : class {}

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class {}
