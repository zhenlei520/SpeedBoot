// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.Abstractions;

public class EntityPropertyInfo
{
    public bool IsPrimaryKey { get; set; }

    public Type PropertyType { get; set; }

    public string PropertyName { get; set; }

    public object? OldValue { get; set; }

    public object? NewValue { get; set; }
}
