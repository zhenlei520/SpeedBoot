// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Configuration;

public static class SpeedBootApplicationExternalExtensions
{
    public static IConfiguration GetConfiguration(this SpeedBootApplicationExternal applicationExternal)
        => applicationExternal.GetRequiredSingletonService<IConfiguration>();

    /// <summary>
    /// Get the configuration object according to the SectionName, the default SectionName is consistent with the class name
    ///
    /// 根据SectionName获取配置对象，默认SectionName与类名保持一致
    /// </summary>
    /// <param name="applicationExternal"></param>
    /// <param name="sectionName"></param>
    /// <typeparam name="TOptions"></typeparam>
    /// <returns></returns>
    public static TOptions GetOptions<TOptions>(this SpeedBootApplicationExternal applicationExternal, string? sectionName = null)
        where TOptions : class, new()
    {
        var configuration = applicationExternal.GetConfiguration();
        var configurationSection = configuration.GetSection(sectionName ?? typeof(TOptions).Name);
        return configurationSection.GetOptions<TOptions>();
    }
}
