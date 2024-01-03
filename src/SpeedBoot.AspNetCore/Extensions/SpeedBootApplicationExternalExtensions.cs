// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.AspNetCore;

public static class SpeedBootApplicationExternalExtensions
{
    /// <summary>
    /// Http context
    /// It can be used to obtain request information and response information
    /// </summary>
    public static HttpContext? GetHttpContext(this SpeedBootApplicationBuilder applicationBuilder)
        => applicationBuilder.GetRequiredRootServiceProvider().GetRequiredService<IHttpContextAccessor>().HttpContext;

    /// <summary>
    /// Get the <typeparamref name="TService"/> service (may be empty，Only API requests are supported)
    /// 得到<typeparamref name="TService"/>服务（可能为空，仅支持API的请求）
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService? GetService<TService>(this SpeedBootApplicationBuilder applicationBuilder) where TService : notnull
    {
        var httpContext = applicationBuilder.GetHttpContext();
        return httpContext == null ? default : httpContext.RequestServices.GetService<TService>();
    }

    /// <summary>
    /// Get the <typeparamref name="TService"/> service (not empty，Only API requests are supported)
    /// 得到<typeparamref name="TService"/>服务（不为空，仅支持API的请求）
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService GetRequiredService<TService>(this SpeedBootApplicationBuilder applicationBuilder) where TService : notnull
    {
        var httpContext = applicationBuilder.GetHttpContext();
        SpeedArgumentException.ThrowIfNull(httpContext);
        return httpContext!.RequestServices.GetRequiredService<TService>();
    }

    /// <summary>
    /// Get the service set of <typeparamref name="TService"/>
    /// 得到<typeparamref name="TService"/>服务集合
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TService> GetServices<TService>(this SpeedBootApplicationBuilder applicationBuilder)
        where TService : notnull
    {
        var httpContext = applicationBuilder.GetHttpContext();
        SpeedArgumentException.ThrowIfNull(httpContext);
        return httpContext!.RequestServices.GetServices<TService>();
    }
}
