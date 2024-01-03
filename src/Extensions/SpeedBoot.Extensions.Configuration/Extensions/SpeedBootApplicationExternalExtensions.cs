// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Configuration;

public static class SpeedBootApplicationExternalExtensions
{
    /// <summary>
    /// 获取配置对象IConfiguration，但它可能未注册
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <returns></returns>
    public static IConfiguration? GetConfiguration(this SpeedBootApplicationBuilder applicationBuilder)
        => applicationBuilder.GetSingletonService<IConfiguration>();

    public static IConfiguration GetRequiredConfiguration(this SpeedBootApplicationBuilder applicationBuilder)
        => applicationBuilder.GetRequiredSingletonService<IConfiguration>();

    /// <summary>
    /// Get the configuration object according to the SectionName, the default SectionName is consistent with the class name
    ///
    /// 根据SectionName获取配置对象，默认SectionName与类名保持一致
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <param name="sectionName"></param>
    /// <typeparam name="TOptions"></typeparam>
    /// <returns></returns>
    public static TOptions? GetOptions<TOptions>(this SpeedBootApplicationBuilder applicationBuilder, string? sectionName = null)
        where TOptions : class, new()
    {
        var configuration = applicationBuilder.GetConfiguration();
        var configurationSection = configuration?.GetSection(sectionName ?? typeof(TOptions).Name);
        return configurationSection?.GetOptions<TOptions>();
    }

    /// <summary>
    /// Get the configuration object according to the SectionName, the default SectionName is consistent with the class name
    ///
    /// 根据SectionName获取配置对象，默认SectionName与类名保持一致
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <param name="sectionName"></param>
    /// <typeparam name="TOptions"></typeparam>
    /// <returns></returns>
    public static TOptions GetRequiredOptions<TOptions>(this SpeedBootApplicationBuilder applicationBuilder, string? sectionName = null)
        where TOptions : class, new()
    {
        var options = GetOptions<TOptions>(applicationBuilder, sectionName);
        SpeedArgumentException.ThrowIfNull(options);
        return options;
    }
}
