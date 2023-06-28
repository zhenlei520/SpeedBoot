// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public interface ILogger
{
    void Log(LogLevel logLevel, string tranceId, string messageTemplate, params object?[]? propertyValues);

    void Log(LogLevel logLevel, string messageTemplate, params object?[]? propertyValues);

    void Log(LogLevel logLevel, Exception? exception, string messageTemplate, params object?[]? propertyValues);
}
