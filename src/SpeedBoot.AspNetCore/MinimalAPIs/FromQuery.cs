// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER

namespace SpeedBoot.AspNetCore;

/// <summary>
/// Object nesting is not supported
/// </summary>
/// <typeparam name="TQuery"></typeparam>
public class FromQuery<TQuery> where TQuery : new()
{
    static readonly Dictionary<string, Action<TQuery, string, Type, string[]>> SetPropertyValueData;

    static FromQuery()
    {
        SetPropertyValueData = typeof(TQuery).GetProperties().ToDictionary(p => p.Name, BuildSetterValue, StringComparer.OrdinalIgnoreCase);
    }

    public static ValueTask<TQuery?> BindAsync(HttpContext context)
    {
        GeneratedCode.HelloWorld.SayHello();
        var queryCollection = context.Request.Query;
        var query = new TQuery();
        foreach (var item in queryCollection)
        {
            if (item.Value.Count == 0 || (item.Value.Count == 1 && string.IsNullOrWhiteSpace(item.Value)))
                continue;

            if (SetPropertyValueData.TryGetValue(item.Key, out var setter))
            {
                setter.Invoke(query, item.Key, typeof(TQuery), item.Value);
            }
        }

        return ValueTask.FromResult(query);
    }

    static Action<TQuery, string, Type, string[]> BuildSetterValue(PropertyInfo property)
    {
        ParameterExpression target = Expression.Parameter(typeof(TQuery), "target");
        ParameterExpression value = Expression.Parameter(typeof(string[]), "value");
        ParameterExpression instanceTypeParam = Expression.Parameter(typeof(Type), "instanceType");
        ParameterExpression propertyNameParam = Expression.Parameter(typeof(string), "propertyName");

        Expression convertedValue = GetPropertyValueExpression(property, value);

        MemberExpression propertyAccess = Expression.Property(target, property);
        BinaryExpression assignment = Expression.Assign(propertyAccess, convertedValue);
        Expression<Action<TQuery, string, Type, string[]>> lambda = Expression.Lambda<Action<TQuery, string, Type, string[]>>(
            assignment,
            target, propertyNameParam, instanceTypeParam, value);
        var compiledLambda = lambda.Compile();
        return (obj, val, type, name) => compiledLambda(obj, val, type, name);
    }

    private static Expression GetPropertyValueExpression(PropertyInfo property, ParameterExpression value)
    {
        Expression convertedValue;
        if (property.PropertyType == typeof(string))
            convertedValue = GetSingleValue();
        else if (ConvertUtils.SupportConvertTo(property.PropertyType))
        {
            var methodInfo = typeof(ConvertUtils).GetMethod(nameof(ConvertUtils.GetValue), [typeof(string)]).MakeGenericMethod(property.PropertyType) ??
                             throw new SpeedException($"No 'Parse' method available for type {property.PropertyType.Name}");
            convertedValue = Expression.Call(methodInfo, GetSingleValue());
        }
        else if (property.PropertyType.IsArray && property.PropertyType.GetElementType() != null &&
                 ConvertUtils.SupportConvertTo(property.PropertyType.GetElementType()!))
        {
            var type = property.PropertyType.GetElementType();
            var methodInfo = typeof(FromQuery<TQuery>)
                .GetMethod(nameof(GetValuesWithArray), BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic)!
                .MakeGenericMethod(type!);
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType.IsImplementType(typeof(IList<>)))
        {
            var type = property.PropertyType.GetGenericArguments().First();
            var methodInfo = typeof(FromQuery<TQuery>)
                .GetMethod(nameof(GetValuesWithList), BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic)!
                .MakeGenericMethod(type);
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else
        {
            throw new ArgumentException($"Unsupported property type '{property.PropertyType}'.");
        }

        return convertedValue;

        Expression GetSingleValue()
        {
            return Expression.ArrayIndex(value, Expression.Constant(0));
        }
    }

    static TResult[] GetValuesWithArray<TResult>(IEnumerable<string> values)
        => ConvertUtils.GetValues<TResult>(values).ToArray();

    static List<TResult> GetValuesWithList<TResult>(IEnumerable<string> values)
        => ConvertUtils.GetValues<TResult>(values).ToList();
}

#endif
