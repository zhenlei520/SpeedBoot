// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot;

public static class AppExtensions
{
    public static IServiceProvider GetRequiredRootServiceProvider(this App app, bool isTemporary = false)
    {
        var rootServiceProvider = app.RootServiceProvider;
        if (rootServiceProvider != null)
            return rootServiceProvider;

        SpeedArgumentException.ThrowIfNull(app.RebuildRootServiceProvider);
        rootServiceProvider = app.RebuildRootServiceProvider!.Invoke(app.Services);
        if (!isTemporary)
        {
            app.SetRootServiceProvider(rootServiceProvider);
        }

        return rootServiceProvider;
    }

    /// <summary>
    /// Get the <typeparamref name="TService"/> service (may be empty，Only API requests are supported)
    ///
    /// 得到<typeparamref name="TService"/>服务（可能为空，仅支持API的请求）
    /// </summary>
    /// <param name="app"></param>
    /// <param name="isTemporary"></param>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService? GetSingletonService<TService>(this App app, bool isTemporary = false)
        where TService : notnull
        => app.GetRequiredRootServiceProvider(isTemporary).GetService<TService>();

    /// <summary>
    /// Get the <typeparamref name="TService"/> service (not empty，Only API requests are supported)
    ///
    /// 得到<typeparamref name="TService"/>服务（不为空，仅支持API的请求）
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService GetRequiredSingletonService<TService>(this App app, bool isTemporary = false)
        where TService : notnull
        => app.GetRequiredRootServiceProvider(isTemporary).GetRequiredService<TService>();

    /// <summary>
    ///
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static object? GetSingletonService(this App app, Type serviceType, bool isTemporary = false)
        => app.GetRequiredRootServiceProvider(isTemporary).GetService(serviceType);

    public static object? GetRequiredSingletonService(this App app, Type serviceType, bool isTemporary = false)
        => app.GetRequiredRootServiceProvider(isTemporary).GetRequiredService(serviceType);

    public static IEnumerable<object?> GetRequiredSingletonServices(this App app, Type serviceType, bool isTemporary = false)
        => app.GetRequiredRootServiceProvider(isTemporary).GetServices(serviceType);

    /// <summary>
    /// Get the service set of <typeparamref name="TService"/>
    ///
    /// 得到<typeparamref name="TService"/>服务集合
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TService> GetSingletonServices<TService>(this App app, bool isTemporary = false)
        where TService : notnull
        => app.GetRequiredRootServiceProvider(isTemporary).GetServices<TService>();

    public static TService? GetSingletonService<TService>(this App app, string key, bool isTemporary = false) where TService : IService
        => app.GetSingletonServices<TService>(isTemporary).Where(item => item.Key == key).FirstOrDefault();

    public static TService GetRequiredSingletonService<TService>(this App app, string key, bool isTemporary = false)
        where TService : IService
        => app.GetSingletonService<TService>(key, isTemporary) ?? throw new SpeedException($"Unregistered service: {typeof(TService).FullName}, key: {key}");
}
