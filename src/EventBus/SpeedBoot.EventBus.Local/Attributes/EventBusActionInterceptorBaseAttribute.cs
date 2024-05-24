// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public abstract class EventBusActionInterceptorBaseAttribute : Attribute
{
    public Type ServiceType { get; }

    public int Order { get; }

    protected EventBusActionInterceptorBaseAttribute(Type serviceType, int order)
    {
        ServiceType = serviceType;
        Order = order;
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public abstract class EventBusActionInterceptorBaseAttribute<TEventBusInterceptor> : EventBusActionInterceptorBaseAttribute
    where TEventBusInterceptor : IEventBusActionInterceptor
{
    public EventBusActionInterceptorBaseAttribute() : this(999)
    {
    }

    public EventBusActionInterceptorBaseAttribute(int order)
        : base(typeof(TEventBusInterceptor), order)
    {
    }
}
