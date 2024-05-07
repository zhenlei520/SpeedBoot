// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Tests;

[TestClass]
public class StringExtensionsTest
{
    [DataRow("112das3,1.2asd", 6)]
    [DataTestMethod]
    public void TestTotalDigits(string str, int expectedLength)
    {
        var length = str.TotalDigits();
        Assert.AreEqual(expectedLength, length);
    }
}
