// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using SpeedBoot.Data.EFCore.Tests.Infrastructure.Interceptors;
using ISaveChangesInterceptor = SpeedBoot.Data.Abstractions.ISaveChangesInterceptor;

namespace SpeedBoot.Data.EFCore.Tests.Components;

public class EFCoreServiceRegister : ServiceRegisterComponentBase
{
    /// <summary>
    /// SqlServer = 1
    /// Mysql = 2
    /// PostgreSQL = 3
    /// Sqlite = 4
    /// </summary>
    private static int DataBase;

    public override void ConfigureServices(ConfigureServiceContext context)
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile("appsettings.json");
        var configurationRoot = configurationBuilder.Build();
        DataBase = int.Parse(configurationRoot["ConnectionStrings:DataBase"]);
        context.Services.AddConfiguration(configurationRoot);
        context.Services.AddSingleton<ISaveChangesInterceptor, SaveChangesInterceptor>();
        context.Services.AddSpeedDbContext<TestDbContext>(speedDbContextOptionsBuilder =>
        {
            switch (DataBase)
            {
                case 1:
                    speedDbContextOptionsBuilder.UseSqlServer();
                    break;
                case 2:
                    speedDbContextOptionsBuilder.UseMySql(new MySqlServerVersion("8.2.0"));
                    break;
                case 3:
                    speedDbContextOptionsBuilder.UsePostgreSQL();
                    break;
                case 4:
                    speedDbContextOptionsBuilder.UseSqlite();
                    break;
            }
        });
    }
}
