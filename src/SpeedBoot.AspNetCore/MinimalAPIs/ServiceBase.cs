// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SpeedBoot.AspNetCore.Options;

namespace SpeedBoot.AspNetCore;

public abstract class ServiceBase
{
    private WebApplication? _app = null;

    public WebApplication App => _app ??= SpeedBoot.App.Instance.GetRequiredSingletonService<WebApplication>();

    public IServiceProvider Services =>
        ServiceCollection.BuildServiceProvider().GetService<IHttpContextAccessor>()?.HttpContext?.RequestServices ??
        ServiceCollection.BuildServiceProvider() ?? throw new Exception($"{nameof(ServiceBase)} get the IServiceProvider faild.");

    public IServiceCollection ServiceCollection => SpeedBoot.App.Instance.GetRequiredSingletonService<IServiceCollection>();

    public string? ServiceName { get; init; }

    public ServiceRouteOptions RouteOptions { get; set; } = new ServiceRouteOptions();

#if NET7_0_OR_GREATER
    protected List<ActionFilterBaseAttribute> ActionFilters;
#endif

    protected ServiceBase()
    {
#if NET7_0_OR_GREATER
        ActionFilters = GetType().GetCustomAttributes<ActionFilterBaseAttribute>(true).ToList();
#endif
    }

    protected ServiceBase(string prefix) : this()
    {
        RouteOptions.Prefix = prefix;
    }

    internal void AutoMapRoute(GlobalServiceRouteOptions globalServiceRouteOptions)
    {
        foreach (var methodInfo in GetMethodInfos())
        {
            (string? httpMethod, string httpMethodPrefix) = ParseMethod(globalServiceRouteOptions, methodInfo.Name);

            (httpMethod, string pattern) = GetMapMetadata(methodInfo, httpMethodPrefix, httpMethod, globalServiceRouteOptions);

            var routeHandlerBuilder = MapMethods(pattern, httpMethod, CreateDelegate(methodInfo, this));

            var actions = (RouteOptions.RouteHandlerBuilders ?? globalServiceRouteOptions.RouteHandlerBuilders) ??
                new List<Action<RouteHandlerBuilder>>();
            foreach (var action in actions)
            {
                action.Invoke(routeHandlerBuilder);
            }

            TryRegisterActionFilter(routeHandlerBuilder, methodInfo);
        }
    }

    private IEnumerable<MethodInfo> GetMethodInfos()
    {
        var bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        var routeIgnoreAttribute = typeof(RouteIgnoreAttribute);

        return GetType()
            .GetMethods(bindingFlags)
            .Where(methodInfo =>
                (!methodInfo.IsSpecialName || (methodInfo.IsSpecialName && methodInfo.Name.StartsWith("get_"))) &&
                methodInfo.CustomAttributes.All(attr => attr.AttributeType != routeIgnoreAttribute));
    }

    Delegate CreateDelegate(MethodInfo methodInfo, object targetInstance)
    {
        var type = Expression.GetDelegateType(
            methodInfo.GetParameters().Select(parameterInfo => parameterInfo.ParameterType)
                .Concat(new List<Type>
                {
                    methodInfo.ReturnType
                }).ToArray()
        );
        return Delegate.CreateDelegate(type, targetInstance, methodInfo);
    }

    (string? HttpMethod, string Prefix) ParseMethod(GlobalServiceRouteOptions globalServiceRouteOptions, string methodName)
    {
        var prefix = ParseMethodPrefix(RouteOptions.GetPrefixes ?? globalServiceRouteOptions.GetPrefixes!, methodName);
        if (!string.IsNullOrEmpty(prefix))
            return ("GET", prefix);

        prefix = ParseMethodPrefix(RouteOptions.PostPrefixes ?? globalServiceRouteOptions.PostPrefixes!, methodName);
        if (!string.IsNullOrEmpty(prefix))
            return ("POST", prefix);

        prefix = ParseMethodPrefix(RouteOptions.PutPrefixes ?? globalServiceRouteOptions.PutPrefixes!, methodName);
        if (!string.IsNullOrEmpty(prefix))
            return ("PUT", prefix);

        prefix = ParseMethodPrefix(RouteOptions.DeletePrefixes ?? globalServiceRouteOptions.DeletePrefixes!, methodName);
        if (!string.IsNullOrEmpty(prefix))
            return ("DELETE", prefix);

        return (null, string.Empty);
    }

    RouteHandlerBuilder MapMethods(string pattern, string? httpMethod, Delegate handler)
    {
        if (!string.IsNullOrWhiteSpace(httpMethod))
        {
            return App.MapMethods(pattern, new[] { httpMethod }, handler);
        }

        return App.Map(pattern, handler);
    }

    string ParseMethodPrefix(IEnumerable<string> prefixes, string methodName)
    {
        return prefixes.FirstOrDefault(prefix => methodName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) ?? string.Empty;
    }

