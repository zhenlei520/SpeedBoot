// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore;

internal class RouteTemplate
{
    public bool SpeculateHttpMethods { get; }

    public string Template { get; }

    public string[]? HttpMethods { get; }

    public RouteTemplate(bool speculateHttpMethods, string template, string[]? httpMethods = null)
    {
        SpeculateHttpMethods = speculateHttpMethods;
        Template = template;
        HttpMethods = httpMethods;
    }

    public static RouteTemplate Success(string template, string[] httpMethods)
    {
        return new RouteTemplate(false, template, httpMethods);
    }

    public static RouteTemplate Mock(string template)
    {
        return new RouteTemplate(true, template);
    }
}
