// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.Abstractions;

public class SaveSucceedDbContextEventData : DbContextEventDataBase
{
    public DbTransaction? DbTransaction { get; set; }

    public Guid? ConnectionId { get; set; }
}
