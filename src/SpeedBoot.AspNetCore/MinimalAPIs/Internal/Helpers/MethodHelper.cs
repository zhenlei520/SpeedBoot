// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER

namespace SpeedBoot.AspNetCore;

internal static class MethodHelper
{
    public static IEnumerable<MethodInfo> GetMethodInfos(Type serviceType)
    {
        var bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        var routeIgnoreAttribute = typeof(RouteIgnoreAttribute);

        return serviceType
            .GetMethods(bindingFlags)
            .Where(methodInfo =>
                (!methodInfo.IsSpecialName || (methodInfo.IsSpecialName && methodInfo.Name.StartsWith("get_"))) &&
                methodInfo.CustomAttributes.All(attr => attr.AttributeType != routeIgnoreAttribute));
    }

    public static string GetMethodPrefix(IEnumerable<string> prefixes, string methodName)
    {
        return prefixes.FirstOrDefault(prefix => methodName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) ?? string.Empty;
    }

    private static string GetActionName(
        GlobalServiceRouteOptions globalServiceRouteOptions,
        ServiceRouteOptions routeOptions,
        string methodName,
        string prefix)
        => GetActionName(methodName,
            prefix,
            globalServiceRouteOptions.DisableTrimMethodSuffix ?? routeOptions.DisableTrimMethodSuffix,
            globalServiceRouteOptions.DisableTrimMethodPrefix ?? routeOptions.DisableTrimMethodPrefix);

    private static string GetActionName(string methodName, string prefix, bool? disableTrimMethodSuffix, bool? disableTrimMethodPrefix)
    {
        methodName = methodName.StartsWith("get_") ? methodName.Substring(4) : methodName;

        if (!(disableTrimMethodSuffix ?? false))
        {
            var suffix = "Async";
            methodName = methodName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)
                ? methodName.Substring(0, methodName.Length - suffix.Length)
                : methodName;
        }

        if (disableTrimMethodPrefix ?? false)
            return methodName;

