// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER

namespace SpeedBoot.AspNetCore;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class RouteIgnoreAttribute : Attribute
{
}

#endif
