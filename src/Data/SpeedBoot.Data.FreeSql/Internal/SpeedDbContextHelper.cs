// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

[assembly: InternalsVisibleTo("SpeedBoot.Data.FreeSql.Tests")]

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Data.FreeSql;

internal static class SpeedDbContextHelper<TDbContext>
    where TDbContext : DbContext
{
    private static Type[] _parameterTypes;
    private static Func<object[], TDbContext> _func;

    public static void Register()
    {
        var constructor = typeof(TDbContext)
            .GetConstructors(BindingFlags.Instance | BindingFlags.Public)
            .OrderBy(c => c.GetParameters().Length)
            .FirstOrDefault();
        SpeedArgumentException.ThrowIfNull(constructor);
        _parameterTypes = constructor.GetParameters().Select(p => p.ParameterType).ToArray();
        _func = InstanceExpressionUtils.BuildCreateInstanceDelegate<TDbContext>(constructor);
    }

    public static TDbContext Execute(IServiceProvider serviceProvider, SpeedDbContextOptionsBuilder speedDbContextOptionsBuilder)
    {
        if (_parameterTypes.Length == 0)
        {
            return _func.Invoke(Array.Empty<object>());
        }

        IFreeSql? freeSql = null;
        DbContextOptions? dbContextOptions = null;

        var parameters = new List<object>();
        foreach (var parameterType in _parameterTypes)
        {
            if (parameterType == typeof(IFreeSql))
            {
                if (freeSql == null)
                {
                    var freeSqlBuilder = new FreeSqlBuilder();
                    speedDbContextOptionsBuilder.OptionsAction?.Invoke(serviceProvider, freeSqlBuilder);
                    freeSql = freeSqlBuilder.Build();
                }
                speedDbContextOptionsBuilder.FreeSqlOptionsAction?.Invoke(freeSql);
                parameters.Add(freeSql);
            }
            else if (parameterType == typeof(DbContextOptions))
            {
                dbContextOptions ??= new DbContextOptions();
                speedDbContextOptionsBuilder.DbContextOptionsAction?.Invoke(dbContextOptions);
                parameters.Add(dbContextOptions);
            }
            else
            {
                parameters.Add(serviceProvider.GetService(parameterType)!);
            }
        }

        return _func.Invoke(parameters.ToArray());
    }
}
