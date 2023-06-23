// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System.Linq;

public static class StringExtension
{
#if !(NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1)
    public static bool Contains(this string str, string value, StringComparison stringComparison)
        => str.IndexOf(value, stringComparison) >= 0;
#endif

    public static bool IsNullOrWhiteSpace(
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(false)]
#endif
        this string? value)
        => string.IsNullOrWhiteSpace(value);

    public static bool IsNullOrEmpty(
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(false)]
#endif
        this string? value)
        => string.IsNullOrEmpty(value);

    public static string TrimStart(this string value, string trimParameter)
        => value.TrimStart(trimParameter, StringComparison.CurrentCulture);

    public static string TrimStart(this string value,
        string trimParameter,
        StringComparison stringComparison)
    {
        if (!value.StartsWith(trimParameter, stringComparison))
            return value;

        return value.Substring(trimParameter.Length);
    }

    public static string TrimEnd(this string value, string trimParameter)
        => value.TrimEnd(trimParameter, StringComparison.CurrentCulture);

    public static string TrimEnd(this string value,
        string trimParameter,
        StringComparison stringComparison)
    {
        if (!value.EndsWith(trimParameter, stringComparison))
            return value;

        return value.Substring(0, value.Length - trimParameter.Length);
    }
}
