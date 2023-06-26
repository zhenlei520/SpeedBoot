// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public static class App
{
    /// <summary>
    /// collection of services
    /// </summary>
    public static IServiceCollection? Services => AppCore.Services;

    /// <summary>
    /// root service provider
    /// </summary>
    public static IServiceProvider? RootServiceProvider => AppCore.RootServiceProvider;

    /// <summary>
    /// Valid Assemblies Collection
    /// </summary>
    public static IEnumerable<Assembly>? Assemblies => AppCore.Assemblies;

    /// <summary>
    /// current environment information
    /// </summary>
    public static string Environment => AppCore.Environment;

    /// <summary>
    /// get the current configuration
    /// </summary>
    public static IConfiguration? Configuration => AppConfiguration.Configuration;

    /// <summary>
    /// Http context
    /// It can be used to obtain request information and response information
    /// </summary>
    private static HttpContext? HttpContext => GetRootServiceProvider().GetRequiredService<IHttpContextAccessor>().HttpContext;

    #region Get the configuration object according to the SectionName, the default SectionName is consistent with the class name（根据SectionName获取配置对象，默认SectionName与类名保持一致）

    /// <summary>
    /// Get the configuration object according to the SectionName, the default SectionName is consistent with the class name
    /// 根据SectionName获取配置对象，默认SectionName与类名保持一致
    /// </summary>
    /// <param name="sectionName"></param>
    /// <typeparam name="TOptions"></typeparam>
    /// <returns></returns>
    public static TOptions GetOptions<TOptions>(string? sectionName = null) where TOptions : class, new()
    {
        SpeedArgumentException.ThrowIfNull(Configuration);
        var configuration = Configuration!.GetSection(sectionName ?? typeof(TOptions).Name);
        return configuration.GetOptions<TOptions>();
    }

    #endregion

    #region Provide support for API services（为API服务提供支持）

    /// <summary>
    /// Get the <typeparamref name="TService"/> service (may be empty，Only API requests are supported)
    /// 得到<typeparamref name="TService"/>服务（可能为空，仅支持API的请求）
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService? GetService<TService>() where TService : notnull
    {
        SpeedArgumentException.ThrowIfNull(HttpContext);
        return HttpContext!.RequestServices.GetService<TService>();
    }

    /// <summary>
    /// Get the <typeparamref name="TService"/> service (not empty，Only API requests are supported)
    /// 得到<typeparamref name="TService"/>服务（不为空，仅支持API的请求）
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService GetRequiredService<TService>() where TService : notnull
    {
        SpeedArgumentException.ThrowIfNull(HttpContext);
        return HttpContext!.RequestServices.GetRequiredService<TService>();
    }

    /// <summary>
    /// Get the service set of <typeparamref name="TService"/>
    /// 得到<typeparamref name="TService"/>服务集合
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TService> GetServices<TService>() where TService : notnull
    {
        return HttpContext?.RequestServices.GetServices<TService>() ?? new List<TService>();
    }

    #endregion

    #region Get Root ServiceProvider（得到根ServiceProvider）

    /// <summary>
    /// Get Root ServiceProvider
    /// 得到根ServiceProvider
    /// </summary>
    /// <returns></returns>
    private static IServiceProvider GetRootServiceProvider()
    {
        SpeedArgumentException.ThrowIfNull(RootServiceProvider);
        return RootServiceProvider;
    }

    #endregion
}
