// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql.Tests;

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
            UpdateTime = DateTime.Now.ToUniversalTime()
        };
        dbContext.Set<User>().Add(user);

        var user2 = new User()
        {
            Name = "speed-freesql2",
            CreateTime = DateTime.Today.ToUniversalTime(),
            UpdateTime = DateTime.Now.ToUniversalTime()
        };
        dbContext.Set<User>().Add(user2);

        var person = new Person()
        {
            Name = "speed-freesql",
            CreateTime = DateTime.Today.ToUniversalTime(),
            UpdateTime = DateTime.Now.ToUniversalTime()
        };
        dbContext.Set<Person>().Add(person);
        var effectRow = dbContext.SaveChanges();
        Assert.AreEqual(3, effectRow);
        //todo: Modifications, deletions, additions, and transactions to be tested
    }

    [TestMethod]
    public void Update()
    {
        var serviceProvider = Services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();
        var user = dbContext.User.Where(p => p.Name == "speed-freesql").First();
        user.Name = "speed-freesql3";
        dbContext.Set<User>().Update(user);
        dbContext.SaveChanges();
    }
}
