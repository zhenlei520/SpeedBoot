// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace


namespace System;

public static class AssemblyExtensions
{
    public static List<Type> GetTypes(this IEnumerable<Assembly> assemblies)
        => assemblies.GetTypes(null);

    public static List<Type> GetTypes(this IEnumerable<Assembly> assemblies, Func<Type, bool>? condition)
    {
        return assemblies.SelectMany(assembly => assembly.GetTypes()).WhereIfNotNull(condition).Distinct().ToList();
    }
}
