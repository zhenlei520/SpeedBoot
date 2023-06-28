// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

internal static class InternalApp
{
    public static IServiceCollection? Services;

    public static IServiceProvider? RootServiceProvider;

    public static readonly List<IAppStartup> AppStartupContextList = new();

    /// <summary>
    /// Valid Assemblies Collection
    ///
    /// 可用的程序集集合
    /// </summary>
    public static IEnumerable<Assembly>? Assemblies;

    /// <summary>
    /// environment
    ///
    /// 当前环境
    /// </summary>
    public static string Environment;

    /// <summary>
    /// 配置对象
    ///
    /// Configuration
    /// </summary>
    public static object? Configuration;

    internal static void ConfigureServices(IServiceCollection services)
    {
        Services ??= services;
    }

    internal static void ConfigureAssemblies(IEnumerable<Assembly> assemblies)
    {
        Assemblies ??= assemblies;
    }

    internal static void ConfigureEnvironment(string? environment)
    {
        if (environment == null)
            return;

        Environment ??= environment;
    }

    internal static void ConfigureConfiguration(object configuration)
    {
        Configuration ??= configuration;
    }

    internal static void ConfigureRootServiceProvider(IServiceProvider rootServiceProvider)
    {
        RootServiceProvider ??= rootServiceProvider;
    }
}
