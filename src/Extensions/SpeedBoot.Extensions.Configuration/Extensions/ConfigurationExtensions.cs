// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{
    #region Obtain configuration information according to IConfiguration（根据IConfiguration获取配置对象信息）

    /// <summary>
    /// Obtain configuration information according to IConfiguration
    /// 根据IConfiguration获取配置对象信息
    /// </summary>
    /// <param name="configuration">IConfiguration（配置对象）</param>
    /// <typeparam name="TOptions">options type（配置类型）</typeparam>
    /// <returns></returns>
    public static TOptions GetOptions<TOptions>(this IConfiguration configuration)
        where TOptions : class, new()
    {
        var instance = Activator.CreateInstance(typeof(TOptions)) as TOptions;
        SpeedArgumentException.ThrowIfNull(instance);
        configuration.Bind(instance);
        return instance!;
    }

    #endregion Obtain configuration information according to IConfiguration（根据IConfiguration获取配置对象信息）
}
