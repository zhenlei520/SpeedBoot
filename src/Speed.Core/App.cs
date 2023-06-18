// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed;

public class App
{
    public static IServiceCollection Services { get; private set; }

    public static IServiceProvider? RootServiceProvider { get; private set; }

    public App(IServiceCollection services)
    {
        Services = services;
    }

    internal static void SetRootServiceProvider(IServiceProvider serviceProvider)
    {
        RootServiceProvider = serviceProvider;
    }
}
