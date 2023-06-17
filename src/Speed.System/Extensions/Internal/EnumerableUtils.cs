// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

using System.Linq.Expressions;

namespace System;

public static class EnumerableUtils
{
    public static IEnumerable<TSource> WhereIfNotNull<TSource>(
        IEnumerable<TSource> source,
        Func<TSource, bool>? predicate)
        => WhereIf(source, true, predicate);

    public static IEnumerable<TSource> WhereIf<TSource>(
        IEnumerable<TSource> source,
        bool isCompose,
        Func<TSource, bool>? predicate)
    {
        return isCompose && predicate != null ? source.Where(predicate) : source;
    }

    public static IEnumerable<TSource> WhereIf<TSource>(
        IEnumerable<TSource> source,
        bool isCompose,
        Expression<Func<TSource, bool>?> predicate)
    {
        return WhereIf(source, isCompose, predicate.Compile());
    }
}
