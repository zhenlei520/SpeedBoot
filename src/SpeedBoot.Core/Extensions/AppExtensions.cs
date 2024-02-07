// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot;

public static class AppExtensions
{
    public static IServiceProvider GetRequiredRootServiceProvider(this App app)
    {
        var rootServiceProvider = app.RootServiceProvider;
        if (rootServiceProvider != null)
            return rootServiceProvider;

        SpeedArgumentException.ThrowIfNull(app.RebuildRootServiceProvider);
        rootServiceProvider =  app.RebuildRootServiceProvider.Invoke(app.Services);
        app.SetRootServiceProvider(rootServiceProvider);
        return rootServiceProvider;
    }

    /// <summary>
    /// Get the <typeparamref name="TService"/> service (may be empty，Only API requests are supported)
    ///
    /// 得到<typeparamref name="TService"/>服务（可能为空，仅支持API的请求）
    /// </summary>
    /// <param name="app"></param>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService? GetSingletonService<TService>(this App app)
        where TService : notnull
        => app.GetRequiredRootServiceProvider().GetService<TService>();

    /// <summary>
    /// Get the <typeparamref name="TService"/> service (not empty，Only API requests are supported)
    ///
    /// 得到<typeparamref name="TService"/>服务（不为空，仅支持API的请求）
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService GetRequiredSingletonService<TService>(this App app)
        where TService : notnull
        => app.GetRequiredRootServiceProvider().GetRequiredService<TService>();

    /// <summary>
    /// Get the <typeparamref name="TService"/> service (not empty，Only API requests are supported)
    ///
    /// 得到<typeparamref name="TService"/>服务（不为空，仅支持API的请求）
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TService> GetRequiredSingletonServices<TService>(this App app)
        where TService : notnull
        => app.GetRequiredRootServiceProvider().GetServices<TService>();

    /// <summary>
    /// Get the service set of <typeparamref name="TService"/>
    ///
    /// 得到<typeparamref name="TService"/>服务集合
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TService> GetSingletonServices<TService>(this App app)
        where TService : notnull
        => app.GetRequiredRootServiceProvider().GetServices<TService>();

    public static TService? GetSingletonService<TService>(this App app, string key) where TService : IService
        => app.GetRequiredSingletonServices<TService>().Where(item => item.Key == key).FirstOrDefault();

    public static TService GetRequiredSingletonService<TService>(this App app, string key) where TService : IService
        => app.GetSingletonService<TService>(key) ?? throw new SpeedException($"Unregistered service: {typeof(TService).FullName}, key: {key}");
}
