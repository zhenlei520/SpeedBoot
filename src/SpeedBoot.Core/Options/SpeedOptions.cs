﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public class SpeedOptions
{
    public bool EnabledServiceComponent { get; set; } = true;

    #region Valid Assemblies

    /// <summary>
    /// Valid Assemblies Collection
    /// The assembly collection used by default globally, when it is not null, it will be used first
    /// <!--Assemblies > AssemblyName-->
    /// </summary>
    public Assembly[]? Assemblies { get; set; }

    /// <summary>
    /// Framework Assembly Rules
    /// </summary>
    internal List<string> FrameworkAssemblyNames { get; set; } = new()
    {
        "SpeedBoot.*"
    };

    /// <summary>
    /// Assembly name prefix
    /// default: *
    /// Support for regular expressions
    /// When no assembly collection is specified, automatic matching based on the assembly name satisfies the use of
    /// <!--AssemblyName < Assemblies-->
    /// </summary>
    public string AssemblyName { get; set; } = ".*";

    #endregion

    /// <summary>
    /// current environment information
    /// </summary>
    public string? Environment { get; set; }

    /// <summary>
    /// Get the valid set of assembly rules
    /// </summary>
    /// <returns></returns>
    internal List<string> GetEffectAssemblyNames()
    {
        var assemblyNames = new List<string>(FrameworkAssemblyNames)
        {
            AssemblyName
        };
        return assemblyNames;
    }
}
