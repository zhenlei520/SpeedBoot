﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class LocalEventHandlerBaseAttribute : Attribute
{
    /// <summary>
    /// Used to control the order in which methods are executed, in ascending order. default is int.MaxValue
    /// Must be greater than or equal to 0
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// The default value is 3，EnableRetry must be true to take effect
    /// </summary>
    public int RetryTimes { get; set; } = 3;

    public bool RetryEnabled { get; set; }

    internal Type InstanceType { get; set; }

    internal Type EventType { get; set; }

    internal Type[] ParameterTypes { get; private set; }

    internal MethodInfo MethodInfo { get; private set; }

    internal int MaxRetryTimes { get; private set; }

    public FailureLevel FailureLevel { get; set; }

    internal bool IsSyncMethod { get; set; }

    internal Action<object, object?[]>? SyncInvokeDelegate { get; private set; }

    internal Func<object, object?[], Task>? TaskInvokeDelegate { get; private set; }

    internal Type[] EventBusActionInterceptorTypes { get; private set; }

    public LocalEventHandlerBaseAttribute(int order)
    {
        Order = order;
    }

    private void Initialize(Type instanceType, MethodInfo methodInfo, Type eventType)
    {
        InstanceType = instanceType;
        EventType = eventType;
        MaxRetryTimes = RetryEnabled ? RetryTimes : 0;
        MethodInfo = methodInfo;
        ParameterTypes = methodInfo.GetParameters().Select(parameterInfo => parameterInfo.ParameterType).ToArray();
        IsSyncMethod = MethodInfo.IsSyncMethod();

        EventBusActionInterceptorTypes = MethodInfo.GetCustomAttributes<EventBusActionInterceptorBaseAttribute>().OrderBy(a => a.Order).Select(a=> a.ServiceType).ToArray();
    }

    internal void BuildExpression(Type instanceType, MethodInfo methodInfo, Type eventType)
    {
        Initialize(instanceType, methodInfo, eventType);

        if (IsSyncMethod)
        {
            SyncInvokeDelegate = MethodExpressionUtils.BuildSyncInvokeDelegate(InstanceType, MethodInfo);
        }
        else
        {
            TaskInvokeDelegate = MethodExpressionUtils.BuildTaskInvokeDelegate(InstanceType, MethodInfo);
        }
    }

    protected object?[] GetParameters<TEvent>(IServiceProvider serviceProvider, TEvent @event, CancellationToken? cancellationToken = null)
        where TEvent : IEvent
    {
        var parameters = new object?[ParameterTypes.Length];
        for (var index = 0; index < ParameterTypes.Length; index++)
        {
            if (ParameterTypes[index] == @event.GetType())
                parameters[index] = @event;
            else if (ParameterTypes[index] == typeof(CancellationToken))
                parameters[index] = cancellationToken ?? CancellationToken.None;
            else
                parameters[index] = serviceProvider.GetService(ParameterTypes[index]);
        }
        return parameters;
    }
}
