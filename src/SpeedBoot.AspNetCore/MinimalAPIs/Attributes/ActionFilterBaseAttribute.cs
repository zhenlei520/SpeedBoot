// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET7_0_OR_GREATER

namespace SpeedBoot.AspNetCore;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public abstract class ActionFilterBaseAttribute : Attribute
{
    public Type ServiceType { get; }

    public int Order { get; }

    protected ActionFilterBaseAttribute(Type serviceType, int order)
    {
        ServiceType = serviceType;
        Order = order;
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public abstract class ActionFilterBaseAttribute<TActionFilterProvider> : ActionFilterBaseAttribute
    where TActionFilterProvider : IActionFilterProvider
{
    public ActionFilterBaseAttribute() : this(999)
    {

    }

    public ActionFilterBaseAttribute(int order)
        : base(typeof(TActionFilterProvider), order)
    {
    }
}
#endif
