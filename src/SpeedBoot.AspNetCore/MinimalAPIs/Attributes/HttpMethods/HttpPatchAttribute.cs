// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore;

[AttributeUsage(AttributeTargets.Method)]
public class HttpPatchAttribute : HttpMethodAttribute
{
    public HttpPatchAttribute() : base([nameof(HttpMethod.Patch)])
    {
    }

    public HttpPatchAttribute(string template) : base(template, nameof(HttpMethod.Patch))
    {

    }
}
