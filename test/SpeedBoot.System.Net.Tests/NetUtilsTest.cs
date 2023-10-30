// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Net.Tests;

[TestClass]
public class NetUtilsTest
{
    [TestMethod]
    public void TestGetIpV4()
    {
        var localIpV4 = NetUtils.GetIpV4();
        Assert.IsNotNull(localIpV4);
    }

    [TestMethod]
    public void TestGetIpV6()
    {
        var localIpV4 = NetUtils.GetIpV6();
        Assert.IsNotNull(localIpV4);
    }
}
