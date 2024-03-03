// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class HttpMethodAttribute : Attribute
{
    public string[] HttpMethods { get; set; }

    public string? Template { get; set; }

    public HttpMethodAttribute(string[] httpMethods)
    {
        HttpMethods = httpMethods;
    }

    public HttpMethodAttribute(string template, params string[] httpMethods) : this(httpMethods)
    {
        SpeedArgumentException.ThrowIfNullOrWhiteSpace(template);
        Template = template;
    }
}
