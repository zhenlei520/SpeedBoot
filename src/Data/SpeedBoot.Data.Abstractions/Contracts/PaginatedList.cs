// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Data.Abstractions;

public class PaginatedList<TEntity>
    where TEntity : class
{
    public long Total { get; set; }

    public long TotalPages { get; set; }

    public List<TEntity> Result { get; set; } = default!;

    public PaginatedList()
    {

    }

    public PaginatedList(long total, int pageSize, List<TEntity> result)
    {
        Total = total;
        SetTotalPages(pageSize);
        Result = result;
    }

    public PaginatedList<TEntity> SetTotalPages(int pageSize)
    {
        TotalPages = (Total + pageSize - 1) / pageSize;
        return this;
    }
}
