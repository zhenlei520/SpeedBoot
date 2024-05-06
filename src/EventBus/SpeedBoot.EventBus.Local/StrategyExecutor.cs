// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class StrategyExecutor : IStrategyExecutor
{
    private readonly ILogger? _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly LocalEventExecuteContext _localEventExecutor;

    public StrategyExecutor(IServiceProvider serviceProvider, LocalEventExecuteContext localEventExecuteContext, ILogger? logger = null)
    {
        _serviceProvider = serviceProvider;
        _localEventExecutor = localEventExecuteContext;
        _logger = logger;
    }

    public void Execute<TEvent>(DispatchRecord dispatchRecord, TEvent @event) where TEvent : IEvent
    {
        var isCancel = false;
        _localEventExecutor.Execute();

        foreach (var handler in dispatchRecord.Handlers)
        {
            if (handler.IsSyncMethod)
            {
                handler.SyncExecuteAction(_serviceProvider, @event);
            }
            else
            {
                handler.ExecuteActionAsync(_serviceProvider, @event, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }
    }

    public async Task ExecuteAsync<TEvent>(DispatchRecord dispatchRecord, TEvent @event, CancellationToken cancellationToken)
        where TEvent : IEvent
    {
        _localEventExecutor.Execute();
        await ExecuteAsync(dispatchRecord.Handlers, @event, () =>
        {
            _logger?.LogDebug("Publish event, event id: {EventId}, event: {@Event}", @event.GetId(), @event);
            _localEventExecutor.Succeed();
        }, async (handlerException, handler) =>
        {
            if (handler.FailureLevel != FailureLevel.Ignore)
            {
                _localEventExecutor.Exception = handlerException;
                (bool IsSucceed, Exception? CancelException)? cancelHandlerResult = null;
                if (dispatchRecord.CancelHandlers.Any())
                {
                    cancelHandlerResult = await ExecuteCancelHandlerAsync(
                        handler.GetCancelList(dispatchRecord.CancelHandlers),
                        @event,
                        cancellationToken);
                }
                switch (cancelHandlerResult)
                {
                    case { IsSucceed: false }:
                        _localEventExecutor.Exception!.Data.Add("cancel handler exception", cancelHandlerResult.Value.CancelException);
                        _localEventExecutor.RollbackFailed();
                        break;
                    case { IsSucceed: true }:
                        _localEventExecutor.RollbackRollbackSucceeded();
                        break;
                }
            }
            else
            {
                _logger?.LogError(
                    "Publishing event error is ignored, event id: {EventId}, instance: {InstanceName}, method: {MethodName}, event: {@Event}",
                    @event.GetId(),
                    handler.InstanceType.FullName ?? handler.InstanceType.Name,
                    handler.MethodInfo.Name,
                    @event);
            }

            if (_localEventExecutor.Exception is not null)
                ExceptionDispatchInfo.Capture(_localEventExecutor.Exception).Throw();

            return handler.FailureLevel != FailureLevel.Ignore;
        }, cancellationToken);
    }

    public async Task<(bool IsSucceed, Exception? CancelException)> ExecuteAsync<TEvent>(
        IEnumerable<LocalEventHandlerAttribute> handlers,
        TEvent @event,
        Action? succeedAction,
        Func<Exception?, LocalEventHandlerAttribute, Task<bool>>? failedFunc,
        CancellationToken cancellationToken)
        where TEvent : IEvent
    {
        var isSucceed = true;
        Exception? exception = null;
        var isReturn = false;
        foreach (var handler in handlers)
        {
            await ExecuteAsync(handler, @event, async () =>
            {
                if (handler.IsSyncMethod)
                {
                    handler.SyncExecuteAction(_serviceProvider, @event);
                }
                else
                {
                    await handler.ExecuteActionAsync(_serviceProvider, @event, cancellationToken);
                }

                succeedAction?.Invoke();
            }, async (ex, failureLevel) =>
            {
                if (failureLevel != FailureLevel.Ignore)
                {
                    isSucceed = false;
                    exception = ex;
                }
                if (failedFunc != null)
                {
                    isReturn = await failedFunc.Invoke(ex, handler);
                }
            });
            if (exception is not null && isReturn)
                return (isSucceed, exception);
        }
        return (isSucceed, exception);
    }

    public async Task ExecuteAsync<TEvent>(
        LocalEventHandlerAttribute eventHandlerAttribute,
        TEvent @event,
        Func<Task> func,
        Func<Exception, FailureLevel, Task>? cancel)
        where TEvent : IEvent
    {
        var retryTimes = 0;
        Exception? exception;

        do
        {
            try
            {
                await func.Invoke();
                return;
            }
            catch (Exception ex)
            {
                exception = ex;

                if (retryTimes++ > 0)
                {
                    _logger?.LogError(ex,
                        $"Strategy execute event failed [{{RetryTimes}} / {eventHandlerAttribute.MaxRetryTimes}], event id: {{EventId}}, event: {{@Event}}",
                        retryTimes, @event.GetId(), @event);
                }
            }
        } while (retryTimes <= eventHandlerAttribute.MaxRetryTimes);

        if (cancel != null)
        {
            await cancel.Invoke(exception, eventHandlerAttribute.FailureLevel);
        }
    }

    async Task<(bool IsSucceed, Exception? CancelException)> ExecuteCancelHandlerAsync<TEvent>(
        IEnumerable<LocalEventHandlerAttribute> cancelHandlers,
        TEvent localEvent,
        CancellationToken cancellationToken)
        where TEvent : IEvent
    {
        return await ExecuteAsync(cancelHandlers, localEvent, () =>
        {
            _logger?.LogDebug("Publish cancel event, event id: {EventId}, event: {@Event}", localEvent.GetId(), localEvent);
        }, (ex, failureLevel) =>
        {
            _logger?.LogError("Publish cancel event ignored, event id: {EventId}, event: {@Event}", localEvent.GetId(), localEvent);
            return Task.FromResult(true);
        }, cancellationToken);
    }
}
