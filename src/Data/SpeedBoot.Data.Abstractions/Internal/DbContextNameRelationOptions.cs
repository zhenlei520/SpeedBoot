// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Data.Abstractions;

internal class DbContextNameRelationOptions
{
    public string Name { get; }

    public Type DbContextType { get; }

    public DbOperateTypes DbOperateType { get; set; }

    public DbContextNameRelationOptions(string name, Type dbContextType)
    {
        Name = name;
        DbContextType = dbContextType;
    }
}
