// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore;

[AttributeUsage(AttributeTargets.Method)]
public class HttpPutAttribute : HttpMethodAttribute
{
    public HttpPutAttribute() : base([nameof(HttpMethod.Put)])
    {
    }

    public HttpPutAttribute(string template) : base(template, nameof(HttpMethod.Put))
    {

    }
}
