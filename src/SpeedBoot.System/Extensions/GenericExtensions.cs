// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Extensions;

public static class GenericExtensions
{
    public static TParameter Set<TParameter>(this TParameter parameter, TParameter? value)
    {
        if (value != null)
            return value;
        return parameter;
    }

    public static TParameter Set<TParameter>(this TParameter parameter, TParameter? value, Func<TParameter> func)
    {
        return value ?? func.Invoke();
    }
}
