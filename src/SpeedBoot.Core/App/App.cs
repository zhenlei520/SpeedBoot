// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public class App
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

    public static IConfiguration? Configuration => InternalApp.Configuration;

    public static string Environment => InternalApp.Environment;
}
