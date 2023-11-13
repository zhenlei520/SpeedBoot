// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.FreeSql.Tests;

[TestClass]
public class SpeedDbContextHelperTest : TestBase
{
    [TestMethod]
    public void TestCreateDbContext()
    {
        SpeedDbContextHelper.Register<TestDbContext>();
        var freeSqlBuilder = new FreeSqlBuilder()
            .UseConnectionString(DataType.Sqlite, @"Data Source=|DataDirectory|\document.db;Pooling=true;Max Pool Size=10")
            .UseAutoSyncStructure(true);
        var freeSql = freeSqlBuilder.Build();
        var testDbContext = new TestDbContext();
        SpeedDbContextHelper.SetDbContext(testDbContext, freeSql);

        var freeSqlFields = typeof(TestDbContext).BaseType?.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
        var freeSqlFieldInfo = freeSqlFields?.FirstOrDefault(x => x.Name == "_freeSql");
        SpeedArgumentException.ThrowIfNull(freeSqlFieldInfo);
        var actualFreeSql = freeSqlFieldInfo.GetValue(testDbContext);
        Assert.AreEqual(freeSql, actualFreeSql);
    }
}
