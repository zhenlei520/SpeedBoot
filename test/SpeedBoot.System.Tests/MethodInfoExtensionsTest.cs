// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using SpeedBoot.System.Extensions;

namespace SpeedBoot.System.Tests;

[TestClass]
public class MethodInfoExtensionsTest
{
    [TestMethod]
    public void IsAsyncMethod()
    {
        var instanceType = typeof(MethodInfoExtensionsTest);
        Assert.IsTrue(instanceType.GetMethod(nameof(Method1))!.IsAsyncMethod());
        Assert.IsTrue(instanceType.GetMethod(nameof(Method2))!.IsAsyncMethod());
        Assert.IsTrue(instanceType.GetMethod(nameof(Method3))!.IsAsyncMethod());
        Assert.IsTrue(instanceType.GetMethod(nameof(Method4))!.IsAsyncMethod());
        Assert.IsTrue(instanceType.GetMethod(nameof(Method5))!.IsAsyncMethod());
    }

    public Task Method1()
    {
        return Task.CompletedTask;
    }

    public Task<long> Method2()
    {
        return Task.FromResult(1L);
    }

    public Task<(int, string)> Method3()
    {
        return Task.FromResult((1, ""));
    }

    public Task<List<string>> Method4()
    {
        return Task.FromResult(new List<string>());
    }

    public Task<object> Method5()
    {
        var list = new List<string>();
        return Task.FromResult((object)list);
    }
}
