// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Authentication.Tests;

[TestClass]
public class TypeHelperTest
{
    [DataRow("1", typeof(int), true)]
    [DataRow("1", typeof(int?), true)]
    [DataRow("", typeof(int), false)]
    [DataRow("", typeof(int?), false)]
    [DataRow("1.1", typeof(string), true)]
    [DataRow("1", typeof(string), true)]
    [DataRow("1", typeof(short), true)]
    [DataRow("1", typeof(short?), true)]
    [DataRow("", typeof(short), false)]
    [DataRow("", typeof(short?), false)]
    [DataRow("1", typeof(long), true)]
    [DataRow("1", typeof(long?), true)]
    [DataRow("", typeof(long), false)]
    [DataRow("", typeof(long?), false)]
    [DataRow("1", typeof(float), true)]
    [DataRow("1", typeof(float?), true)]
    [DataRow("", typeof(float), false)]
    [DataRow("", typeof(float?), false)]
    [DataRow("1", typeof(decimal), true)]
    [DataRow("1", typeof(decimal?), true)]
    [DataRow("", typeof(decimal), false)]
    [DataRow("", typeof(decimal?), false)]
    [DataRow("1", typeof(double), true)]
    [DataRow("1", typeof(double?), true)]
    [DataRow("", typeof(double), false)]
    [DataRow("", typeof(double?), false)]
    [DataRow("1", typeof(bool), true)]
    [DataRow("1", typeof(bool?), true)]
    [DataRow("", typeof(bool), false)]
    [DataRow("", typeof(bool?), false)]
    [DataRow("666964b3-ae98-4a27-88ec-cc072b204215", typeof(Guid), true)]
    [DataRow("666964b3-ae98-4a27-88ec-cc072b204215", typeof(Guid?), true)]
    [DataRow("", typeof(Guid), false)]
    [DataRow("", typeof(Guid), false)]
    [DataRow("2024-01-01", typeof(DateTime), true)]
    [DataRow("2024-01-01", typeof(DateTime?), true)]
    [DataRow("", typeof(DateTime), false)]
    [DataRow("", typeof(DateTime?), false)]
    [DataTestMethod]
    public void TestTryConvertTo(string claim, Type type, bool expected)
    {
        Assert.AreEqual(expected, TypeHelper.TryConvertTo(claim, Nullable.GetUnderlyingType(type) ?? type, out _));
    }
}
