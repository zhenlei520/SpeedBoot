// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

[AttributeUsage(AttributeTargets.Class)]
public class DependsOnAttribute : Attribute
{
    public Type[] DependComponentTypes { get; set; }

    public DependsOnAttribute(params Type[] dependComponentTypes)
    {
        DependComponentTypes = dependComponentTypes;
    }
}