    (string? HttpMethod, string Pattern) GetMapMetadata(MethodInfo methodInfo, string httpMethodPrefix, string? httpMethod,
        GlobalServiceRouteOptions globalServiceRouteOptions)
    {
        var routePatternAttribute = methodInfo.GetCustomAttribute<RoutePatternAttribute>();
        string pattern;

        if (routePatternAttribute is not null)
        {
            if (routePatternAttribute.HttpMethod is not null)
            {
                httpMethod = routePatternAttribute.HttpMethod;
            }

            if (routePatternAttribute.IgnorePrefix)
            {
#if NET6_0
                ArgumentNullException.ThrowIfNull(routePatternAttribute.Pattern, nameof(routePatternAttribute.Pattern));
#else
                ArgumentException.ThrowIfNullOrEmpty(routePatternAttribute.Pattern, nameof(routePatternAttribute.Pattern));
#endif
                pattern = routePatternAttribute.Pattern;
            }
            else
            {
                pattern = string.Join('/', GetBaseUri(globalServiceRouteOptions).Trim('/'), routePatternAttribute.Pattern?.Trim('/'))
                    .ToString();
            }
        }
        else
        {
            pattern = string.Join('/', GetBaseUri(globalServiceRouteOptions).Trim('/'),
                GetMethodName(methodInfo, httpMethodPrefix, globalServiceRouteOptions).Trim('/')).ToString();
        }

        return (httpMethod, pattern);
    }

    string GetBaseUri(GlobalServiceRouteOptions globalServiceRouteOptions)
    {
        var list = new List<string>()
        {
            RouteOptions.Prefix ?? globalServiceRouteOptions.Prefix ?? string.Empty,
            RouteOptions.Version ?? globalServiceRouteOptions.Version ?? string.Empty,
            ServiceName ??
            GetServiceName(RouteOptions.DisablePluralizeServiceName ?? globalServiceRouteOptions.DisablePluralizeServiceName ?? false
                ? null
                : globalServiceRouteOptions)
        };

        return string.Join('/', list.Where(x => !string.IsNullOrWhiteSpace(x)).Select(u => u.Trim('/')));
    }

    string GetServiceName(GlobalServiceRouteOptions? globalServiceRouteOptions)
    {
        var serviceName = GetType().Name;
        var suffix = "Service";

        serviceName = serviceName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)
            ? serviceName.Substring(0, serviceName.Length - suffix.Length)
            : serviceName;

        return serviceName;
    }

    string GetMethodName(MethodInfo methodInfo, string httpMethodPrefix, GlobalServiceRouteOptions globalServiceRouteOptions)
    {
        var methodName = TrimMethodName(methodInfo.Name, httpMethodPrefix, globalServiceRouteOptions);

        if (RouteOptions.DisableAutoAppendId ?? globalServiceRouteOptions.DisableAutoAppendId ?? false)
        {
            return methodName;
        }

        var idParameter = methodInfo.GetParameters().FirstOrDefault(p =>
            p.Name!.Equals("id", StringComparison.OrdinalIgnoreCase) &&
            p.GetCustomAttributes().All(attr => attr is not IBindingSourceMetadata)
        );

        if (idParameter is not null)
        {
            var id =
                (idParameter.ParameterType.IsGenericType && idParameter.ParameterType.GetGenericTypeDefinition() == typeof(Nullable<>)) ||
                idParameter.HasDefaultValue
                    ? "{id?}"
                    : "{id}";
            return $"{methodName}/{id}";
        }

        return methodName;
    }

    string TrimMethodName(string methodName, string httpMethodPrefix, GlobalServiceRouteOptions globalServiceRouteOptions)
    {
        methodName = methodName.StartsWith("get_") ? methodName.Substring(4) : methodName;

        if (!(RouteOptions.DisableTrimMethodSuffix ?? globalServiceRouteOptions.DisableTrimMethodSuffix ?? false))
        {
            var suffix = "Async";
            methodName = methodName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)
                ? methodName.Substring(0, methodName.Length - suffix.Length)
                : methodName;
        }

        if (RouteOptions.DisableTrimMethodPrefix ?? globalServiceRouteOptions.DisableTrimMethodPrefix ?? false)
            return methodName;

        return methodName.Substring(httpMethodPrefix.Length);
    }

    protected virtual void TryRegisterActionFilter(RouteHandlerBuilder routeHandlerBuilder, MethodInfo methodInfo)
    {
#if NET7_0_OR_GREATER
        RegisterActionFilter(routeHandlerBuilder, methodInfo);
#endif
    }

#if NET7_0_OR_GREATER
    private void RegisterActionFilter(RouteHandlerBuilder routeHandlerBuilder, MethodInfo methodInfo)
    {
        var customFilterAttributes = GetAllActionFilterAttributes(methodInfo);
        foreach (var customFilterAttribute in customFilterAttributes)
        {
            routeHandlerBuilder.AddEndpointFilter((invocationContext, next) =>
            {
                var actionFilterProvider =
                    invocationContext.HttpContext.RequestServices.GetService(customFilterAttribute.ServiceType) as IActionFilterProvider;
                SpeedArgumentException.ThrowIfNull(actionFilterProvider);
                return actionFilterProvider.HandlerAsync(invocationContext, next);
            });
        }
    }

    private IEnumerable<ActionFilterBaseAttribute> GetAllActionFilterAttributes(MethodInfo methodInfo)
    {
        var actionFiltersByMethod = methodInfo.GetCustomAttributes<ActionFilterBaseAttribute>(true).OrderBy(attribute => attribute.Order)
            .ToList();

        return actionFiltersByMethod.UnionBy(ActionFilters, attribute => attribute.ServiceType);
    }
#endif
}

#endif
