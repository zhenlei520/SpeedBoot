// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

[assembly: InternalsVisibleTo("Speed.Boot.Data.FreeSql.Tests")]

// ReSharper disable once CheckNamespace

namespace Speed.Boot.Data.FreeSql;

internal static class SpeedDbContextHelper
{
    private static readonly Dictionary<Type, Expression<Action<SpeedDbContext, IFreeSql>>?> _data = new();

    public static void Register<TDbContext>() where TDbContext : SpeedDbContext, new()
    {
        var dbContextType = typeof(TDbContext);
        if (_data.ContainsKey(dbContextType))
            return;

        var speedDbContextParam = Expression.Parameter(typeof(SpeedDbContext), "speedDbContext");
        var freeSqlParam = Expression.Parameter(typeof(IFreeSql), "freeSql");

        var setDbContextMethodCall = Expression.Call(
            instance: speedDbContextParam,
            method: typeof(SpeedDbContext).GetMethod("SetDbContext", BindingFlags.Instance | BindingFlags.NonPublic)!,
            arguments: freeSqlParam
        );

        var lambdaBody = Expression.Block(
            setDbContextMethodCall
        );

        var lambdaExpression = Expression.Lambda<Action<SpeedDbContext, IFreeSql>>(
            body: lambdaBody,
            parameters: new[] { speedDbContextParam, freeSqlParam }
        );
        _data.Add(dbContextType, lambdaExpression);
    }

    public static void SetDbContext<TDbContext>(TDbContext dbContext, IFreeSql freeSql) where TDbContext : SpeedDbContext
    {
        var lambdaExpression = _data[typeof(TDbContext)];
        SpeedArgumentException.ThrowIfNull(lambdaExpression);
        var compiledLambda = lambdaExpression.Compile();
        compiledLambda.Invoke(dbContext, freeSql);
    }
}
