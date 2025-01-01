// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.EFCore.Options;

/// <summary>
/// Only when inheriting SpeedDbContext<TDbContext>, can the default behavior of Filter be modified
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public class FilterOptions<TDbContext> : Abstractions.Options.FilterOptions<TDbContext>
    where TDbContext : IDbContext
{
}
