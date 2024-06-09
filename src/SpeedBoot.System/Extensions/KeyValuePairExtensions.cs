// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.System.Collections.Generic;

public static class KeyValuePairExtensions
{
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this KeyValuePair<TKey, TValue> keyValuePair)
        where TKey : notnull
        => new[] { keyValuePair }.ToDictionary();
}
