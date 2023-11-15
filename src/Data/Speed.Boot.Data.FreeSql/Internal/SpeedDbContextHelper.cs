// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

[assembly: InternalsVisibleTo("Speed.Boot.Data.FreeSql.Tests")]

// ReSharper disable once CheckNamespace

namespace Speed.Boot.Data.FreeSql;

internal static class SpeedDbContextHelper
{
    private static Dictionary<Type, ConstructorType> _dictionary = new();
    private static readonly Dictionary<Type, Func<object>> _data = new();
    private static readonly Dictionary<Type, Func<IFreeSql, object>> _data2 = new();
    private static readonly Dictionary<Type, Func<IFreeSql, DbContextOptions, object>> _data3 = new();

    #region Register

    public static void Register<TDbContext>() where TDbContext : SpeedDbContext
    {
        var constructors = typeof(TDbContext).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var constructor = constructors.FirstOrDefault(c => Match(c.GetParameters(), typeof(IFreeSql), typeof(DbContextOptions)));
        if (constructor != null)
        {
            RegisterByTwoConstructor<TDbContext>(constructor);
            return;
        }

        constructor = constructors.FirstOrDefault(c => Match(c.GetParameters(), typeof(IFreeSql)));
        if (constructor != null)
        {
            RegisterByOneConstructor<TDbContext>(constructor);
            return;
        }

        constructor = constructors.FirstOrDefault(c => Match(c.GetParameters()));
        SpeedArgumentException.ThrowIfNull(constructor);
        RegisterByZeroConstructor<TDbContext>(constructor);

        bool Match(ParameterInfo[] parameterInfos, params Type[] types)
        {
            if (parameterInfos.Length != types.Length)
                return false;

            return !parameterInfos.Where((t, index) => t.ParameterType != types[index]).Any();
        }
    }

    private static void RegisterByZeroConstructor<TDbContext>(ConstructorInfo constructor) where TDbContext : SpeedDbContext
    {
        _dictionary[typeof(TDbContext)] = ConstructorType.Zero;
        var param1 = Expression.Parameter(typeof(IFreeSql));
        var newExpr = Expression.New(constructor, param1);
        var lambda = Expression.Lambda<Func<IFreeSql, object>>(newExpr, param1).Compile();
        _data2[typeof(TDbContext)] = lambda;
    }

    private static void RegisterByOneConstructor<TDbContext>(ConstructorInfo constructor) where TDbContext : SpeedDbContext
    {
        _dictionary[typeof(TDbContext)] = ConstructorType.One;
        var param1 = Expression.Parameter(typeof(IFreeSql));
        var newExpr = Expression.New(constructor, param1);
        var lambda = Expression.Lambda<Func<IFreeSql, object>>(newExpr, param1).Compile();
        _data2[typeof(TDbContext)] = lambda;
    }

    private static void RegisterByTwoConstructor<TDbContext>(ConstructorInfo constructor) where TDbContext : SpeedDbContext
    {
        _dictionary[typeof(TDbContext)] = ConstructorType.Two;
        var param1 = Expression.Parameter(typeof(IFreeSql));
        var param2 = Expression.Parameter(typeof(DbContextOptions));
        var newExpr = Expression.New(constructor, param1, param2);
        var lambda = Expression.Lambda<Func<IFreeSql, DbContextOptions, object>>(newExpr, param1, param2).Compile();
        _data3[typeof(TDbContext)] = lambda;
    }

    #endregion

    public static TDbContext CreateInstance<TDbContext>()
        where TDbContext : SpeedDbContext
    {
        var lambda = _data[typeof(TDbContext)];
        SpeedArgumentException.ThrowIfNull(lambda);
        return (lambda.Invoke() as TDbContext)!;
    }

    public static TDbContext CreateInstance<TDbContext>(IFreeSql freeSql)
        where TDbContext : SpeedDbContext
    {
        var lambda = _data2[typeof(TDbContext)];
        SpeedArgumentException.ThrowIfNull(lambda);
        return (lambda.Invoke(freeSql) as TDbContext)!;
    }

    public static TDbContext CreateInstance<TDbContext>(IFreeSql freeSql, DbContextOptions dbContextOptions)
        where TDbContext : SpeedDbContext
    {
        var lambda = _data3[typeof(TDbContext)];
        SpeedArgumentException.ThrowIfNull(lambda);
        return (lambda.Invoke(freeSql, dbContextOptions) as TDbContext)!;
    }

    public static ConstructorType GetConstructorType<TDbContext>() where TDbContext : SpeedDbContext
    {
        return _dictionary[typeof(TDbContext)];
    }
}
