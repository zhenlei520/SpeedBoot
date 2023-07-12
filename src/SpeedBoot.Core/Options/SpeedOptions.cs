// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public class SpeedOptions
{
    public bool EnabledServiceRegisterComponent { get; set; } = true;

    /// <summary>
    /// Valid Assemblies Collection
    /// The assembly collection used by default globally, when it is not null, it will be used first
    /// <!--Assemblies > AssemblyName-->
    /// </summary>
    public Assembly[]? Assemblies { get; set; }

    /// <summary>
    /// current environment information
    /// </summary>
    public string? Environment { get; set; }
}
