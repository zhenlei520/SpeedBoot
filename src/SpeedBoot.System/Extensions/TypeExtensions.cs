// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace System;

public static class TypeExtensions
{
    public static ConstructorInfo? GetConstructor(this Type instanceType, BindingFlags bindingAttr, params Type[] parameterTypes)
    {
        var constructors = instanceType.GetConstructors(bindingAttr);
        foreach (var constructor in constructors)
        {
            var parameterTypeList = constructor.GetParameters().Select(parameterInfo => parameterInfo.ParameterType).ToList();
            if (parameterTypeList.Equals(parameterTypes))
                return constructor;
        }
        return null;
    }

    public static ConstructorInfo GetRequestConstructor(this Type instanceType, BindingFlags bindingAttr = Constant.DefaultBindingFlags)
        => GetRequestConstructor(instanceType, bindingAttr, Type.EmptyTypes);

    public static ConstructorInfo GetRequestConstructor(this Type instanceType, BindingFlags bindingAttr, params Type[] parameterTypes)
    {
        var constructor = instanceType.GetConstructor(bindingAttr, parameterTypes);
        SpeedArgumentException.ThrowIfNull(constructor);
        return constructor!;
    }
}
