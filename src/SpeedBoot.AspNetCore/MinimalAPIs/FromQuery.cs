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
        SetPropertyValueData =
            typeof(TQuery).GetProperties().ToDictionary(p => p.Name, BuildSetterValue, StringComparer.OrdinalIgnoreCase);
    }

    public static ValueTask<TQuery?> BindAsync(HttpContext context)
    {
        var queryCollection = context.Request.Query;
        var query = new TQuery();
        foreach (var item in queryCollection)
        {
            if (item.Value.Count == 0)
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
        else if (property.PropertyType == typeof(short) || property.PropertyType == typeof(short?))
            convertedValue = Expression.Call(typeof(short).GetMethod(nameof(short.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
            convertedValue = Expression.Call(typeof(int).GetMethod(nameof(int.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(long) || property.PropertyType == typeof(long?))
            convertedValue = Expression.Call(typeof(long).GetMethod(nameof(long.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(float) || property.PropertyType == typeof(float?))
            convertedValue = Expression.Call(typeof(float).GetMethod(nameof(float.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
            convertedValue = Expression.Call(typeof(decimal).GetMethod(nameof(decimal.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(double) || property.PropertyType == typeof(double?))
            convertedValue = Expression.Call(typeof(double).GetMethod(nameof(double.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
            convertedValue = Expression.Call(typeof(bool).GetMethod(nameof(bool.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(char) || property.PropertyType == typeof(long?))
            convertedValue = Expression.Call(typeof(char).GetMethod(nameof(char.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(byte) || property.PropertyType == typeof(byte?))
            convertedValue = Expression.Call(typeof(byte).GetMethod(nameof(byte.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(sbyte) || property.PropertyType == typeof(sbyte?))
            convertedValue = Expression.Call(typeof(sbyte).GetMethod(nameof(sbyte.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(ushort) || property.PropertyType == typeof(ushort?))
            convertedValue = Expression.Call(typeof(ushort).GetMethod(nameof(ushort.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(uint) || property.PropertyType == typeof(uint?))
            convertedValue = Expression.Call(typeof(uint).GetMethod(nameof(uint.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(ulong) || property.PropertyType == typeof(ulong?))
            convertedValue = Expression.Call(typeof(ulong).GetMethod(nameof(ulong.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(Guid) || property.PropertyType == typeof(Guid?))
            convertedValue = Expression.Call(typeof(Guid).GetMethod(nameof(Guid.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
            convertedValue = Expression.Call(typeof(DateTime).GetMethod(nameof(DateTime.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(DateOnly) || property.PropertyType == typeof(DateOnly?))
            convertedValue = Expression.Call(typeof(DateOnly).GetMethod(nameof(DateOnly.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(TimeOnly) || property.PropertyType == typeof(TimeOnly?))
            convertedValue = Expression.Call(typeof(TimeOnly).GetMethod(nameof(TimeOnly.Parse), [typeof(string)]), GetSingleValue());
        else if (property.PropertyType == typeof(List<string>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToStringList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<short>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToShortList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<int>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToIntList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<long>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToLongList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<float>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToFloatList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<decimal>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToDecimalList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<double>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToDoubleList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<bool>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToBoolList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<char>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToCharList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<byte>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToByteArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<sbyte>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToSByteList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<ushort>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToUShortList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<uint>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToUIntList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<ulong>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToULongList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<Guid>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToGuidList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<DateTime>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToDateTimeList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<DateOnly>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToDateOnlyList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(List<TimeOnly>))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToTimeOnlyList)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(string[]))
            convertedValue = value;
        else if (property.PropertyType == typeof(short[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToShortArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(int[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToIntArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(long[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToLongArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(float[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToFloatArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(decimal[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToDecimalArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(double[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToDoubleArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(bool[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToBoolArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(char[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToCharArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(byte[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToByteArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(sbyte[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToSByteArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(ushort[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToUShortArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(uint[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToUIntArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(ulong[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToULongArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(Guid[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToGuidArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(DateTime[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToDateTimeArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(DateOnly[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToDateOnlyArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        else if (property.PropertyType == typeof(TimeOnly[]))
        {
            var methodInfo = typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m => m.Name.Equals(nameof(EnumerableExtensions.ToTimeOnlyArray)));
            convertedValue = Expression.Call(null, methodInfo, value);
        }
        // else if (property.PropertyType.IsImplementType(typeof(IEnumerable<>)))
        // {
        //     var methodInfo= typeof(EnumerableExtensions).GetMethods().FirstOrDefault(m=>m.Name.Equals(nameof(EnumerableExtensions.ConvertTo)));
        //     convertedValue = Expression.Call(null, methodInfo, value, Expression.Constant(property.PropertyType));
        //     convertedValue = Expression.Convert(convertedValue, property.PropertyType);
        // }
        else
        {
            throw new ArgumentException($"Unsupported property type '{property.PropertyType}'.");
        }

        if (property.PropertyType.IsNullableType())
            convertedValue = Expression.Convert(convertedValue, property.PropertyType);

        return convertedValue;

        Expression GetSingleValue()
        {
            return Expression.ArrayIndex(value, Expression.Constant(0));
        }
    }
}

#endif
