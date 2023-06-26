// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public static class AppConfiguration
{
    /// <summary>
    /// current environment information
    /// </summary>
    public static IConfiguration? Configuration => SpeedBoot.Configuration.InternalApp.Configuration;
}
