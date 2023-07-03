// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public static class LoggerExtensions
{
    public static void LogTrace(this ILogger logger, string tranceId, string messageTemplate, params object?[]? propertyValues)
    {
        logger.Log(LogLevel.Trace, tranceId, messageTemplate, propertyValues);
    }

    public static void LogDebug(this ILogger logger, string messageTemplate, params object?[]? propertyValues)
    {
        logger.Log(LogLevel.Debug, messageTemplate, propertyValues);
    }

    public static void LogInformation(this ILogger logger, string messageTemplate, params object?[]? propertyValues)
    {
        logger.Log(LogLevel.Information, messageTemplate, propertyValues);
    }

    public static void LogWarning(this ILogger logger, string messageTemplate, params object?[]? propertyValues)
    {
        logger.Log(LogLevel.Warning, messageTemplate, propertyValues);
    }

    public static void LogError(this ILogger logger, string messageTemplate, params object?[]? propertyValues)
    {
        logger.Log(LogLevel.Error, messageTemplate, propertyValues);
    }

    public static void LogError(this ILogger logger, Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        logger.Log(LogLevel.Error, exception, messageTemplate, propertyValues);
    }

    public static void LogCritical(this ILogger logger, string messageTemplate, params object?[]? propertyValues)
    {
        logger.Log(LogLevel.Critical, messageTemplate, propertyValues);
    }

    public static void LogCritical(this ILogger logger, Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        logger.Log(LogLevel.Critical, exception, messageTemplate, propertyValues);
    }
}
