// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public abstract class AppStartupBase : OrderBase, IAppStartup
{
    private bool _initialized;

    private ILogger? _logger;

    private ILogger? Logger
    {
        get
        {
            if (!_initialized)
            {
                _initialized = true;
                _logger = _loggerLazy.Value;
            }

            return _logger;
        }
    }

    private readonly Lazy<ILogger?> _loggerLazy;
    private readonly LogLevel? _logLevel;

    public abstract string Name { get; }

    protected AppStartupBase(Lazy<ILogger?> loggerLazy, LogLevel? logLevel = null)
    {
        _loggerLazy = loggerLazy;
        _logLevel = logLevel;
    }

    protected List<Type> GetDerivedClassTypes<TService>(IEnumerable<Assembly> assemblies)
    {
        return assemblies.GetTypes(type => type is { IsClass: true, IsGenericType: false, IsAbstract: false } && typeof(TService).IsAssignableFrom(type));
    }

    protected virtual void PreInitialized()
    {
        Logger?.Log(_logLevel ?? LogLevel.Debug, messageTemplate: "AppStartup：【{0}】，before initialization", propertyValues: Name);
    }

    public virtual void Initialize()
    {
        PreInitialized();

        Load();

        PostInitialized();
    }

    protected abstract void Load();

    protected virtual void PostInitialized()
    {
        Logger?.Log(_logLevel ?? LogLevel.Debug, messageTemplate: "AppStartup：【{0}】，after initialization", propertyValues: Name);
    }
}
