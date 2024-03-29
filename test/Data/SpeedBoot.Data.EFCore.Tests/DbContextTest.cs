// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using SpeedBoot.Data.EFCore.Tests.Infrastructure;
using SpeedBoot.Data.EFCore.Tests.Model;

namespace SpeedBoot.Data.EFCore.Tests;

[TestClass]
public class DbContextTest : TestBase
{
    [TestMethod]
    public void Add()
    {
        var serviceProvider = Services.BuildServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();
            dbContext.Database.EnsureCreated();
            var user = new User()
            {
                Name = "speed",
                CreateTime = DateTime.Today.ToUniversalTime(),
                UpdateTime = DateTime.Now.ToUniversalTime()
            };
            dbContext.User.Add(user);
            var effectRow = dbContext.SaveChanges();
            Assert.AreEqual(1, effectRow);

            var person = new Person()
            {
                Name = "speed",
                CreateTime = DateTime.Today.ToUniversalTime(),
                UpdateTime = DateTime.Now.ToUniversalTime()
            };
            dbContext.Person.Add(person);
            effectRow = dbContext.SaveChanges();
            Assert.AreEqual(1, effectRow);
        }
    }
}
