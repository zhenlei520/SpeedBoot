// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System.Linq;

public static class EnumerableExtension
{
    public static List<Type> GetTypes(this IEnumerable<Assembly> assemblies)
        => assemblies.GetTypes(null);

    public static List<Type> GetTypes(this IEnumerable<Assembly> assemblies, Expression<Func<Type, bool>>? condition)
    {
        if (condition == null) return assemblies.SelectMany(assembly => assembly.GetTypes()).ToList();

        return assemblies.SelectMany(assembly => assembly.GetTypes()).Where(condition.Compile()).ToList();
    }


}
