// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System;

internal class ConvertToDelegate<TValue>
{
    private static object _obj = new { };
    private static Func<string, TValue>? _parseFunc;

    public static Func<string, TValue> GetConvertToDelegate()
    {
        if (_parseFunc != null)
            return _parseFunc;

        lock (_obj)
        {
            _parseFunc ??= BuildDelegate();
            return _parseFunc;
        }
    }

    private static Func<string, TValue> BuildDelegate()
    {
        var valueType = typeof(TValue);
        var inputParam = Expression.Parameter(typeof(string), "input");

        if (valueType == typeof(string))
        {
            var lambda = Expression.Lambda<Func<string, TValue>>(inputParam, inputParam);
            return lambda.Compile();
        }

        var isNullable = valueType.IsNullableType();
        var nonNullableType = isNullable ? Nullable.GetUnderlyingType(valueType)! : valueType;

        if (isNullable)
        {
            var parseMethod = nonNullableType.GetMethod("Parse", new[] { typeof(string) }) ??
                              throw new SpeedException($"No 'Parse' method available for type {nonNullableType}");

            var isNullOrWhiteSpaceMethod = typeof(string).GetMethod(nameof(string.IsNullOrWhiteSpace), new[] { typeof(string) });
            var isNullOrWhiteSpaceCall = Expression.Call(isNullOrWhiteSpaceMethod!, inputParam);
            var callParseMethodExpression = Expression.Call(null, parseMethod, inputParam);
            var conditionExpression = Expression.Condition(
                isNullOrWhiteSpaceCall,
                Expression.Constant(null, valueType),
                Expression.Convert(callParseMethodExpression, valueType)
            );

            return Expression.Lambda<Func<string, TValue>>(conditionExpression, inputParam).Compile();
        }

        var nonNullableParseMethod = nonNullableType.GetMethod("Parse", new[] { typeof(string) }) ??
                                     throw new SpeedException($"No 'Parse' method available for type {nonNullableType}");
        var callNonNullableParseMethodExpr = Expression.Call(null, nonNullableParseMethod, inputParam);
        return Expression.Lambda<Func<string, TValue>>(callNonNullableParseMethodExpr, inputParam).Compile();
    }
}
