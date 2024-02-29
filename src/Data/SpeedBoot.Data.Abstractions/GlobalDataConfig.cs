// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Data.Abstractions;

public static class GlobalDataConfig
{
    public static List<Type> DbContextTypes = new();

    public static void RegisterDbContext<TDbContext>()
    {
        var dbContextType = typeof(TDbContext);
        if (DbContextTypes.Contains(dbContextType))
        {
            DbContextTypes.Add(dbContextType);
        }
    }
}
