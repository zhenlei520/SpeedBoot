// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql.Tests;

[TestClass]
public class FreeSqlAuditInterceptorTest : TestBase
{
    [TestMethod]
    public void TestSetEntity()
    {
        var serviceProvider = Services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var userInfo = new User()
        {
            Id = Guid.NewGuid(),
            Name = "speedboot",
            CreateTime = DateTime.Today,
            UpdateTime = DateTime.Now
        };
        scope.ServiceProvider.GetRequiredService<FreeSqlAuditInterceptor>().SetEntity(scope.ServiceProvider, new DbContext.EntityChangeReport.ChangeInfo()
        {
            EntityType = typeof(User),
            Object = userInfo
        });
    }
}
