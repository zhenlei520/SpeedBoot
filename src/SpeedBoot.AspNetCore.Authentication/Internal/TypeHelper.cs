// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using IConvertible = System.IConvertible;

[assembly: InternalsVisibleTo("SpeedBoot.AspNetCore.Authentication.Tests")]

namespace SpeedBoot.AspNetCore.Authentication;

internal static class TypeHelper
{
    public static bool TryConvertTo(string value, Type type, out object? val)
    {
        val = null;
        if (!typeof(IConvertible).IsAssignableFrom(type) || string.IsNullOrWhiteSpace(value))
            return false;

        if (type == typeof(DateTime))
            val = DateTime.Parse(value);
        else if (type == typeof(Guid))
            val = Guid.Parse(value);
        else if (type == typeof(bool))
        {
            if (value.TryToBool(out var res))
                val = res;
        }
        else
            val = Convert.ChangeType(value, type);
        return true;
    }
}
