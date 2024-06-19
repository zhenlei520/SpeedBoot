// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Tests.Applications.Requests;

public class UserItemQuery : FromQuery<UserItemQuery>
{
    public string Name { get; set; }

    public int Id { get; set; }

    public List<string>? Tags { get; set; }

    public string[]? Tags2 { get; set; }

    public int[] Ids { get; set; }

    public FileMode FileMode { get; set; }

    public List<UserItemQuery> Items { get; set; }
}
