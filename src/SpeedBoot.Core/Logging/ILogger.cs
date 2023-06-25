// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public interface ILogger
{
    void Log<TState>(TState state, LogLevel logLevel, Exception? exception);
}
