// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Collections.Generic;

internal static class AssemblyHelper
{
    public static IEnumerable<Type> GetJobTypes(this IEnumerable<Assembly> assemblies, Type backgroundJobType)
    {
        return assemblies
            .SelectMany(assembly => assembly.DefinedTypes)
            .Where(type => type is { IsAbstract: false, IsInterface: false, IsClass: true, IsGenericType: false } && backgroundJobType
                .GetInterfaces().Any(interfaceType =>
                {
                    var current = interfaceType.GetTypeInfo().IsGenericType ? interfaceType.GetGenericTypeDefinition() : interfaceType;
                    return current == backgroundJobType;
                }));
    }
}
