// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public abstract class EventBusActionFilterBaseAttribute : Attribute
{
    public Type ServiceType { get; }

    public int Order { get; }

    public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Transient;

    protected EventBusActionFilterBaseAttribute(Type serviceType, int order)
    {
        ServiceType = serviceType;
        Order = order;
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public abstract class EventBusActionFilterBaseAttribute<TEventBusInterceptor> : EventBusActionFilterBaseAttribute
    where TEventBusInterceptor : IEventBusInterceptor
{
    public EventBusActionFilterBaseAttribute() : this(999)
    {
    }

    public EventBusActionFilterBaseAttribute(int order)
        : base(typeof(TEventBusInterceptor), order)
    {
    }

    public EventBusActionFilterBaseAttribute(int order, ServiceLifetime serviceLifetime)
        : this(order)
    {
        ServiceLifetime = serviceLifetime;
    }
}
