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
    static CustomConcurrentDictionary<Type, Func<HttpRequest, TQuery>> _data = new();
    static CustomConcurrentDictionary<Type, Func<HttpRequest, Dictionary<string, object>>> _valueData = new();
    static CustomConcurrentDictionary<Type, Dictionary<string, Action<TQuery, object>>> _setPropertyValueData = new();

    static FromQuery()
    {
        var queryType = typeof(TQuery);
        // _data.TryAdd(queryType, _ => BuildBindDelegate());
        _setPropertyValueData.TryAdd(queryType, type =>
        {
            var list = new Dictionary<string, Action<TQuery, object>>();
            foreach (var propertyInfo in type.GetProperties())
            {
                if (!propertyInfo.CanWrite)
                    continue;

                var action = InstanceExpressionUtils.SetPropertyValue<TQuery>(propertyInfo);
                list.Add(propertyInfo.Name, action);
            }
            return list;
        });
    }

    public static ValueTask<TQuery?> BindAsync(HttpContext context)
    {
        var func = _data[typeof(TQuery)];
        SpeedArgumentException.ThrowIfNull(func);
        return ValueTask.FromResult(func.Invoke(context.Request))!;
    }

    // private static Func<HttpRequest, TQuery> BuildBindDelegate()
    // {
    //     Convert.ChangeType()
    // }

    // private static Func<HttpRequest, TQuery> BuildBindDelegate(HttpRequest httpRequest)
    // {
    //     var instance = new TQuery();
    //
    //     foreach (var keyValuePair in httpRequest.Query)
    //     {
    //         keyValuePair.Key
    //     }
    // }
}

#endif
