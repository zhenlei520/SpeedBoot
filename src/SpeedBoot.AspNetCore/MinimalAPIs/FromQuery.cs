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
    static CustomConcurrentDictionary<Type, Dictionary<(string Name, Type Type), Action<TQuery, object>>> _setPropertyValueData = new();

    static FromQuery()
    {
        var queryType = typeof(TQuery);
        _setPropertyValueData.TryAdd(queryType, type =>
        {
            var list = new Dictionary<(string, Type), Action<TQuery, object>>();
            foreach (var propertyInfo in type.GetProperties())
            {
                if (!propertyInfo.CanWrite)
                    continue;

                var action = InstanceExpressionUtils.SetPropertyValue<TQuery>(propertyInfo);
                list.Add((propertyInfo.Name, propertyInfo.PropertyType), action);
            }

            return list;
        });
    }

    public static ValueTask<TQuery?> BindAsync(HttpContext context)
    {
        var instance = new TQuery();
        var setPropertyValueData = _setPropertyValueData[typeof(TQuery)];
        foreach (var item in setPropertyValueData)
        {
            if (!context.Request.Query.TryGetValue(item.Key.Name, out var value))
                continue;

            object? parameterValue = null;
            switch (value.Count)
            {
                case 0:
                    continue;
                case 1:
                    var str = value.ToString();
                    if (str.TryConvertTo(item.Key.Type, out parameterValue))
                        item.Value.Invoke(instance, parameterValue);
                    continue;
                default:
                    if (value.TryConvertTo(item.Key.Type, out parameterValue))
                        item.Value.Invoke(instance, parameterValue);
                    break;
            }
        }

        return ValueTask.FromResult(instance);
    }
}

#endif
