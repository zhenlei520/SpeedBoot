// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System;

public static class ConvertUtils
{
    private static readonly List<Type> _data = new List<Type>()
    {
        typeof(string),
        typeof(short),
        typeof(short?),
        typeof(int),
        typeof(int?),
        typeof(long),
        typeof(long?),
        typeof(float),
        typeof(float?),
        typeof(decimal),
        typeof(decimal?),
        typeof(double),
        typeof(double?),
        typeof(bool),
        typeof(bool?),
        typeof(char),
        typeof(char?),
        typeof(byte),
        typeof(byte?),
        typeof(sbyte),
        typeof(sbyte?),
        typeof(ushort),
        typeof(ushort?),
        typeof(uint),
        typeof(uint?),
        typeof(ulong),
        typeof(ulong?),
        typeof(Guid),
        typeof(Guid?),
        typeof(DateTime),
        typeof(DateTime?),
#if NET6_0_OR_GREATER
        typeof(DateOnly),
        typeof(DateOnly?),
        typeof(TimeOnly),
        typeof(TimeOnly?),
#endif
    };

    public static bool SupportConvertTo(Type type)
        => _data.Contains(type) || type.IsEnum || (type.IsNullableType() && Nullable.GetUnderlyingType(type)!.IsEnum);

    public static bool TryGetValue<TResult>(string value, out TResult? result)
    {
        result = default;
        if (!SupportConvertTo(typeof(TResult)))
            return false;

        result = GetValue<TResult>(value);
        return true;
    }

    public static TResult GetValue<TResult>(string value)
    {
        var func = ConvertDelegate<TResult>.GetConvertToDelegate();
        return func.Invoke(value);
    }

    public static bool TryGetValue<TResult>(IEnumerable<string> values, out IEnumerable<TResult>? result)
    {
        result = default;
        if (!SupportConvertTo(typeof(TResult)))
            return false;

        result = GetValues<TResult>(values);
        return true;
    }

    public static IEnumerable<TResult> GetValues<TResult>(IEnumerable<string> values)
    {
        var func = ConvertDelegate<TResult>.GetConvertToDelegate();
        return values.Select(value => func.Invoke(value));
    }
}
