// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System;

internal static partial class AssemblyUtils
{
    public static List<Type> GetServiceComponentTypes(params Assembly[] assemblies)
    {
        return AssemblyUtils.GetTypes(assemblies, type => type is { IsClass: true, IsAbstract: false } && type.IsSubclassOf(typeof(IServiceComponent)));
    }
}
