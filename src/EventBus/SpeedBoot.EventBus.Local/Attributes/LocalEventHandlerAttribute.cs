// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class LocalEventHandlerAttribute : LocalEventHandlerBaseAttribute
{
    /// <summary>
    /// Used to cancel or compensate
    /// </summary>
    public bool IsCancel { get; set; }

    public LocalEventHandlerAttribute() : this(999)
    {
    }

    public LocalEventHandlerAttribute(int order) : base(order)
    {
        Order = order;
    }

    internal void SyncExecuteAction<TEvent>(
        IServiceProvider serviceProvider,
        TEvent @event,
        CancellationToken cancellationToken)
        where TEvent : IEvent
    {
        SyncInvokeDelegate!.Invoke(serviceProvider.GetRequiredService(InstanceType),
            GetParameters(serviceProvider, @event, cancellationToken));
    }

    internal TResponse SyncExecuteActionWithResult<TResponse, TEvent>(
        IServiceProvider serviceProvider,
        TEvent @event,
        CancellationToken cancellationToken)
        where TEvent : IEvent
    {
        return (TResponse)SyncInvokeWithResultDelegate!.Invoke(serviceProvider.GetRequiredService(InstanceType),
            GetParameters(serviceProvider, @event, cancellationToken));
    }

    internal Task ExecuteActionAsync<TEvent>(IServiceProvider serviceProvider, TEvent @event, CancellationToken cancellationToken)
        where TEvent : IEvent
    {
        return TaskInvokeDelegate!.Invoke(serviceProvider.GetRequiredService(InstanceType),
            GetParameters(serviceProvider, @event, cancellationToken));
    }

    internal async Task<TResponse> ExecuteActionWithResultAsync<TResponse, TEvent>(IServiceProvider serviceProvider, TEvent @event,
        CancellationToken cancellationToken)
        where TEvent : IEvent
    {
        var result = await TaskInvokeWithResultDelegate!.Invoke(serviceProvider.GetRequiredService(InstanceType),
            GetParameters(serviceProvider, @event, cancellationToken));
        return (TResponse)result;
    }
}
