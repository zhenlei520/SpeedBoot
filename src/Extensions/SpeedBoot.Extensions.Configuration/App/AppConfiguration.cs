// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public static class AppConfiguration
{
    /// <summary>
    /// current environment information
    /// </summary>
    public static IConfiguration? Configuration => AppCore.Configuration as IConfiguration;

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
}
