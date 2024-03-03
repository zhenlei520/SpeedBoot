// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER
namespace SpeedBoot.AspNetCore.Internal;

internal static class AssemblyHelper
{
#if NET7_0_OR_GREATER
    public static IEnumerable<Type> GetActionFilterProviders(IEnumerable<Assembly> assemblies)
        => from type in assemblies.SelectMany(assembly => assembly.GetTypes())
            where type.IsClass && !type.IsAbstract && typeof(IActionFilterProvider).IsAssignableFrom(type)
            select type;
#endif

    public static IEnumerable<Type> GetServiceTypes(IEnumerable<Assembly> assemblies)
        => from type in assemblies.SelectMany(assembly => assembly.GetTypes())
            where !type.IsAbstract && BaseOf<ServiceBase>(type)
            select type;

    private static bool BaseOf<T>(Type type)
    {
        if (type.BaseType == typeof(T)) return true;

        return type.BaseType != null && BaseOf<T>(type.BaseType);
    }
}

#endif
