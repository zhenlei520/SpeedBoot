// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

internal static class ConfigurationExtensions
{
    public static Assembly[]? GetAssemblies(this IConfiguration configuration)
    {
        if (!configuration.GetEnableAssemblyRulesState())
        {
            return null;
        }

        var assemblyOptions = new AssemblyOptions();

        var includeAssemblyRules = configuration.GetRules("SpeedBoot:IncludeAssemblyRules");
        if (includeAssemblyRules != null)
        {
            assemblyOptions.IncludeAssemblyRules = includeAssemblyRules;
        }
        else
        {
            var assemblyName = configuration["SpeedBoot:AssemblyName"];
            if (assemblyName != null) assemblyOptions.IncludeAssemblyRules = [assemblyName];
        }

        var excludeAssemblyRules = configuration.GetRules("SpeedBoot:ExcludeAssemblyRules");
        if (excludeAssemblyRules != null)
        {
            assemblyOptions.ExcludeAssemblyRules = excludeAssemblyRules;
        }

        var defaultExcludeAssemblyRules = configuration.GetRules("SpeedBoot:DefaultExcludeAssemblyRules");
        if (defaultExcludeAssemblyRules != null)
        {
            assemblyOptions.DefaultExcludeAssemblyRules = defaultExcludeAssemblyRules;
        }

        return GetValidAssemblies(assemblyOptions);
    }

    private static Assembly[] GetValidAssemblies(AssemblyOptions assemblyOptions)
    {
        Expression<Func<string, bool>> condition = name => true;
        var includeAssemblyRules = assemblyOptions.GetIncludeAssemblyRules();
        var excludeAssemblyRules = assemblyOptions.GetExcludeAssemblyRules();
        condition = condition.And(
            includeAssemblyRules.Count > 0 || excludeAssemblyRules.Count > 0,
            assemblyName =>
                includeAssemblyRules.Any(n => Regex.Match(assemblyName, n).Success) &&
                !excludeAssemblyRules.Any(n => Regex.Match(assemblyName, n).Success));
        return AssemblyUtils.GetAllAssembly(condition);
    }

    private static bool GetEnableAssemblyRulesState(this IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection("SpeedBoot").GetSection("EnableAssemblyRulesState");
        return !configurationSection.Exists() || configurationSection.Get<bool>();
    }

    private static List<string>? GetRules(this IConfiguration configuration, string sectionName)
    {
        var rules = new List<string>();
        var configurationSection = configuration.GetSection(sectionName);
        if (!configurationSection.Exists())
        {
            return null;
        }

        configurationSection.Bind(rules);

        if (rules.IsAny())
        {
            return rules;
        }

        var rulesStr = configuration[sectionName];
        return rulesStr?.Split(';').ToList();
    }
}
