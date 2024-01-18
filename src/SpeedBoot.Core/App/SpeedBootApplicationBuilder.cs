// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public class SpeedBootApplicationBuilder
{
    private readonly SpeedBootApplication _speedBootApplication;

    internal SpeedBootApplicationBuilder(SpeedBootApplication speedBootApplication)
    {
        _speedBootApplication = speedBootApplication;
    }

    /// <summary>
    /// Initialization module
    /// </summary>
    public void Build() => _speedBootApplication.InitializeComponents();
}
