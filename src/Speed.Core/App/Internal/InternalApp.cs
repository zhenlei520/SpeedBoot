// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed;

internal static class InternalApp
{
    public static IServiceCollection? Services;

    public static IServiceProvider? RootServiceProvider;

    public static readonly List<IAppStartup> AppStartupContextList = new();

    /// <summary>
    /// Valid Assemblies Collection
    /// </summary>
    public static IEnumerable<Assembly>? Assemblies;

    public static IConfiguration? Configuration;

    /// <summary>
    /// environment
    /// </summary>
    public static string Environment;

    internal static void ConfigureServices(IServiceCollection services)
    {
        Services ??= services;
    }

    internal static void ConfigureAssemblies(IEnumerable<Assembly> assemblies)
    {
        Assemblies ??= assemblies;
    }

    internal static void ConfigureConfiguration(IConfiguration? configuration)
    {
        if (configuration == null)
            return;

        Configuration ??= configuration;
    }

    internal static void ConfigureEnvironment(string? environment)
    {
        if (environment == null)
            return;

        Environment ??= environment;
    }

    internal static void ConfigureRootServiceProvider(IServiceProvider serviceProvider)
    {
        RootServiceProvider ??= serviceProvider;
    }
}
