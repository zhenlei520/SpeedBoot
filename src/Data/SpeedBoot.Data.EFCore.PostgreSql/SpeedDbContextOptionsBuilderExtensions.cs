// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Data.EFCore;

public static class SpeedDbContextOptionsBuilderExtensions
{
    #region Compatible with lower versions

    public static SpeedDbContextOptionsBuilder UsePostgreSQL(
        this SpeedDbContextOptionsBuilder builder,
        Action<NpgsqlDbContextOptionsBuilder>? npgsqlOptionsAction = null)
    {
        var name = ConnectionStringNameAttribute.GetConnStringName(builder.DbContextType);
        builder.OptionsAction = (serviceProvider, dbContextOptionsBuilder) =>
        {
            var connectionStringProvider = serviceProvider.GetRequiredService<IConnectionStringProvider>();
            var connectionString = connectionStringProvider.GetConnectionString(name);
            dbContextOptionsBuilder.UseNpgsql(connectionString, npgsqlOptionsAction);
        };
        return builder;
    }

    public static SpeedDbContextOptionsBuilder UsePostgreSQL(
        this SpeedDbContextOptionsBuilder builder,
        string connectionString,
        Action<NpgsqlDbContextOptionsBuilder>? npgsqlOptionsAction = null)
    {
        builder.OptionsAction = (_, dbContextOptionsBuilder) =>
        {
            dbContextOptionsBuilder.UseNpgsql(connectionString, npgsqlOptionsAction);
        };
        return builder;
    }

    public static SpeedDbContextOptionsBuilder UsePostgreSQL(
        this SpeedDbContextOptionsBuilder builder,
        DbConnection connection,
        Action<NpgsqlDbContextOptionsBuilder>? npgsqlOptionsAction = null)
    {
        builder.OptionsAction = (_, dbContextOptionsBuilder) =>
        {
            dbContextOptionsBuilder.UseNpgsql(connection, npgsqlOptionsAction);
        };
        return builder;
    }

    #endregion
}
