// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

    private IEnglishPluralizationService? _englishPluralizationService;

    protected IEnglishPluralizationService EnglishPluralizationService
        => _englishPluralizationService ??= App.Services.GetRequiredService<IEnglishPluralizationService>();

    protected List<RouteAttribute> RouteAttributes;
#if NET7_0_OR_GREATER
    protected List<ActionFilterBaseAttribute> ActionFilters;
#endif

    protected ServiceBase()
    {
        RouteAttributes = GetType().GetCustomAttributes<RouteAttribute>(true).ToList();
#if NET7_0_OR_GREATER
        ActionFilters = GetType().GetCustomAttributes<ActionFilterBaseAttribute>(true).ToList();
#endif
    }

    internal void AutoMapRoute(GlobalServiceRouteOptions globalServiceRouteOptions)
    {
        var methodInfos = MethodHelper.GetMethodInfos(GetType());
        var templatesByClass = RouteAttributes.Any() ? RouteAttributes.Select(attribute => attribute.Template).ToList() : new List<string>() { globalServiceRouteOptions.RouteTemplate };
        string? serviceName = null;
        foreach (var methodInfo in methodInfos)
        {
            var metadata = GetMapMetadata(globalServiceRouteOptions, methodInfo,templatesByClass, GetServiceName);
            foreach (var (pattern, httpMethods) in metadata)
            {
                var routeHandlerBuilder = MapMethods(pattern, httpMethods,CreateDelegate(methodInfo, this));
                var actions = RouteHandlerBuilderHelper.GetRouteHandlerBuilderActions(globalServiceRouteOptions, RouteOptions);
                foreach (var action in actions)
                {
                    action.Invoke(methodInfo, routeHandlerBuilder);
                }
                TryRegisterActionFilter(routeHandlerBuilder, methodInfo);
            }
        }

        string GetServiceName()
        {
            return serviceName ??= this.GetServiceName(RouteOptions.DisablePluralizeServiceName ?? globalServiceRouteOptions.DisablePluralizeServiceName ?? false);
        }
    }

    /// <summary>
    /// key: pattern
    /// value: httpMethods
    /// </summary>
    /// <param name="globalServiceRouteOptions"></param>
    /// <param name="methodInfo"></param>
    /// <param name="templatesByClass"></param>
    /// <param name="serviceNameFunc"></param>
    /// <returns></returns>
    protected virtual List<(string Pattern, string[]? HttpMethods)> GetMapMetadata(
        GlobalServiceRouteOptions globalServiceRouteOptions,
        MethodInfo methodInfo,
        List<string> templatesByClass,
        Func<string> serviceNameFunc)
    {
        string? actionName = null;
        SpeculateMethod? speculateMethod = null;
        var routeTemplates = MethodHelper.GetRouteTemplate(methodInfo, templatesByClass);

        var patterns = new List<(string Pattern, string[]? HttpMethods)>();
        foreach (var routeTemplate in routeTemplates)
        {
            var httpMethods = routeTemplate.SpeculateHttpMethods ? GetSpeculateMethod().HttpMethods : routeTemplate.HttpMethods;
            var actionNameFunc = () => routeTemplate.SpeculateHttpMethods ? GetSpeculateMethod().ActionNameFunc.Invoke() : GetActionName();

            var newTemplate = routeTemplate.Template;
            if (newTemplate.Contains("[service]"))
            {
                newTemplate = newTemplate.Replace("[service]", serviceNameFunc.Invoke());
            }
            if (newTemplate.Contains("[action]"))
            {
                newTemplate = newTemplate.Replace("[action]", actionNameFunc.Invoke());
            }
            patterns.Add((newTemplate, httpMethods));
        }
        return patterns;

        string GetActionName()
        {
            return actionName ??= MethodHelper.ActionNameFunc(globalServiceRouteOptions, RouteOptions, methodInfo).Invoke();
        }

        SpeculateMethod GetSpeculateMethod()
        {
            return speculateMethod ??= MethodHelper.SpeculateMethodsAndActionNameFunc(globalServiceRouteOptions, RouteOptions, methodInfo);
        }
    }

    Delegate CreateDelegate(MethodInfo methodInfo, object targetInstance)
    {
        var types = methodInfo.GetParameters()
            .Select(parameterInfo => parameterInfo.ParameterType)
            .Concat(new List<Type>
            {
                methodInfo.ReturnType
            }).ToArray();
        var type = Expression.GetDelegateType(types);
        return Delegate.CreateDelegate(type, targetInstance, methodInfo);
    }

    RouteHandlerBuilder MapMethods(string pattern, string[]? httpMethods, Delegate handler)
    {
        if (httpMethods == null || !httpMethods.Any())
            return App.Map(pattern, handler);

        return App.MapMethods(pattern, httpMethods, handler);
    }

    protected virtual string GetServiceName(bool disablePluralizeServiceName)
    {
        var serviceName = GetType().Name;
        var suffix = "Service";

        serviceName = serviceName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)
            ? serviceName.Substring(0, serviceName.Length - suffix.Length)
            : serviceName;

        if (disablePluralizeServiceName)
            return serviceName;

        return EnglishPluralizationService.Pluralize(serviceName);
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
