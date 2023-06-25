// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public class ServiceDescriptorModel
{
    public Type ServiceType { get; }

    public Type ImplementationType { get; }

    public ServiceLifetime Lifetime { get; }

    public ServiceDescriptorModel(Type serviceType, Type implementationType, ServiceLifetime lifetime)
    {
        ServiceType = serviceType;
        ImplementationType = implementationType;
        Lifetime = lifetime;
    }
}