        return methodName.Substring(prefix.Length);
    }

    public static Func<string> ActionNameFunc(GlobalServiceRouteOptions globalServiceRouteOptions,
        ServiceRouteOptions routeOptions,
        MethodInfo methodInfo) => () => GetActionName(globalServiceRouteOptions, routeOptions, methodInfo.Name, string.Empty);

    public static SpeculateMethod SpeculateMethodsAndActionNameFunc(
        GlobalServiceRouteOptions globalServiceRouteOptions,
        ServiceRouteOptions routeOptions,
        MethodInfo methodInfo)
    {
        var methodName = methodInfo.Name;

        var prefix = GetMethodPrefix(routeOptions.GetPrefixes ?? globalServiceRouteOptions.GetPrefixes!, methodName);
        if (!string.IsNullOrEmpty(prefix))
            return new SpeculateMethod(["GET"], ActionNameFunc());

        prefix = GetMethodPrefix(routeOptions.PostPrefixes ?? globalServiceRouteOptions.PostPrefixes!, methodName);
        if (!string.IsNullOrEmpty(prefix))
            return new SpeculateMethod(["POST"], ActionNameFunc());

        prefix = GetMethodPrefix(routeOptions.PutPrefixes ?? globalServiceRouteOptions.PutPrefixes!, methodName);
        if (!string.IsNullOrEmpty(prefix))
            return new SpeculateMethod(["PUT"], ActionNameFunc());

        prefix = GetMethodPrefix(routeOptions.DeletePrefixes ?? globalServiceRouteOptions.DeletePrefixes!, methodName);
        if (!string.IsNullOrEmpty(prefix))
            return new SpeculateMethod(["DELETE"], ActionNameFunc());

        return new SpeculateMethod(null, () => string.Empty);

        Func<string> ActionNameFunc() => () => GetActionName(globalServiceRouteOptions, routeOptions, methodName, prefix);
    }

    private static string[]? GetHttpMethods(MethodInfo methodInfo)
    {
        var httpMethodAttribute = methodInfo.GetCustomAttribute<HttpMethodAttribute>();
        return httpMethodAttribute?.HttpMethods;
    }

    public static List<RouteTemplate> GetRouteTemplate(
        MethodInfo methodInfo,
        List<string> templatesByClass)
    {
        var tempRouteTemplates = GetRouteTemplateList();
        var routeTemplates = new List<RouteTemplate>();

        foreach (var tempRouteTemplate in tempRouteTemplates)
        {
            if (tempRouteTemplate.Template.StartsWith("/"))
            {
                var template = tempRouteTemplate.Template.Substring(1);
                var routeTemplate = new RouteTemplate(tempRouteTemplate.SpeculateHttpMethods, template, tempRouteTemplate.HttpMethods);
                routeTemplates.Add(routeTemplate);
            }
            else if (tempRouteTemplate.Template.StartsWith("~/"))
            {
                var template = tempRouteTemplate.Template.Substring(2);
                var routeTemplate = new RouteTemplate(tempRouteTemplate.SpeculateHttpMethods, template, tempRouteTemplate.HttpMethods);
                routeTemplates.Add(routeTemplate);
            }
            else
            {
                foreach (var templateByClass in templatesByClass)
                {
                    var template = string.Join("/", templateByClass.Split("/").Union(tempRouteTemplate.Template.Split('/')));
                    var templateTemplate = new RouteTemplate(tempRouteTemplate.SpeculateHttpMethods,template, tempRouteTemplate.HttpMethods);
                    routeTemplates.Add(templateTemplate);
                }
            }
        }
        return routeTemplates;

        List<RouteTemplate> GetRouteTemplateList()
        {
            var routeAttributes = methodInfo.GetCustomAttributes<RouteAttribute>(true).ToList();
            var httpMethodAttributes = methodInfo.GetCustomAttributes<HttpMethodAttribute>(true).ToList();
            var httpMethodAttributesWithExistTemplates = httpMethodAttributes.Where(attribute => attribute.Template != null).ToList();
            var httpMethodAttributesWithNotExistTemplates = httpMethodAttributes.Where(attribute => attribute.Template == null).ToList();

            var list = new List<RouteTemplate>();
            if (routeAttributes.Any())
            {
                if (!httpMethodAttributes.Any())
                {
                    // [Route("/test")], only use httpMethod: mock、template: /test
                    list.AddRange(routeAttributes.Select(attribute => RouteTemplate.Mock(attribute.Template)));
                }
                else
                {
                    if (httpMethodAttributesWithExistTemplates.Any())
                    {
                        // [HttpGet("/temp")], [Route("/test")], only use httpMethod: get、template: /temp
                        var routeTemplates = httpMethodAttributesWithExistTemplates.Select(attribute => RouteTemplate.Success(attribute.Template!, attribute.HttpMethods));
                        list.AddRange(routeTemplates);
                    }
                    if (httpMethodAttributesWithNotExistTemplates.Any())
                    {
                        // [HttpGet]、[HttpPost], [Route("/test")], only use httpMethod: get、template: /test and httpMethod: post、template: /test
                        var httpMethods = httpMethodAttributesWithNotExistTemplates.SelectMany(attribute => attribute.HttpMethods).ToArray();
                        list.AddRange(routeAttributes.Select(attribute => RouteTemplate.Success(attribute.Template, httpMethods)));
                    }
                }
            }
            else
            {
                if (httpMethodAttributes.Any())
                {
                    var routeTemplates = httpMethodAttributes.Select(attribute => RouteTemplate.Success(attribute.Template ?? string.Empty, attribute.HttpMethods));
                    list.AddRange(routeTemplates);
                }
                else
                {
                    list.Add(RouteTemplate.Mock(string.Empty));
                }
            }

            return list;
        }
    }
}

#endif
