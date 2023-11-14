// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed.Boot.Data.FreeSql;

public static class SpeedDbContextOptionsBuilderExtensions
{
    public static SpeedDbContextOptionsBuilder UseSqlServer(
        this SpeedDbContextOptionsBuilder builder,
        Action<FreeSqlBuilder>? freeSqlBuilderAction = null)
    {
        var name = ConnectionStringNameAttribute.GetConnStringName(builder.DbContextType);
        builder.OptionsAction = (serviceProvider, dbContextOptionsBuilder) =>
        {
            var connectionStringProvider = serviceProvider.GetRequiredService<IConnectionStringProvider>();
            dbContextOptionsBuilder.UseConnectionString(DataType.SqlServer, connectionStringProvider.GetConnectionString(name));
            freeSqlBuilderAction?.Invoke(dbContextOptionsBuilder);
        };
        return builder;
    }

    public static SpeedDbContextOptionsBuilder UseSqlServer(
        this SpeedDbContextOptionsBuilder builder,
        string connectionString,
        Action<FreeSqlBuilder>? freeSqlBuilderAction = null)
    {
        builder.OptionsAction = (_, dbContextOptionsBuilder) =>
        {
            dbContextOptionsBuilder.UseConnectionString(DataType.SqlServer, connectionString);
            freeSqlBuilderAction?.Invoke(dbContextOptionsBuilder);
        };
        return builder;
    }
}
