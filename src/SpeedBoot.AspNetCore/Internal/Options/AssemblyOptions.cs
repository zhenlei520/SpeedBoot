// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.AspNetCore;

/// <summary>
///
/// 程序集配置
/// </summary>
internal class AssemblyOptions
{
    public List<string> IncludeAssemblyRules { get; set; }

    public List<string> DefaultIncludeAssemblyRules { get; set; } = new()
    {
        "^SpeedBoot.*"
    };

    public List<string>? ExcludeAssemblyRules { get; set; }

    public List<string> DefaultExcludeAssemblyRules { get; set; } = new()
    {
        "^System.*",
        "^Microsoft.*",
        "^Serilog.*",
        "^Swashbuckle.*",
        "^Npgsql.*"
    };

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
