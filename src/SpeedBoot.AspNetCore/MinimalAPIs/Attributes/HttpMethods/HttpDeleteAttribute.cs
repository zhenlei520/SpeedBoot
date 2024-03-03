// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore;

[AttributeUsage(AttributeTargets.Method)]
public class HttpDeleteAttribute : HttpMethodAttribute
{
    public HttpDeleteAttribute() : base([nameof(HttpMethod.Delete)])
    {
    }

    public HttpDeleteAttribute(string template) : base(template, nameof(HttpMethod.Delete))
    {

    }
}
