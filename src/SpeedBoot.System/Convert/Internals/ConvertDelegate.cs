// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

[assembly: InternalsVisibleTo("SpeedBoot.System.Tests")]
namespace SpeedBoot.System;

internal class ConvertDelegate<TValue>
{
    private static Lock _lock = LockFactory.Create();
    private static Func<string, TValue>? _parseFunc;

    public static Func<string, TValue> GetConvertToDelegate()
    {
        if (_parseFunc != null)
            return _parseFunc;

        lock (_lock)
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

        if (valueType.IsEnum || valueType.IsNullableType() && Nullable.GetUnderlyingType(valueType)!.IsEnum)
        {
            Expression body;

            if (valueType.IsEnum)
            {
                body = Expression.Convert(
                    Expression.Call(
                        typeof(Enum).GetMethod("Parse", new[] { typeof(Type), typeof(string), typeof(bool) })!,
                        Expression.Constant(valueType),
                        inputParam,
                        Expression.Constant(true)
                    ),
                    valueType
                );
            }
            else
            {
                var underlyingType = Nullable.GetUnderlyingType(valueType)!;
                var parseEnum = Expression.Convert(
                    Expression.Call(
                        typeof(Enum).GetMethod("Parse", new[] { typeof(Type), typeof(string), typeof(bool) })!,
                        Expression.Constant(underlyingType),
                        inputParam,
                        Expression.Constant(true)
                    ),
                    underlyingType
                );

                var isNullOrWhiteSpaceMethod = typeof(string).GetMethod(nameof(string.IsNullOrWhiteSpace), new[] { typeof(string) });
                var isNullOrWhiteSpaceCall = Expression.Call(isNullOrWhiteSpaceMethod!, inputParam);

                body = Expression.Condition(
                    isNullOrWhiteSpaceCall,
                    Expression.Default(valueType),
                    Expression.Convert(parseEnum, valueType)
                );
            }

            var lambda = Expression.Lambda<Func<string, TValue>>(body, inputParam);
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
