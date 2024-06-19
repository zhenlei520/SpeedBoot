// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Core.Tests;

[TestClass]
public class ServiceCollectionTest
{
    [TestMethod]
    public void TestAddSpeed()
    {
        var services = new ServiceCollection();
        services.AddSpeedBoot();
        Assert.IsTrue(services.Any(d => d.ServiceType == typeof(AppOptions)));
    }
}
