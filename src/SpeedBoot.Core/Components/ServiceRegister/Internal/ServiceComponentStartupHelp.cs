// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

internal static class ServiceComponentStartupHelp
{
    public static List<Type> GetComponentTypesByOrdered(List<Type> allComponentTypes)
    {
        var typesByNotDependType = GetComponentTypeByNotDependType(allComponentTypes);
        return typesByNotDependType.Count == 0 ? new List<Type>() : GetComponentTypesByOrdered(allComponentTypes, typesByNotDependType, 1);
    }

    private static List<Type> GetComponentTypesByOrdered(List<Type> allComponentTypes, List<Type> existTypes, int executeTimes)
    {
        List<Type> types = new List<Type>(existTypes);
        var typesByOrdered = allComponentTypes.Except(existTypes); //Types of pending sorts
        foreach (var type in typesByOrdered)
        {
            if (!IsAnyDepend(type, out var dependTypes))
            {
                if (!existTypes.Contains(type))
                {
                    existTypes.Add(type);
                }
            }
            else
            {
                bool isExist = true;
                foreach (var dependType in dependTypes!)
                {
                    if (!types.Contains(dependType))
                    {
                        isExist = false;
                    }
                }
                if (isExist)
                    types.Add(type);
            }
        }

        if (types.Count == allComponentTypes.Count)
            return types;

        if (executeTimes >= allComponentTypes.Count)
            throw new SpeedFriendlyException("Please check for circular dependencies");

        return GetComponentTypesByOrdered(allComponentTypes, types, executeTimes + 1);
    }

    /// <summary>
    /// get a type that doesn't depend on any component
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    private static List<Type> GetComponentTypeByNotDependType(List<Type> types)
        => types.Where(type => !IsAnyDepend(type, out _)).ToList();

    /// <summary>
    /// Are there dependencies
    /// </summary>
    /// <param name="type">service component Type</param>
    /// <param name="dependTypes">Dependent Service Component Type</param>
    /// <returns></returns>
    private static bool IsAnyDepend(Type type, out Type[]? dependTypes)
    {
        dependTypes = null;
        var dependsOnAttribute = type.GetCustomAttribute<DependsOnAttribute>();
        if (dependsOnAttribute == null || dependsOnAttribute.DependComponentTypes.Length == 0)
            return false;

        dependTypes = dependsOnAttribute.DependComponentTypes;
        return true;
    }
}
