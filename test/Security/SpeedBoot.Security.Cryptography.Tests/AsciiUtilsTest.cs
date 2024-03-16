// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Security.Cryptography.Tests;

[TestClass]
public class AsciiUtilsTest
{
    [TestMethod]
    public void Test()
    {
        var str = "测试";
        var content = AsciiUtils.Encode(str);
        Assert.AreEqual(str, AsciiUtils.Decode(content));
    }
}
