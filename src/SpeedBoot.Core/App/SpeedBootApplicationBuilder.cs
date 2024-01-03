// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public class SpeedBootApplicationBuilder
{
    private List<Assembly> _assemblies;
    public IReadOnlyList<Assembly> Assemblies => _assemblies;

    private readonly SpeedBootApplication _speedBootApplication;
    public IServiceCollection Services => _speedBootApplication.Services;
    public IServiceProvider? RootServiceProvider => _speedBootApplication.RootServiceProvider;
    public string? Environment { get; private set; }

    internal SpeedBootApplicationBuilder(SpeedBootApplication speedBootApplication, string? environment)
    {
        _assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        _speedBootApplication = speedBootApplication;
        Environment = environment;
    }

    /// <summary>
    /// Initialization module
    /// </summary>
    public void Initialize() => _speedBootApplication.InitializeComponents();

    public void SetServiceProvider(IServiceProvider rootServiceProvider)
    {
        _speedBootApplication.SetServiceProvider(rootServiceProvider);
    }

    public void SetAssemblies(IEnumerable<Assembly> assemblies)
    {
        _assemblies = assemblies.ToList();
    }

    public void AddAssembly(Assembly assembly) => _assemblies.Add(assembly);

    public void TryAddAssembly(Assembly assembly)
    {
        if (_assemblies.Contains(assembly))
            return;

        _assemblies.Add(assembly);
    }
}
