// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Configuration;

public static class AppExtensions
{
    /// <summary>
    /// 获取配置对象IConfiguration，但它可能未注册
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IConfiguration? GetConfiguration(this App app)
        => app.GetSingletonService<IConfiguration>();

    /// <summary>
    ///
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IConfiguration GetRequiredConfiguration(this App app)
        => app.GetRequiredSingletonService<IConfiguration>();

    /// <summary>
    /// Get the configuration object according to the SectionName, the default SectionName is consistent with the class name
    ///
    /// 根据SectionName获取配置对象，默认SectionName与类名保持一致
    /// </summary>
    /// <param name="app"></param>
    /// <param name="sectionName"></param>
    /// <typeparam name="TOptions"></typeparam>
    /// <returns></returns>
    public static TOptions? GetOptions<TOptions>(this App app, string? sectionName = null)
        where TOptions : class, new()
    {
        var configuration = app.GetConfiguration();
        var configurationSection = configuration?.GetSection(sectionName ?? typeof(TOptions).Name);
        return configurationSection?.GetOptions<TOptions>();
    }

    /// <summary>
    /// Get the configuration object according to the SectionName, the default SectionName is consistent with the class name
    ///
    /// 根据SectionName获取配置对象，默认SectionName与类名保持一致
    /// </summary>
    /// <param name="app"></param>
    /// <param name="sectionName"></param>
    /// <typeparam name="TOptions"></typeparam>
    /// <returns></returns>
    public static TOptions GetRequiredOptions<TOptions>(this App app, string? sectionName = null)
        where TOptions : class, new()
    {
        var options = GetOptions<TOptions>(app, sectionName);
        SpeedArgumentException.ThrowIfNull(options);
        return options!;
    }
}
