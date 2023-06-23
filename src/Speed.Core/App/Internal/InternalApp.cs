// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed;

internal static class InternalApp
{
    public static IServiceCollection Services;

    public static IServiceProvider RootServiceProvider;

    public static readonly List<IAppStartup> AppStartupContextList = new();

    internal static void ConfigureServices(IServiceCollection services)
    {
        Services = services;
    }
}
