// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value, string defaultValue = "")
    {
        value.TryGetDescription(out var description, defaultValue);
        return description;
    }

    public static bool TryGetDescription(this Enum value, out string description, string defaultValue = "")
    {
        var attribute = value.GetCustomAttribute<DescriptionAttribute>();
        if (attribute == null)
        {
            description = defaultValue;
            return false;
        }
        description = attribute.Description;
        return true;
    }

    public static TAttribute? GetCustomAttribute<TAttribute>(this Enum value, bool inherit = false) where TAttribute : Attribute
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = fieldInfo.GetCustomAttribute(typeof(TAttribute), inherit);
        return attributes as TAttribute;
    }

    public static TAttribute[] GetCustomAttributes<TAttribute>(this Enum value, bool inherit = false) where TAttribute : Attribute
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = fieldInfo.GetCustomAttributes(typeof(TAttribute), inherit);
        return attributes.Select(attribute => (attribute as TAttribute)!).ToArray();
    }
}
