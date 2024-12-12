// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET7_0_OR_GREATER

namespace SpeedBoot.AspNetCore;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public abstract class EndpointFilterBaseAttribute : Attribute, IMetadataAttribute
{
    public Type ServiceType { get; }

    public int Order { get; }

    protected EndpointFilterBaseAttribute(Type serviceType, int order)
    {
        ServiceType = serviceType;
        Order = order;
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public abstract class EndpointFilterBaseAttribute<TEndpointFilterProvider> : EndpointFilterBaseAttribute
    where TEndpointFilterProvider : IEndpointFilterProvider
{
    public EndpointFilterBaseAttribute() : this(999)
    {
    }

    public EndpointFilterBaseAttribute(int order)
        : base(typeof(TEndpointFilterProvider), order)
    {
    }
}
#endif
