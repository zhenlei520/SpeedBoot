// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Benchmarks.Queries;

public class UserQuery: FromQuery<UserQuery>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool? Gender { get; set; }

    public string[] Tags { get; set; }

    public List<DateTime?> Times { get; set; }
}
