// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if !(NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1)
// ReSharper disable once CheckNamespace
namespace System.Linq;

public static class StringExtension
{
    public static bool Contains(this string str, string value, StringComparison stringComparison)
        => str.IndexOf(value, stringComparison) >= 0;
}
#endif
