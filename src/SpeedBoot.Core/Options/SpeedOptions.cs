// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public class SpeedOptions
{
    public bool EnabledServiceRegisterComponent { get; set; } = true;

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
    internal List<string> DefaultIncludeAssemblyRules { get; set; } = new()
    {
        "SpeedBoot.*"
    };

    /// <summary>
    /// Assembly rules loaded by default
    /// default: *
    /// Support for regular expressions
    /// When no assembly collection is specified, automatic matching based on the assembly name satisfies the use of
    /// <!--AssemblyName < Assemblies-->
    /// </summary>
    public List<string> IncludeAssemblyRules { get; set; } = new()
    {
        ".*"
    };

    /// <summary>
    /// Rules for assemblies excluded by default
    /// </summary>
    public List<string>? ExcludeAssemblyRules { get; set; }

    /// <summary>
    /// Rules for assemblies excluded by default
    /// </summary>
    public List<string> DefaultExcludeAssemblyRules { get; set; } = new()
    {
        "^System.*",
        "^Microsoft.*",
        "^Serilog.*",
        "^Swashbuckle.*",
        "^Npgsql.*"
    };

    #endregion

    /// <summary>
    /// current environment information
    /// </summary>
    public string? Environment { get; set; }

    /// <summary>
    /// Get the valid set of assembly rules
    /// </summary>
    /// <returns></returns>
    internal List<string> GetIncludeAssemblyRules()
    {
        var assemblyRules = new List<string>(DefaultIncludeAssemblyRules);
        assemblyRules.AddRange(IncludeAssemblyRules);
        return assemblyRules.Distinct().ToList();
    }

    /// <summary>
    /// Get the valid set of assembly rules
    /// </summary>
    /// <returns></returns>
    internal List<string> GetExcludeAssemblyRules()
    {
        var excludeAssemblyRules = new List<string>(DefaultExcludeAssemblyRules);
        if (ExcludeAssemblyRules != null && ExcludeAssemblyRules.Any())
            excludeAssemblyRules.AddRange(ExcludeAssemblyRules);
        return excludeAssemblyRules.Distinct().ToList();
    }
}
