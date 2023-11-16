// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.EFCore.Tests;

public class TestBase
{
    /// <summary>
    /// SqlServer =1
    /// Mysql = 2
    /// </summary>
    public const int DataBase = 2;

    /// <summary>
    /// Service Collections
    /// </summary>
    protected IServiceCollection Services;

    protected TestBase()
    {
        Services = new ServiceCollection();
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile("appsettings.json");
        var configurationRoot = configurationBuilder.Build();
        Services.AddConfiguration(configurationRoot);
        Services.AddSpeed();
        Services.AddSpeedDbContext<TestDbContext>(speedDbContextOptionsBuilder =>
        {
            if (DataBase == 1)
            {
                speedDbContextOptionsBuilder.UseSqlServer();
            }
            else if (DataBase == 2)
            {
                speedDbContextOptionsBuilder.UseMySql(new MySqlServerVersion("8.2.0"));
            }
        });
    }
}
