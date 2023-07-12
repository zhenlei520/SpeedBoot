﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public static class App
{
    public static SpeedBootApplicationExternal ApplicationExternal { get; private set; }

    internal static void SetApplicationExternal(SpeedBootApplicationExternal applicationExternal)
    {
        ApplicationExternal = applicationExternal;
    }
}
