// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed.Extensions.DependencyInjection;

internal class ServiceDescriptorInfo
{
    public Type ServiceType { get; }

    public Type ImplementationType { get; }

    public ServiceLifetime Lifetime { get; }

    public ServiceDescriptorInfo(Type serviceType, Type implementationType, ServiceLifetime lifetime)
    {
        ServiceType = serviceType;
        ImplementationType = implementationType;
        Lifetime = lifetime;
    }
}
