// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Tests;

[TestClass]
public class MethodExpressionTest
{
    [TestMethod]
    public void SyncInvokeDelegate()
    {
        var @delegate = MethodExpressionUtils.BuildSyncInvokeDelegate(GetType(), GetType().GetMethod(nameof(Method1)));
        @delegate.Invoke(this, null);
        Console.WriteLine($"exec SyncInvokeDelegate, {DateTime.UtcNow}");
    }

    [TestMethod]
    public void SyncInvokeDelegate2()
    {
        var methodInfo = GetType().GetMethods().Where(m => m.Name == nameof(Method5)).FirstOrDefault();

        var @delegate = MethodExpressionUtils.BuildSyncInvokeDelegate(GetType(), methodInfo.MakeGenericMethod(typeof(User)));
        @delegate.Invoke(this, new object[] { new User() });
    }

    [TestMethod]
    public void SyncInvokeDelegate3()
    {
        var methodInfo = GetType().GetMethods().Where(m => m.Name == nameof(Method6)).FirstOrDefault();
        var @delegate = MethodExpressionUtils.BuildSyncInvokeDelegate(GetType(), methodInfo);
        @delegate.Invoke(this, new object[] { new User()
        {
            Name = "SpeedBoot"
        }});
    }

    [TestMethod]
    public async Task TaskInvokeDelegate()
    {
        var @delegate = MethodExpressionUtils.BuildTaskInvokeDelegate(GetType(), GetType().GetMethod(nameof(Method2)));
        await @delegate.Invoke(this, null);
        Console.WriteLine($"exec TaskInvokeDelegate, {DateTime.UtcNow}");
    }

    [TestMethod]
    public void SyncInvokeWithResultDelegate()
    {
        var @delegate = MethodExpressionUtils.BuildSyncInvokeWithResultDelegate<string>(GetType(), GetType().GetMethod(nameof(Method3)));
        var res = @delegate.Invoke(this, null);
        Console.WriteLine($"exec SyncInvokeWithResultDelegate, {DateTime.UtcNow}, {res}");
    }

    [TestMethod]
    public void SyncInvokeWithResultDelegate2()
    {
        var @delegate = MethodExpressionUtils.BuildSyncInvokeWithResultDelegate(GetType(), GetType().GetMethod(nameof(Method31)));
        var res = @delegate.Invoke(this, null);
        Console.WriteLine($"exec SyncInvokeWithResultDelegate, {DateTime.UtcNow}, {string.Join(',', res as List<string>)}");
    }

    [TestMethod]
    public async Task TaskInvokeWithResultDelegate()
    {
        var @delegate = MethodExpressionUtils.BuildTaskInvokeWithResultDelegate<string>(GetType(), GetType().GetMethod(nameof(Method4)));
        var res = await @delegate.Invoke(this, null);
        Console.WriteLine($"exec SyncInvokeWithResultDelegate, {DateTime.UtcNow}, {res}");
    }

    [TestMethod]
    public async Task TaskInvokeWithResultDelegate2()
    {
        var @delegate = MethodExpressionUtils.BuildTaskInvokeWithResultDelegate(GetType(), GetType().GetMethod(nameof(Method4)));
        var res = await @delegate.Invoke(this, null);
        Console.WriteLine($"exec SyncInvokeWithResultDelegate, {DateTime.UtcNow}, {res}");
    }

    public void Method1()
    {
        Console.WriteLine($"exec Method1, {DateTime.UtcNow}");
        Thread.Sleep(3000);
    }

    public Task Method2()
    {
        Console.WriteLine($"exec Method2, {DateTime.UtcNow}");
        Thread.Sleep(3000);
        return Task.CompletedTask;
    }

    public string Method3()
    {
        Console.WriteLine($"exec Method3, {DateTime.UtcNow}");
        Thread.Sleep(3000);
        return "succeed";
    }

    public List<string> Method31()
    {
        Console.WriteLine($"exec Method3, {DateTime.UtcNow}");
        Thread.Sleep(3000);
        return ["succeed"];
    }

    public Task<string> Method4()
    {
        Console.WriteLine($"exec Method4, {DateTime.UtcNow}");
        Thread.Sleep(3000);
        return Task.FromResult("succeed");
    }

    public void Method5<TRequest>(TRequest request)
        where TRequest : class
    {
        Console.WriteLine($"exec Method5, {typeof(TRequest).Name}");
        Thread.Sleep(3000);
    }

    public void Method6(User user)
    {
        Console.WriteLine($"exec Method6, UserName: {user.Name}, {DateTime.UtcNow}");
        Thread.Sleep(3000);
    }
}
