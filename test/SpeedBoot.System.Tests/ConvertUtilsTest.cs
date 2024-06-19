// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Tests;

[TestClass]
public class ConvertUtilsTest
{
    [TestMethod]
    public void TestSupportConvertTo()
    {
        var result = ConvertUtils.SupportConvertTo(typeof(FillPattern));
        Assert.AreEqual(true, result);

        result = ConvertUtils.SupportConvertTo(typeof(FillPattern?));
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void TestBuildDelegate()
    {
        var convertDelegate = ConvertDelegate<FillPattern>.GetConvertToDelegate();
        var result = convertDelegate.Invoke("1");
        Assert.AreEqual(FillPattern.NoFill, result);

        var convertDelegate2 = ConvertDelegate<FillPattern?>.GetConvertToDelegate();
        var result2 = convertDelegate2.Invoke("1");
        Assert.AreEqual(FillPattern.NoFill, result2);

        var convertDelegate3 = ConvertDelegate<FillPattern?>.GetConvertToDelegate();
        var result3 = convertDelegate3.Invoke("");
        Assert.AreEqual(null, result3);
    }
}
