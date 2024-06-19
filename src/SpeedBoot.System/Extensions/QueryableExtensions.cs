// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System;

public static class QueryableExtensions
{
    public static IQueryable<TSource> WhereIf<TSource>(
        this IQueryable<TSource> source,
        bool isCompose,
        Expression<Func<TSource, bool>> predicate)
        => isCompose ? source.Where(predicate) : source;
}
