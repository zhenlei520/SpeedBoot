// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Data.Abstractions;

public static class GlobalDataConfig
{
    private static List<Type> _dbContextTypes = new();
    public static IReadOnlyList<Type> DbContextTypes => _dbContextTypes;

    public static ConnectionStringInfo ConnectionString = new ConnectionStringInfo()
    {
        DefaultSection = "ConnectionStrings",
        DefaultConnectionStringName = "DefaultConnection"
    };

    public static void RegisterDbContext<TDbContext>()
    {
        var dbContextType = typeof(TDbContext);
        if (!DbContextTypes.Contains(dbContextType))
        {
            _dbContextTypes.Add(dbContextType);
        }
    }
}
