// Copyright (c) zhenlei520 All rights reserved.
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
    public int RetryTimes { get; set; }

    public bool RetryEnabled { get; set; }

    internal Type InstanceType { get; set; }

    /// <summary>
    /// todo: await remove
    /// </summary>
    internal Type EventType { get; set; }

    internal Type[] ParameterTypes { get; private set; }

    internal MethodInfo MethodInfo { get; private set; }

    internal int MaxRetryTimes { get; private set; }

    internal bool IsSyncMethod { get; set; }

    internal bool HasReturnValue { get; private set; }

    internal TaskInvokeDelegate? InvokeDelegate { get; private set; }
    internal TaskInvokeDelegate2? InvokeDelegate2 { get; private set; }

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
        HasReturnValue = MethodInfo.HasReturnValue();
    }

    internal void BuildExpression(Type instanceType, MethodInfo methodInfo, Type eventType)
    {
        Initialize(instanceType, methodInfo, eventType);
        if (HasReturnValue)
        {
            InvokeDelegate2 = ExpressionBuilder.Build2(MethodInfo, InstanceType);
        }
        else
        {
            InvokeDelegate = ExpressionBuilder.Build(MethodInfo, InstanceType);
        }
    }

    protected object?[] GetParameters<TEvent>(IServiceProvider serviceProvider, TEvent @event, CancellationToken cancellationToken)
        where TEvent : IEvent
    {
        var parameters = new object?[ParameterTypes.Length];
        for (var index = 0; index < ParameterTypes.Length; index++)
        {
            if (ParameterTypes[index] == @event.GetType())
                parameters[index] = @event;
            else if (ParameterTypes[index] == typeof(CancellationToken))
                parameters[index] = cancellationToken;
            else
                parameters[index] = serviceProvider.GetService(ParameterTypes[index]);
        }
        return parameters;
    }
}
