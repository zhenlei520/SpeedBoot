// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class RecordTimeEventBusActionFilterProvider : EventBusActionFilterProviderBase
{
    private readonly Stopwatch _stopwatch;
    private readonly ILogger? _logger;
    private readonly LogLevel _logLevel;

    public RecordTimeEventBusActionFilterProvider(ILogger? logger = null, RecordTimeOption? recordTimeOption = null)
    {
        _stopwatch = new Stopwatch();
        _logger = logger;
        _logLevel = recordTimeOption?.LogLevel ?? LogLevel.Information;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch.Start();
    }

    public override void OnActionExecuted(ActionExecutingContext context)
    {
        _stopwatch.Stop();
        _logger?.Log(_logLevel, "[Class][MethodName] executed in {ElapsedMilliseconds} ms",
            context.MethodInfo.DeclaringType?.FullName ?? context.MethodInfo.DeclaringType?.Name ?? "",
            context.MethodInfo.Name,
            _stopwatch.ElapsedMilliseconds);
    }
}
