// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System;

public static class EnumerableExtensions
{
    public static IEnumerable<TSource> WhereIfNotNull<TSource>(
        this IEnumerable<TSource> source,
        Func<TSource, bool>? predicate)
        => EnumerableUtils.WhereIf(source, true, predicate);

    public static IEnumerable<TSource> WhereIf<TSource>(
        this IEnumerable<TSource> source,
        bool isCompose,
        Func<TSource, bool>? predicate)
        => EnumerableUtils.WhereIf(source, isCompose, predicate);

    public static IEnumerable<TSource> WhereIf<TSource>(
        this IEnumerable<TSource> source,
        bool isCompose,
        Expression<Func<TSource, bool>?> predicate)
        => EnumerableUtils.WhereIf(source, isCompose, predicate);

    public static bool IsAny<TSource>(
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        this IEnumerable<TSource>? sources)
        => sources != null && sources.Any();
}
