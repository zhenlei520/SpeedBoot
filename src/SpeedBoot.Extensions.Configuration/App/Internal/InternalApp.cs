// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Configuration;

internal static class InternalApp
{
    public static IConfiguration? Configuration;

    internal static void ConfigureConfiguration(IConfiguration? configuration)
    {
        if (configuration == null)
            return;

        Configuration ??= configuration;
    }
}
