// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

using MSystemLinq = System.Linq;

namespace SpeedBoot.System;

public static class EnumerableExtensions
{
    public static IEnumerable<TSource>? WhereIfAny<TSource>(
        this IEnumerable<TSource>? source,
        Func<TSource, bool>? predicate)
        => EnumerableUtils.WhereIf(source, source.IsAny(), predicate);

    public static IEnumerable<TSource>? WhereIfNotNull<TSource>(
        this IEnumerable<TSource>? source,
        Func<TSource, bool>? predicate)
        => EnumerableUtils.WhereIf(source, source != null, predicate);

    public static IEnumerable<TSource>? WhereIf<TSource>(
        this IEnumerable<TSource>? source,
        bool isCompose,
        Func<TSource, bool>? predicate)
        => EnumerableUtils.WhereIf(source, isCompose, predicate);

    public static IEnumerable<TSource>? WhereIf<TSource>(
        this IEnumerable<TSource>? source,
        bool isCompose,
        Expression<Func<TSource, bool>?> predicate)
        => EnumerableUtils.WhereIf(source, isCompose, predicate);

    /// <summary>
    /// is not null and is not an empty collection
    ///
    /// 不是 null 并且 不是空集合
    /// </summary>
    /// <param name="sources"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static bool IsAny<TSource>(
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        this IEnumerable<TSource>? sources)
        => sources != null && sources.Any();

#if !NET8_0_OR_GREATER
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        where TKey : notnull
    {
        Dictionary<TKey, TValue> dictionary;
#if NETSTANDARD2_0
        dictionary = keyValuePairs.ToDictionary(item => item.Key, item => item.Value);
#else
        dictionary = new Dictionary<TKey, TValue>(keyValuePairs);
#endif
        return dictionary;
    }
#endif

    public static bool TryGet<TSource>(
        this IEnumerable<TSource>? sources,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        Expression<Func<TSource, bool>> condition,
        out TSource? result)
    {
        result = default;
        if (sources == null)
            return false;

        var predicate = condition.Compile();
        result = sources.Where(predicate).FirstOrDefault();
        return result != null;
    }

    public static bool Equal<TSource>(
        this IEnumerable<TSource>? source,
        IEnumerable<TSource>? target)
    {
        var sourceList = source?.ToList() ?? [];
        var targetList = target?.ToList() ?? [];
        return sourceList.Count == targetList.Count && sourceList.Where((t, index) => t.Equals(targetList[index])).Any();
    }

    private static CustomConcurrentDictionary<Type, Func<object, object[]?, object>> _arrayInstanceDelegate = new();

    public static bool TryConvertTo(
        this IEnumerable<string> source,
        Type type,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out object? result)
    {
        result = null;
        if (!type.IsImplementType(typeof(IEnumerable<>)))
            return false;

        var singleValueType = type.IsArray ? type.GetElementType() : type.GetGenericArguments()[0];
        var list = ConvertToEnumerable(source, singleValueType);
        if (type.IsArray)
        {
            var func = _arrayInstanceDelegate.GetOrAdd(singleValueType, _ =>
            {
                var instanceType = list.GetType();
                return MethodExpressionUtils.BuildSyncInvokeWithResultDelegate(instanceType,
                    instanceType.GetMethod(nameof(List<string>.ToArray))!);
            });
            result = func.Invoke(list, null);
        }
        else
        {
            result = list;
        }

        return true;
    }

    private static CustomConcurrentDictionary<Type, Func<object>> _instanceDelegate = new();

    private static IList ConvertToEnumerable(this IEnumerable<string> sources, Type singleValueType)
    {
        var func = _instanceDelegate.GetOrAdd(singleValueType,
            (type) => InstanceExpressionUtils.BuildCreateInstanceDelegate(typeof(List<>).MakeGenericType(singleValueType)));
        var list = func.Invoke() as IList;
        if (sources == null)
            return list;

        foreach (var source in sources)
        {
            if (source.TryConvertTo(singleValueType, out var itemValue))
            {
                list.Add(itemValue!);
            }
        }

        return list;
    }
}
