// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

internal static class SpeedBootApplicationExternalExtensions
{
    public static IServiceProvider GetRequiredRootServiceProvider(this SpeedBootApplicationBuilder applicationBuilder)
        => applicationBuilder.RootServiceProvider ?? applicationBuilder.Services.BuildServiceProvider();

    /// <summary>
    /// Get the <typeparamref name="TService"/> service (may be empty，Only API requests are supported)
    ///
    /// 得到<typeparamref name="TService"/>服务（可能为空，仅支持API的请求）
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService? GetSingletonService<TService>(this SpeedBootApplicationBuilder applicationBuilder)
        where TService : notnull
        => applicationBuilder.GetRequiredRootServiceProvider().GetService<TService>();

    /// <summary>
    /// Get the <typeparamref name="TService"/> service (not empty，Only API requests are supported)
    ///
    /// 得到<typeparamref name="TService"/>服务（不为空，仅支持API的请求）
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService GetRequiredSingletonService<TService>(this SpeedBootApplicationBuilder applicationBuilder)
        where TService : notnull
        => applicationBuilder.GetRequiredRootServiceProvider().GetRequiredService<TService>();

    /// <summary>
    /// Get the service set of <typeparamref name="TService"/>
    ///
    /// 得到<typeparamref name="TService"/>服务集合
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TService> GetSingletonServices<TService>(this SpeedBootApplicationBuilder applicationBuilder)
        where TService : notnull
        => applicationBuilder.GetRequiredRootServiceProvider().GetServices<TService>();
}
