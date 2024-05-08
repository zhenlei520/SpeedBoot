// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Tests.Applications.Requests;

public class UserItemQuery
{
    public string Name { get; set; }

    public int Id { get; set; }

    public List<string>? Tags { get; set; }

    // public PageQuery Query { get; set; }
}
public class PageQuery
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}
