// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class RoutePatternAttribute : Attribute
{
    public string? HttpMethod { get; set; }

    public string? Pattern { get; set; } = default!;

    public bool IgnorePrefix { get; set; } = false;
}
