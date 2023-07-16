// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System;

internal static class AssemblyUtils
{
    public static List<Type> GetTypes(IEnumerable<Assembly> assemblies)
        => GetTypes(assemblies, null);

    public static List<Type> GetTypes(IEnumerable<Assembly> assemblies, Func<Type, bool>? condition)
    {
        var types = assemblies.SelectMany(assembly => assembly.GetTypes());
        return EnumerableUtils.WhereIfNotNull(types, condition)?.Distinct().ToList() ?? new List<Type>();
    }
}
