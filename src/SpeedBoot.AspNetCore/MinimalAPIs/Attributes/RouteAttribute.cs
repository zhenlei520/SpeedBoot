// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER

namespace SpeedBoot.AspNetCore;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class RouteAttribute : Attribute
{
    public string Template { get; }

    public RouteAttribute(string template)
    {
        SpeedArgumentException.ThrowIfNullOrWhiteSpace(template);
        Template = template;
    }
}

#endif
