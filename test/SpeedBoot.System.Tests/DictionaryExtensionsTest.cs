// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Tests;

[TestClass]
public class DictionaryExtensionsTest
{
    [TestMethod]
    public void TestToDynamic()
    {
        var data = new Dictionary<string, object>()
        {
            { "Name", "SpeedBoot" },
            { "Age", 18 }
        };
        var @dynamic = data.ToDynamic();
        Assert.IsNotNull(@dynamic);
        Assert.AreEqual("SpeedBoot", @dynamic.Name);
        Assert.AreEqual(18, @dynamic.Age);
    }
}
