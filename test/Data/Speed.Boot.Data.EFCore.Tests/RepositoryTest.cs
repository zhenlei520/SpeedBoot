// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using Speed.Boot.Data.Abstractions;

namespace Speed.Boot.Data.EFCore.Tests;

[TestClass]
public class RepositoryTest
{
    [TestMethod]
    public async Task FirstOrDefaultAsync()
    {
        var services = new ServiceCollection();
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile("appsettings.json");
        var configurationRoot = configurationBuilder.Build();
        services.AddConfiguration(configurationRoot);
        services.AddSpeed();
        services.AddSpeedDbContext<TestDbContext>(speedDbContextOptionsBuilder => speedDbContextOptionsBuilder.UseSqlServer());
        var serviceProvider = services.BuildServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var repository = scope.ServiceProvider.GetRequiredService<IRepository<User>>();
            var user = await repository.FirstOrDefaultAsync(user => user.Name == "speed");
            Assert.IsNotNull(user);
        }
    }
}
