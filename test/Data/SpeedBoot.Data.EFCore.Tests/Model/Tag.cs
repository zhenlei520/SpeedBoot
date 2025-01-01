// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.EFCore.Tests.Model;

public class Tag : Entity<Guid>, ISoftDelete
{
    public string Name { get; set; }

    public bool IsDeleted { get; set; }
}
