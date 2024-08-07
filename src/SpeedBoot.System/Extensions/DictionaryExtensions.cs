﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Collections.Generic;

public static class DictionaryExtensions
{
#if NETSTANDARD2_0
    public static TDictionary TryAdd<TKey, TValue, TDictionary>(this TDictionary data, TKey key, TValue value)
        where TDictionary : IDictionary<TKey, TValue>
    {
        if (!data.ContainsKey(key))
        {
            data.Add(key, value);
        }

        return data;
    }
#endif

    public static dynamic ToDynamic<TValue>(this IDictionary<string, TValue> data)
    {
        IDictionary<string, object?> expand = new ExpandoObject();
        foreach (var item in data)
        {
            expand[item.Key] = item.Value;
        }

        return expand;
    }
}
