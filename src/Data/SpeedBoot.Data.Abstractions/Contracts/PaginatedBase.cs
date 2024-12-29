// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Data.Abstractions;

public class PaginatedBase
{
    private int _page = 1;

    public virtual int Page
    {
        get => _page;
        set
        {
            SpeedArgumentException.ThrowIfLessThan(value, 1, nameof(Page));
            _page = value;
        }
    }

    private int _pageSize = 20;

    public virtual int PageSize
    {
        get => _pageSize;
        set
        {
            SpeedArgumentException.ThrowIfLessThan(value, 1, nameof(PageSize));
            _pageSize = value;
        }
    }
}
