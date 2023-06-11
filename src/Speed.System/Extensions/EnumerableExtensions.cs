// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System;

public static class EnumerableExtensions
{
    public static IEnumerable<TSource> WhereIfNotNull<TSource>(
        this IEnumerable<TSource> source,
        Func<TSource, bool>? predicate)
        => source.WhereIf(true, predicate);

    public static IEnumerable<TSource> WhereIf<TSource>(
        this IEnumerable<TSource> source,
        bool isCompose,
        Func<TSource, bool>? predicate)
    {
        return isCompose && predicate != null ? source.Where(predicate) : source;
    }

    public static IEnumerable<TSource> WhereIf<TSource>(
        this IEnumerable<TSource> source,
        bool isCompose,
        Expression<Func<TSource, bool>?> predicate)
    {
        return source.WhereIf(isCompose, predicate.Compile());
    }
}
