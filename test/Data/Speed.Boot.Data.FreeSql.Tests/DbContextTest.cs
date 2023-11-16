// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.FreeSql.Tests;

[TestClass]
public class DbContextTest : TestBase
{
    [TestMethod]
    public void Add()
    {
        var serviceProvider = Services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();

        var user = new User()
        {
            Name = "speed-freesql",
            CreateTime = DateTime.Today.ToUniversalTime(),
            UpdateTime = DateTime.Now.ToUniversalTime()
        };
        dbContext.Set<User>().Add(user);
        var effectRow = dbContext.SaveChanges();
        Assert.AreEqual(1, effectRow);

        var person = new Person()
        {
            Name = "speed-freesql",
            CreateTime = DateTime.Today.ToUniversalTime(),
            UpdateTime = DateTime.Now.ToUniversalTime()
        };
        dbContext.Set<Person>().Add(person);
        effectRow = dbContext.SaveChanges();
        Assert.AreEqual(1, effectRow);
    }
}
