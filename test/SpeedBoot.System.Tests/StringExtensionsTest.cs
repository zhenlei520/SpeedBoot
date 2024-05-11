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

    [DataRow("123", typeof(int), 123)]
    [DataRow("", typeof(int), null)]
    [DataRow("123", typeof(int?), 123)]
    [DataRow("", typeof(int?), null)]
    [DataRow("123", typeof(long), 123L)]
    [DataRow("", typeof(long), null)]
    [DataRow("123", typeof(long?), 123L)]
    [DataRow("", typeof(long?), null)]
    [DataRow("123", typeof(string), "123")]
    [DataRow("", typeof(string), null)]
    [DataRow("", typeof(short), null)]
    [DataRow("", typeof(float), null)]
    [DataRow("", typeof(decimal), null)]
    [DataRow("", typeof(double), null)]
    [DataRow("", typeof(bool), null)]
    [DataRow("", typeof(char), null)]
    [DataRow("", typeof(byte), null)]
    [DataRow("", typeof(sbyte), null)]
    [DataRow("", typeof(ushort), null)]
    [DataRow("", typeof(uint), null)]
    [DataRow("", typeof(ulong), null)]
    [DataRow("", typeof(Guid), null)]
    [DataRow("", typeof(DateTime), null)]
    [DataRow("1", typeof(short), (short)1)]
    [DataRow("1", typeof(short?), (short)1)]
    [DataRow("1.2", typeof(float), 1.2f)]
    [DataRow("1.2", typeof(float?), 1.2f)]
    [DataRow("1.2", typeof(double), 1.2d)]
    [DataRow("1.2", typeof(double?), 1.2d)]
    [DataRow("true", typeof(bool), true)]
    [DataRow("true", typeof(bool?), true)]
    [DataRow("false", typeof(bool), false)]
    [DataRow("false", typeof(bool?), false)]
    [DataRow("a", typeof(char), (char)'a')]
    [DataRow("a", typeof(char?), (char)'a')]
    [DataRow("1", typeof(byte), (byte)1)]
    [DataRow("1", typeof(byte?), (byte)1)]
    [DataRow("1", typeof(sbyte), (sbyte)1)]
    [DataRow("1", typeof(sbyte?), (sbyte)1)]
    [DataRow("1", typeof(ushort), (ushort)1)]
    [DataRow("1", typeof(ushort?), (ushort)1)]
    [DataRow("1", typeof(uint), (uint)1)]
    [DataRow("1", typeof(uint?), (uint)1)]
    [DataRow("1", typeof(ulong), (ulong)1)]
    [DataTestMethod]
    public void TestTryConvertTo(string str, Type type, object expected)
    {
        str.TryConvertTo(type, out var obj);
        Assert.AreEqual(expected, obj);
    }

    [TestMethod]
    public void TestTryConvertToDecimal()
    {
        var str = "1.2";
        str.TryConvertTo(typeof(decimal), out var obj);
        Assert.AreEqual(1.2m, obj);

        str.TryConvertTo(typeof(decimal?), out var obj2);
        Assert.AreEqual(1.2m, obj2);
    }

    [TestMethod]
    public void TestTryConvertToGuid()
    {
        var str = "a17783e5-0d4e-11ef-a5c3-4ec31e5ba711";
        str.TryConvertTo(typeof(Guid), out var obj);
        Assert.AreEqual(Guid.Parse(str), obj);

        str.TryConvertTo(typeof(Guid?), out var obj2);
        Assert.AreEqual(Guid.Parse(str), obj2);
    }

    [TestMethod]
    public void TestTryConvertToDateTime()
    {
        var str = "2024-01-01T00:00:00";
        str.TryConvertTo(typeof(DateTime), out var obj);
        Assert.AreEqual(DateTime.Parse(str), obj);

        str.TryConvertTo(typeof(DateTime?), out var obj2);
        Assert.AreEqual(DateTime.Parse(str), obj2);
    }
}
