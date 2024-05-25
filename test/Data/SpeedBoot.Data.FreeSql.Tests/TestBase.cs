// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql.Tests;

public abstract class TestBase
{
    /// <summary>
    /// SqlServer = 1
    /// Mysql = 2
    /// PostgreSQL = 3
    /// Sqlite = 4
    /// </summary>
    protected static int DataBase;

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
        DataBase = int.Parse(configurationRoot["ConnectionStrings:DataBase"]);
        Services.AddConfiguration(configurationRoot);
        Services.AddSpeed();
        Services.AddSpeedDbContext<TestDbContext>(speedDbContextOptionsBuilder =>
        {
            switch (DataBase)
            {
                case 1:
                    speedDbContextOptionsBuilder.UseSqlServer();
                    break;
                case 2:
                    speedDbContextOptionsBuilder.UseMySql();
                    break;
                case 3:
                    speedDbContextOptionsBuilder.UsePostgreSQL();
                    break;
                case 4:
                    speedDbContextOptionsBuilder.UseSqlite(freeSqlBuilder => freeSqlBuilder.UseAutoSyncStructure(true));
                    break;
            }
        });

        var dbContext = Services.BuildServiceProvider().GetService<TestDbContext>();
        SpeedArgumentException.ThrowIfNull(dbContext);
    }
}
