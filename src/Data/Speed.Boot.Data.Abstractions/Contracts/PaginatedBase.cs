// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed.Boot.Data.Abstractions;

public class PaginatedBase
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}
