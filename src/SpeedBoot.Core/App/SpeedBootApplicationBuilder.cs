// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public class SpeedBootApplicationBuilder
{
    public readonly SpeedBootApplication SpeedBootApplication;

    internal SpeedBootApplicationBuilder(SpeedBootApplication speedBootApplication)
    {
        SpeedBootApplication = speedBootApplication;
    }

    /// <summary>
    /// Initialization module
    /// </summary>
    public void Build() => SpeedBootApplication.InitializeComponents();
}
