﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using System.ComponentModel;
using System.Reflection;

namespace System;

internal static class FieldExtensions
{
    public static string GetDescription(this FieldInfo field)
    {
        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        return attribute?.Description ?? string.Empty;
    }
}
