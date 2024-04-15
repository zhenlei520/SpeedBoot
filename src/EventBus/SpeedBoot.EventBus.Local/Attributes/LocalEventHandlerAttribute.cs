// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class LocalEventHandlerAttribute : Attribute
{
    /// <summary>
    /// Used to control the order in which methods are executed, in ascending order. default is int.MaxValue
    /// Must be greater than or equal to 0
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// The default value is 3，EnableRetry must be true to take effect
    /// </summary>
    public int RetryTimes { get; set; }

    /// <summary>
    /// Used to cancel or compensate
    /// </summary>
    public bool IsCancel { get; set; }

    public bool RetryEnabled { get; set; }

    internal Type InstanceType { get; set; }

    internal Type EventType { get; set; }

    internal Type[] ParameterTypes { get; private set; }

    internal MethodInfo MethodInfo { get; private set; }

    internal int MaxRetryTimes { get; private set; }

    internal bool IsSyncMethod { get; set; }

    internal bool HasReturnValue { get; private set; }

    public LocalEventHandlerAttribute() : this(999)
    {
    }

    public LocalEventHandlerAttribute(int order)
    {
        Order = order;
    }

    private TaskInvokeDelegate? _invokeDelegate;

    internal void BuildExpression()
    {
        _invokeDelegate = ExpressionBuilder.Build(MethodInfo, InstanceType);
        MaxRetryTimes = RetryEnabled ? RetryTimes : 0;
        IsSyncMethod = MethodInfo.IsSyncMethod();
        HasReturnValue = MethodInfo.HasReturnValue();
    }

    internal async Task ExecuteActionAsync<TEvent>(IServiceProvider serviceProvider, TEvent @event, CancellationToken cancellationToken)
        where TEvent : IEvent
    {

    }

    internal async Task<object> ExecuteAction2Async<TEvent>(IServiceProvider serviceProvider, TEvent @event, CancellationToken cancellationToken)
        where TEvent : IEvent
    {

    }
}
