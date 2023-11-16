// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed.Boot.Data.EFCore;

public static class SpeedDbContextOptionsBuilderExtensions
{
    #region Compatible with lower versions

    public static SpeedDbContextOptionsBuilder UseMySql(
        this SpeedDbContextOptionsBuilder builder,
        ServerVersion serverVersion,
        Action<MySqlDbContextOptionsBuilder>? mySqlOptionsAction = null)
    {
        var name = ConnectionStringNameAttribute.GetConnStringName(builder.DbContextType);
        builder.OptionsAction = (serviceProvider, dbContextOptionsBuilder) =>
        {
            var connectionStringProvider = serviceProvider.GetRequiredService<IConnectionStringProvider>();
            var connectionString = connectionStringProvider.GetConnectionString(name);
#if NET5_0_OR_GREATER
            dbContextOptionsBuilder.UseMySql(connectionStringProvider.GetConnectionString(name), serverVersion, mySqlOptionsAction);
#else
            dbContextOptionsBuilder.UseMySql(connectionString, mySqlOptionsAction);
#endif
        };
        return builder;
    }

    public static SpeedDbContextOptionsBuilder UseMySql(
        this SpeedDbContextOptionsBuilder builder,
        string connectionString,
        ServerVersion serverVersion,
        Action<MySqlDbContextOptionsBuilder>? mySqlOptionsAction = null)
    {
        builder.OptionsAction = (_, dbContextOptionsBuilder) =>
        {
#if NET5_0_OR_GREATER
            dbContextOptionsBuilder.UseMySql(connectionString, serverVersion, mySqlOptionsAction);
#else
            dbContextOptionsBuilder.UseMySql(connectionString, mySqlOptionsAction);
#endif
        };
        return builder;
    }

    public static SpeedDbContextOptionsBuilder UseMySql(
        this SpeedDbContextOptionsBuilder builder,
        DbConnection connection,
        ServerVersion serverVersion,
        Action<MySqlDbContextOptionsBuilder>? mySqlOptionsAction = null)
    {
        builder.OptionsAction = (_, dbContextOptionsBuilder) =>
        {
#if NET5_0_OR_GREATER
            dbContextOptionsBuilder.UseMySql(connection, serverVersion, mySqlOptionsAction);
#else
            dbContextOptionsBuilder.UseMySql(connection, mySqlOptionsAction);
#endif
        };
        return builder;
    }

    #endregion

    #region version below net5.0

#if NET5_0_OR_GREATER
#else

    /// <summary>
    /// Version is not required for lower versions
    /// </summary>
    private static readonly ServerVersion EmptyVersion = new("");

    public static SpeedDbContextOptionsBuilder UseMySql(
        this SpeedDbContextOptionsBuilder builder,
        Action<MySqlDbContextOptionsBuilder>? mySqlOptionsAction = null)
    {
        return builder.UseMySql(EmptyVersion, mySqlOptionsAction);
    }

    public static SpeedDbContextOptionsBuilder UseMySql(
        this SpeedDbContextOptionsBuilder builder,
        string connectionString,
        Action<MySqlDbContextOptionsBuilder>? mySqlOptionsAction = null)
    {
        return builder.UseMySql(connectionString, EmptyVersion, mySqlOptionsAction);
    }

    public static SpeedDbContextOptionsBuilder UseMySql(
        this SpeedDbContextOptionsBuilder builder,
        DbConnection connection,
        Action<MySqlDbContextOptionsBuilder>? mySqlOptionsAction = null)
    {
        return builder.UseMySql(connection, EmptyVersion, mySqlOptionsAction);
    }
#endif

    #endregion
}
