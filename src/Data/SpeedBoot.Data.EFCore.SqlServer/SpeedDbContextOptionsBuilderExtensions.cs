// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Data.EFCore;

public static class SpeedDbContextOptionsBuilderExtensions
{
    public static SpeedDbContextOptionsBuilder UseSqlServer(
        this SpeedDbContextOptionsBuilder builder,
        Action<SqlServerDbContextOptionsBuilder>? sqlServerOptionsAction = null)
    {
        var name = ConnectionStringNameAttribute.GetConnStringName(builder.DbContextType);
        builder.OptionsAction = (serviceProvider, dbContextOptionsBuilder) =>
        {
            var connectionStringProvider = serviceProvider.GetRequiredService<IConnectionStringProvider>();
            dbContextOptionsBuilder.UseSqlServer(connectionStringProvider.GetConnectionString(name), sqlServerOptionsAction);
        };
        return builder;
    }

    public static SpeedDbContextOptionsBuilder UseSqlServer(
        this SpeedDbContextOptionsBuilder builder,
        string connectionString,
        Action<SqlServerDbContextOptionsBuilder>? sqlServerOptionsAction = null)
    {
        builder.OptionsAction = (_, dbContextOptionsBuilder)
            => dbContextOptionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction);
        return builder;
    }
}
