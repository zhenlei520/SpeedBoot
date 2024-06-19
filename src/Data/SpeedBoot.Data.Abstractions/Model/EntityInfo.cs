// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.Abstractions;

public class EntityInfo
{
    public EntityState? EntityState { get; set; }

    public Type EntityType { get; set; }

    public List<EntityPropertyInfo> PropertyInfos { get; set; }
}
