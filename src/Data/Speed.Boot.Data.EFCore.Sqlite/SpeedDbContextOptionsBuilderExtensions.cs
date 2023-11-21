// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed.Boot.Data.EFCore;

public static class SpeedDbContextOptionsBuilderExtensions
{
    public static SpeedDbContextOptionsBuilder UseSqlite(
        this SpeedDbContextOptionsBuilder builder,
        Action<SqliteDbContextOptionsBuilder>? sqliteOptionsAction = null)
    {
        var name = ConnectionStringNameAttribute.GetConnStringName(builder.DbContextType);
        builder.OptionsAction = (serviceProvider, dbContextOptionsBuilder) =>
        {
            var connectionStringProvider = serviceProvider.GetRequiredService<IConnectionStringProvider>();
            dbContextOptionsBuilder.UseSqlite(connectionStringProvider.GetConnectionString(name), sqliteOptionsAction);
        };
        return builder;
    }

    public static SpeedDbContextOptionsBuilder UseSqlite(
        this SpeedDbContextOptionsBuilder builder,
        string connectionString,
        Action<SqliteDbContextOptionsBuilder>? sqliteOptionsAction = null)
    {
        builder.OptionsAction = (_, dbContextOptionsBuilder)
            => dbContextOptionsBuilder.UseSqlite(connectionString, sqliteOptionsAction);
        return builder;
    }
}
