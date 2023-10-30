// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System;

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
}
