// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed;

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

    private static IEnumerable<Assembly>? _assemblies = null;

    /// <summary>
    /// Valid Assemblies Collection
    /// </summary>
    public static IEnumerable<Assembly>? Assemblies => _assemblies;
}
