// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System.Linq;

public static class LinqExtensions
{
    public static IQueryable<TEntity> GetQueryable<TEntity>(this IQueryable<TEntity> query, Dictionary<string, object> fields)
        where TEntity : class
    {
        return fields.Aggregate(query, (current, field) => current.GetQueryable(field.Key, field.Value));
    }

    private static IQueryable<TEntity> GetQueryable<TEntity>(this IQueryable<TEntity> query, string field, object val) where TEntity : class
    {
        var type = typeof(TEntity);
        var parameter = Expression.Parameter(type, "entity");

        var property = type.GetProperty(field)!;
        Expression expProperty = Expression.Property(parameter, property.Name);

        Expression<Func<object>> valueLambda = () => val;
        Expression expValue = Expression.Convert(valueLambda.Body, property.PropertyType);
        Expression expression = Expression.Equal(expProperty, expValue);
        var filter = (Expression<Func<TEntity, bool>>)Expression.Lambda(expression, parameter);
        return query.Where(filter);
    }

    /// <summary>
    /// Sort by specified field
    /// 按照指定字段进行排序
    /// </summary>
    /// <param name="query"></param>
    /// <param name="fields">Sorting field collection (key: sorting field, value: false ascending, true descending)（排序字段集合（key：排序字段、value：false升序、true降序））</param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, Dictionary<string, bool> fields)
        where TEntity : class
    {
        var index = 0;
        foreach (var field in fields)
        {
            query = index == 0 ? query.OrderBy(field.Key, field.Value) : query.ThenBy(field.Key, field.Value);
            index++;
        }

        return query;
    }

    /// <summary>
    /// Sort by specified field
    /// 按照指定字段进行排序
    /// </summary>
    /// <param name="query"></param>
    /// <param name="field">sort field（排序字段）</param>
    /// <param name="isDesc">Whether descending true: descending, false: ascending（是否降序 true：降序、false：升序）</param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, string field, bool isDesc) where TEntity : class
    {
        var propertyInfo = GetPropertyInfo(typeof(TEntity), field);
        var orderExpression = GetOrderExpression(typeof(TEntity), propertyInfo);
        if (isDesc)
        {
            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            var genericMethod = method!.MakeGenericMethod(typeof(TEntity), propertyInfo.PropertyType);
            return (genericMethod.Invoke(null, new object[] { query, orderExpression }) as IQueryable<TEntity>)!;
        }
        else
        {
            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod = method!.MakeGenericMethod(typeof(TEntity), propertyInfo.PropertyType);
            return (IQueryable<TEntity>)genericMethod.Invoke(null, new object[] { query, orderExpression })!;
        }
    }

    private static IQueryable<T> ThenBy<T>(this IQueryable<T> query, string field, bool desc) where T : class
    {
        var propertyInfo = GetPropertyInfo(typeof(T), field);
        var orderExpression = GetOrderExpression(typeof(T), propertyInfo);
        if (desc)
        {
            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "ThenByDescending" && m.GetParameters().Length == 2);
            var genericMethod = method!.MakeGenericMethod(typeof(T), propertyInfo.PropertyType);
            return (genericMethod.Invoke(null, new object[] { query, orderExpression }) as IQueryable<T>)!;
        }
        else
        {
            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "ThenBy" && m.GetParameters().Length == 2);
            var genericMethod = method!.MakeGenericMethod(typeof(T), propertyInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, orderExpression })!;
        }
    }

    private static PropertyInfo GetPropertyInfo(Type entityType, string field)
        => entityType.GetProperties().FirstOrDefault(p => p.Name.Equals(field, StringComparison.OrdinalIgnoreCase))!;

    private static LambdaExpression GetOrderExpression(Type entityType, PropertyInfo propertyInfo)
    {
        var parametersExpression = Expression.Parameter(entityType);
        var fieldExpression = Expression.PropertyOrField(parametersExpression, propertyInfo.Name);
        return Expression.Lambda(fieldExpression, parametersExpression);
    }
}
