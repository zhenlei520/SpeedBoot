// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.EntityFrameworkCore;

internal static class DbContextOptionsExtensions
{

    #region private methods

    private static readonly Func<DbContextOptions, ImmutableSortedDictionary<Type, (IDbContextOptionsExtension Extension, int Ordinal)>>
        Func = InitializeExtensionsMap();

    static Func<DbContextOptions, ImmutableSortedDictionary<Type, (IDbContextOptionsExtension Extension, int Ordinal)>>
        InitializeExtensionsMap()
    {
        var property =
            typeof(DbContextOptions).GetProperty("ExtensionsMap", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var param = Expression.Parameter(typeof(DbContextOptions));
        var body = Expression.Property(param, property);
        var lambda = Expression
            .Lambda<Func<DbContextOptions, ImmutableSortedDictionary<Type, (IDbContextOptionsExtension Extension, int Ordinal)>>>(body,
                param);
        return lambda.Compile();
    }

    #endregion

    public static ImmutableSortedDictionary<Type, (IDbContextOptionsExtension Extension, int Ordinal)> GetExtensionsMap(this DbContextOptions dbContextOptions)
        => Func.Invoke(dbContextOptions);
}
