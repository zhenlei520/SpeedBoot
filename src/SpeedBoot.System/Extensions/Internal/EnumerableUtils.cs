// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System;

internal static class EnumerableUtils
{
    public static IEnumerable<TSource>? WhereIfNotNull<TSource>(
        IEnumerable<TSource>? source,
        Func<TSource, bool>? predicate)
        => WhereIf(source, source != null, predicate);

    public static IEnumerable<TSource>? WhereIf<TSource>(
        IEnumerable<TSource>? source,
        bool isCompose,
        Func<TSource, bool>? predicate)
        => isCompose && predicate != null ? source!.Where(predicate) : source;
}
