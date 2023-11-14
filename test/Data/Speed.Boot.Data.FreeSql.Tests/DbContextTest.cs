// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.FreeSql.Tests;

[TestClass]
public class DbContextTest : TestBase
{
    [TestMethod]
    public void Add()
    {
        var services = new ServiceCollection();
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile("appsettings.json");
        var configurationRoot = configurationBuilder.Build();
        services.AddConfiguration(configurationRoot);
        services.AddSpeed();
        services.AddSpeedDbContext<TestDbContext>(speedDbContextOptionsBuilder => speedDbContextOptionsBuilder.UseSqlServer());
        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();

        var user = new User()
        {
            Name = "speed-freesql",
            CreateTime = DateTime.Today,
            UpdateTime = DateTime.Now
        };
        dbContext.Set<User>().Add(user);
        var effectRow = dbContext.SaveChanges();
        Assert.AreEqual(1, effectRow);
    }
}
