// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed.Extensions.DependencyInjection;

internal static class TypeUtils
{
    public static List<Type> GetServiceTypes(this List<Type> types, Type interfaceType)
    {
        var serviceTypesByInterface = types.Where(t => t.IsInterface && t != interfaceType && interfaceType.IsAssignableFrom(t));
        var serviceTypesByClass = types.Where(type =>
            !type.IsSkip() &&
            IsAssignableFrom(interfaceType, type) &&
            !interfaceServiceTypes.Any(t => IsAssignableFrom(t, type)));

        return new List<Type>(serviceTypesByInterface).Concat(serviceTypesByClass).ToList();
    }

    private static bool IsSkip(this Type type)
    {
        if ((type.IsClass&&type.IsAbstract))
            return true;

        var ignoreInjection = type.GetCustomAttribute<IgnoreInjectionAttribute>();
        if (ignoreInjection == null)
            return false;

        var inheritIgnoreInjection = type.GetCustomAttribute<IgnoreInjectionAttribute>(false);
        return inheritIgnoreInjection != null || ignoreInjection.Cascade;
    }
}
