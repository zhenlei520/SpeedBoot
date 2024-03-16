// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Security.Cryptography.Tests;

[TestClass]
public class UrlUtilsTest
{
    [TestMethod]
    public void Test()
    {
        var str = "测试";
        var content = UrlUtils.Encode(str);
        Assert.AreEqual(str, UrlUtils.Decode(content));
    }
}
