// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore;

[AttributeUsage(AttributeTargets.Method)]
public class HttpGetAttribute : HttpMethodAttribute
{
    public HttpGetAttribute() : base([nameof(HttpMethod.Get)])
    {
    }

    public HttpGetAttribute(string template) : base(template, nameof(HttpMethod.Get))
    {

    }
}
