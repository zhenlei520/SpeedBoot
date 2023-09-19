// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed.Boot.Data.Abstractions;

public class PaginatedOptions: PaginatedBase
{
    public Dictionary<string, bool>? Sorting { get; set; }

    public PaginatedOptions()
    {
    }

    public PaginatedOptions(int page, int pageSize, Dictionary<string, bool>? sorting = null)
    {
        Page = page;
        PageSize = pageSize;
        Sorting = sorting;
    }

    /// <summary>
    /// Initialize a new instance of PaginatedOptions.
    /// </summary>
    /// <param name="page">page number</param>
    /// <param name="pageSize">returns per page</param>
    /// <param name="sortField">sort field name</param>
    /// <param name="isDescending">true descending order, false ascending order, default: true</param>
    public PaginatedOptions(int page, int pageSize, string sortField, bool isDescending = true)
        : this(page, pageSize, new KeyValuePair<string, bool>(sortField, isDescending).ToDictionary())
    {
        ;
    }
}
