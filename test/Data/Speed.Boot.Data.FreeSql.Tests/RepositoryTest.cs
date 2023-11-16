// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.EFCore.Tests;

[TestClass]
public class RepositoryTest
{
    private IServiceCollection _services;

    public RepositoryTest()
    {
        _services = new ServiceCollection();
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile("appsettings.json");
        var configurationRoot = configurationBuilder.Build();
        _services.AddConfiguration(configurationRoot);
        _services.AddSpeed();
        _services.AddSpeedDbContext<TestDbContext>(speedDbContextOptionsBuilder => speedDbContextOptionsBuilder.UseSqlServer());

        var dbContext = _services.BuildServiceProvider().GetService<TestDbContext>();
        SpeedArgumentException.ThrowIfNull(dbContext);
    }

    [TestMethod]
    public async Task FirstOrDefaultAsync()
    {
        var serviceProvider = _services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IRepository<User>>();
        var user = await repository.FirstOrDefaultAsync(user => user.Name == "speed-freesql");
        Assert.IsNotNull(user);
    }

    [TestMethod]
    public async Task FindAsync()
    {
        var serviceProvider = _services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IRepository<Person>>();
        var person = await repository.FirstOrDefaultAsync(user => user.Name == "speed-freesql");
        SpeedArgumentException.ThrowIfNull(person);

        var person2 = await repository.FindAsync(person.Id, "speed-freesql");
        Assert.IsNotNull(person2);
    }
}
