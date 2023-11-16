// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.FreeSql.Tests.Model;

public class Person : Entity<Guid>
{
    [Column(IsPrimary = true)]
    public override Guid Id { get; set; }

    [Column(IsPrimary = true)]
    public string Name { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }
}
