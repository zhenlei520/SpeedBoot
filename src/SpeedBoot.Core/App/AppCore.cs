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

    public static void SetRootServiceProvider(IServiceProvider rootServiceProvider)
    {
        InternalApp.ConfigureRootServiceProvider(rootServiceProvider);
    }
}
