// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using Speed.Boot.Data.Abstractions;

namespace Speed.Boot.Data.EFCore.Tests.Model;

public class User : Entity<Guid>
{
    public string Name { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }
}
