// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql.Tests;

[TestClass]
public class RepositoryTest : TestBase
{
    public RepositoryTest()
    {
        var dbContext = Services.BuildServiceProvider().GetService<TestDbContext>();
        SpeedArgumentException.ThrowIfNull(dbContext);
    }

    [TestMethod]
    public async Task FirstOrDefaultAsync()
    {
        var serviceProvider = Services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IRepository<User>>();
        var user = await repository.FirstOrDefaultAsync(user => user.Name == "speed-freesql");
        Assert.IsNotNull(user);
    }

    [TestMethod]
    public async Task FindAsync()
    {
        var serviceProvider = Services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IRepository<Person>>();
        var person = await repository.FirstOrDefaultAsync(user => user.Name == "speed-freesql");
        SpeedArgumentException.ThrowIfNull(person);

        var person2 = await repository.FindAsync(person.Id, "speed-freesql");
        Assert.IsNotNull(person2);
    }
}
