// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.AspNetCore;

public static class AppExtensions
{
    /// <summary>
    /// Http context
    /// It can be used to obtain request information and response information
    /// </summary>
    public static HttpContext? GetHttpContext(this App app)
        => app.RootServiceProvider.GetService<IHttpContextAccessor>()?.HttpContext;

    /// <summary>
    /// Get the <typeparamref name="TService"/> service (may be empty，Only API requests are supported)
    /// 得到<typeparamref name="TService"/>服务（可能为空，仅支持API的请求）
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService? GetCurrentService<TService>(this App app) where TService : notnull
    {
        var httpContext = app.GetHttpContext();
        return httpContext == null ? default : httpContext.RequestServices.GetService<TService>();
    }

    /// <summary>
    /// Get the <typeparamref name="TService"/> service (not empty，Only API requests are supported)
    /// 得到<typeparamref name="TService"/>服务（不为空，仅支持API的请求）
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService GetCurrentRequiredService<TService>(this App app) where TService : notnull
    {
        var httpContext = app.GetHttpContext();
        SpeedArgumentException.ThrowIfNull(httpContext);
        return httpContext!.RequestServices.GetRequiredService<TService>();
    }

    /// <summary>
    /// Get the service set of <typeparamref name="TService"/>
    /// 得到<typeparamref name="TService"/>服务集合
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TService> GetCurrentServices<TService>(this App app)
        where TService : notnull
    {
        var httpContext = app.GetHttpContext();
        SpeedArgumentException.ThrowIfNull(httpContext);
        return httpContext!.RequestServices.GetServices<TService>();
    }
}
