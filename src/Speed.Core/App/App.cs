// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed;

public class App
{
    public static IServiceCollection Services => InternalApp.Services;

    public static IServiceProvider? RootServiceProvider => InternalApp.RootServiceProvider;
}
