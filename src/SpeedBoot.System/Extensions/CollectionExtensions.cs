// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Collections.Generic;

public static class CollectionExtensions
{
#if NETSTANDARD2_0

    public static TValue GetValueOrDefault<TKey, TValue>(
        this IReadOnlyDictionary<TKey, TValue> dictionary,
        TKey key)
        where TKey : notnull
    {
        return dictionary.GetValueOrDefault<TKey, TValue>(key, default(TValue));
    }

    public static TValue GetValueOrDefault<TKey, TValue>(
        this IReadOnlyDictionary<TKey, TValue> dictionary,
        TKey key,
        TValue defaultValue)
        where TKey : notnull
    {
        if (dictionary == null)
            throw new ArgumentNullException(nameof(dictionary));
        TValue obj;
        return !dictionary.TryGetValue(key, out obj) ? defaultValue : obj;
    }
#endif
}
