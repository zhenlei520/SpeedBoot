// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore;

[AttributeUsage(AttributeTargets.Method)]
public class HttpPostAttribute : HttpMethodAttribute
{
    public HttpPostAttribute() : base([nameof(HttpMethod.Post)])
    {
    }

    public HttpPostAttribute(string template) : base(template, nameof(HttpMethod.Post))
    {

    }
}
