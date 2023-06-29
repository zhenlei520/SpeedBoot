// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public static class AppCore
{
    /// <summary>
    /// collection of services
    /// </summary>
    public static IServiceCollection? Services => InternalApp.Services;

    /// <summary>
    /// root service provider
    /// </summary>
    public static IServiceProvider? RootServiceProvider => InternalApp.RootServiceProvider;

    /// <summary>
    /// Valid Assemblies Collection
    /// </summary>
    public static IEnumerable<Assembly>? Assemblies => InternalApp.Assemblies;

    /// <summary>
    /// current environment information
    /// </summary>
    public static string Environment => InternalApp.Environment;

    /// <summary>
    /// 配置对象
    ///
    /// Configuration
    /// </summary>
    public static object? Configuration => InternalApp.Configuration;

    /// <summary>
    /// Configure Root ServiceProvider
    ///
    /// 配置根 ServiceProvider
    /// </summary>
    /// <param name="rootServiceProvider"></param>
    public static void TryConfigureRootServiceProvider(IServiceProvider rootServiceProvider)
    {
        InternalApp.TryConfigureRootServiceProvider(rootServiceProvider);
    }

    /// <summary>
    /// Configure Root ServiceProvider
    ///
    /// 配置根 ServiceProvider
    /// </summary>
    /// <param name="rootServiceProvider"></param>
    public static void ConfigureRootServiceProvider(IServiceProvider rootServiceProvider)
    {
        InternalApp.ConfigureRootServiceProvider(rootServiceProvider);
    }

    /// <summary>
    /// Configure Configuration
    ///
    /// 配置配置对象
    /// </summary>
    /// <param name="configuration"></param>
    public static void TryConfigureConfiguration(object configuration)
    {
        InternalApp.TryConfigureConfiguration(configuration);
    }

    /// <summary>
    /// Configure Configuration
    ///
    /// 配置配置对象
    /// </summary>
    /// <param name="configuration"></param>
    public static void ConfigureConfiguration(object configuration)
    {
        InternalApp.ConfigureConfiguration(configuration);
    }
}
