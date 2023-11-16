// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed.Boot.Data.FreeSql;

public static class SpeedDbContextOptionsBuilderExtensions
{
    public static SpeedDbContextOptionsBuilder UsePostgreSQL(
        this SpeedDbContextOptionsBuilder builder,
        Action<FreeSqlBuilder>? freeSqlBuilderAction = null,
        Action<IFreeSql>? freeSqlAction = null,
        Action<DbContextOptions>? dbContextOptionsAction = null)
    {
        var name = ConnectionStringNameAttribute.GetConnStringName(builder.DbContextType);
        builder.OptionsAction = (serviceProvider, dbContextOptionsBuilder) =>
        {
            var connectionStringProvider = serviceProvider.GetRequiredService<IConnectionStringProvider>();
            dbContextOptionsBuilder.UseConnectionString(DataType.PostgreSQL, connectionStringProvider.GetConnectionString(name));
            freeSqlBuilderAction?.Invoke(dbContextOptionsBuilder);
        };
        builder.FreeSqlOptionsAction = freeSql => { freeSqlAction?.Invoke(freeSql); };
        builder.DbContextOptionsAction = dbContextOptions => { dbContextOptionsAction?.Invoke(dbContextOptions); };
        return builder;
    }

    public static SpeedDbContextOptionsBuilder UsePostgreSQL(
        this SpeedDbContextOptionsBuilder builder,
        string connectionString,
        Action<FreeSqlBuilder>? freeSqlBuilderAction = null,
        Action<IFreeSql>? freeSqlAction = null,
        Action<DbContextOptions>? dbContextOptionsAction = null)
    {
        builder.OptionsAction = (_, dbContextOptionsBuilder) =>
        {
            dbContextOptionsBuilder.UseConnectionString(DataType.SqlServer, connectionString);
            freeSqlBuilderAction?.Invoke(dbContextOptionsBuilder);
        };
        builder.FreeSqlOptionsAction = freeSql => { freeSqlAction?.Invoke(freeSql); };
        builder.DbContextOptionsAction = dbContextOptions => { dbContextOptionsAction?.Invoke(dbContextOptions); };
        return builder;
    }
}
