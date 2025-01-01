// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.EFCore.Tests;

[TestClass]
public class DbContextTest : TestBase
{
    [TestMethod]
    public void Add()
    {
        using var scope = App.Instance.GetRequiredRootServiceProvider().CreateScope();
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

        var userInfo = dbContext.User.First(u => u.Name == "speed");
        userInfo.Name = "speed2";
        dbContext.SaveChanges();

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

    [TestMethod]
    public void SoftDelete()
    {
        using var scope = App.Instance.GetRequiredRootServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();
        dbContext.Database.EnsureCreated();
        var tag = new Tag()
        {
            Name = "speed",
        };
        dbContext.Tag.Add(tag);
        var effectRow = dbContext.SaveChanges();
        Assert.AreEqual(1, effectRow);

        tag.IsDeleted = true;
        dbContext.Tag.Update(tag);
        dbContext.SaveChanges();
        var tagCount = dbContext.Tag.Count();
        Assert.AreEqual(0, tagCount);
        var dataFilter = scope.ServiceProvider.GetRequiredService<IDataFilter>();
        using (dataFilter.Disable<ISoftDelete>())
        {
            tagCount = dbContext.Tag.Count();
            Assert.AreEqual(1, tagCount);
        }
    }
}
