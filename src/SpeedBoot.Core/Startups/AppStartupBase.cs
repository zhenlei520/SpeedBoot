// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public abstract class AppStartupBase : IAppStartup
{
    private readonly ILogger? _logger;
    private readonly LogLevel? _logLevel;

    public virtual int Order => 999;

    public abstract string Name { get; }

    protected AppStartupBase(ILogger? logger, LogLevel? logLevel = null)
    {
        _logger = logger;
        _logLevel = logLevel;
    }

    protected virtual void PreInitialized()
    {
        _logger?.Log($"AppStartup：【{Name}】，初始化前", _logLevel ?? LogLevel.Debug, null);
    }

    public virtual void Initialized()
    {
        PreInitialized();

        Load();

        PostInitialized();
    }

    protected abstract void Load();

    protected virtual void PostInitialized()
    {
        _logger?.Log($"AppStartup：【{Name}】，初始化后", _logLevel ?? LogLevel.Debug, null);
    }
}
