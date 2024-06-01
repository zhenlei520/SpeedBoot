// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.Abstractions;

public abstract class DbContextEventDataBase
{
    public string EventId { get; set; }

    public string EventName { get; set; }

    public string ContextId { get; set; }

    public List<EntityInfo> Entites { get; set; }
}
