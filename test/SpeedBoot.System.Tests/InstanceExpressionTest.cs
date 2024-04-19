// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Tests;

[TestClass]
public class InstanceExpressionTest
{
    private string _name;

    public InstanceExpressionTest()
    {
        _name = string.Empty;
    }

    public InstanceExpressionTest(string name) : this()
    {
        _name = name;
    }

    [TestMethod]
    public void BuildCreateInstanceDelegate()
    {
        var @delegate = InstanceExpressionUtils.BuildCreateInstanceDelegate(typeof(InstanceExpressionTest).GetConstructors().First());
        var test = @delegate.Invoke([]) as InstanceExpressionTest;
        Assert.IsNotNull(test);
        Assert.AreEqual(string.Empty, test._name);

        @delegate = InstanceExpressionUtils.BuildCreateInstanceDelegate(typeof(InstanceExpressionTest).GetConstructors()
            .First(x => x.GetParameters().Length == 1));
        var name = "SpeedBoot";
        test = @delegate.Invoke([name]) as InstanceExpressionTest;
        Assert.IsNotNull(test);
        Assert.AreEqual(name, test._name);

        @delegate = InstanceExpressionUtils.BuildCreateInstanceDelegate(typeof(Repository<User>).GetConstructors().First());
        var repository = @delegate.Invoke([]) as Repository<User>;
        Assert.IsNotNull(repository);

        InstanceExpressionUtils.BuildCreateInstanceDelegate<string>(typeof(Repository<User>)).Invoke("succeed");
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int>(typeof(Repository<User>)).Invoke("succeed", 2);
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int>(typeof(Repository<User>)).Invoke("succeed", 2);
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int, bool>(typeof(Repository<User>)).Invoke("succeed", 2, true);
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int, bool, DateTime>(typeof(Repository<User>)).Invoke("succeed", 2, true, DateTime.Now);
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int, bool, DateTime, DateOnly>(typeof(Repository<User>)).Invoke("succeed", 2, true, DateTime.Now, DateOnly.FromDateTime(DateTime.Now));
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int, bool, DateTime, DateOnly, TimeOnly>(typeof(Repository<User>)).Invoke("succeed", 2, true, DateTime.Now, DateOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now));
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int, bool, DateTime, DateOnly, TimeOnly, long>(typeof(Repository<User>)).Invoke("succeed", 2, true, DateTime.Now, DateOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now), 1);
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int, bool, DateTime, DateOnly, TimeOnly, long, ushort>(typeof(Repository<User>)).Invoke("succeed", 2, true, DateTime.Now, DateOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now), 1, 1);
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int, bool, DateTime, DateOnly, TimeOnly, long, ushort, ulong>(typeof(Repository<User>)).Invoke("succeed", 2, true, DateTime.Now, DateOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now), 1, 1, 1);
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int, bool, DateTime, DateOnly, TimeOnly, long, ushort, ulong, uint>(typeof(Repository<User>)).Invoke("succeed", 2, true, DateTime.Now, DateOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now), 1, 1, 1, 1);
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int, bool, DateTime, DateOnly, TimeOnly, long, ushort, ulong, uint, UInt16, byte>(typeof(Repository<User>)).Invoke("succeed", 2, true, DateTime.Now, DateOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now), 1, 1, 1, 1, 1, 1);
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int, bool, DateTime, DateOnly, TimeOnly, long, ushort, ulong, uint, UInt16, byte, UInt32>(typeof(Repository<User>)).Invoke("succeed", 2, true, DateTime.Now, DateOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now), 1, 1, 1, 1, 1, 1, 1);
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int, bool, DateTime, DateOnly, TimeOnly, long, ushort, ulong, uint, UInt16, byte, UInt32, float>(typeof(Repository<User>)).Invoke("succeed", 2, true, DateTime.Now, DateOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now), 1, 1, 1, 1, 1, 1, 1, 1);
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int, bool, DateTime, DateOnly, TimeOnly, long, ushort, ulong, uint, UInt16, byte, UInt32, float, double>(typeof(Repository<User>)).Invoke("succeed", 2, true, DateTime.Now, DateOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now), 1, 1, 1, 1, 1, 1, 1, 1, 1d);
        InstanceExpressionUtils.BuildCreateInstanceDelegate<string, int, bool, DateTime, DateOnly, TimeOnly, long, ushort, ulong, uint, UInt16, byte, UInt32, float, double, decimal>(typeof(Repository<User>)).Invoke("succeed", 2, true, DateTime.Now, DateOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now), 1, 1, 1, 1, 1, 1, 1, 1, 2d, 1.1m);
    }
}
