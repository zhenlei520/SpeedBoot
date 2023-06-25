// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

[assembly: InternalsVisibleTo("SpeedBoot.Extensions.DependencyInjection.Tests")]

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public class DefaultAutoInjectProvider
{
    private readonly List<Type> _allTypes;

    public DefaultAutoInjectProvider(IEnumerable<Assembly> assemblies)
    {
        _allTypes = assemblies.GetTypes();
    }

    public List<ServiceDescriptorModel> GetServiceDescriptors()
    {
        var serviceDescriptors = new List<ServiceDescriptorModel>();
        serviceDescriptors.AddRange(GetServiceDescriptors(typeof(ISingletonDependency), ServiceLifetime.Singleton));
        serviceDescriptors.AddRange(GetServiceDescriptors(typeof(IScopedDependency), ServiceLifetime.Scoped));
        serviceDescriptors.AddRange(GetServiceDescriptors(typeof(ITransientDependency), ServiceLifetime.Transient));
        return serviceDescriptors;
    }

    internal List<ServiceDescriptorModel> GetServiceDescriptors(Type dependencyType, ServiceLifetime serviceLifetime)
    {
        var serviceTypes = GetServiceTypes(dependencyType);
        var serviceDescriptors = new List<ServiceDescriptorModel>();
        foreach (var serviceType in serviceTypes)
        {
            var implementationTypes = GetImplementationTypes(serviceType);
            serviceDescriptors.AddRange(implementationTypes.Select(implementationType =>
                new ServiceDescriptorModel(serviceType, implementationType, serviceLifetime)));
        }

        return serviceDescriptors;
    }

    internal List<Type> GetServiceTypes(Type dependencyType)
    {
        var interfaceServiceTypes =
            _allTypes.Where(type => type.IsInterface && type != dependencyType && dependencyType.IsAssignableFrom(type))
                .Select(type => type.GetTypeInfo());
        var classServiceTypes = _allTypes.Where(type =>
            !(type.IsAbstract || !type.IsClass) && dependencyType.IsAssignableFrom(type) &&
            ((type.IsGenericType && !type.GetInterfaces().Any(t => interfaceServiceTypes.Contains(t.GetGenericTypeDefinition()))) ||
             (!type.IsGenericType && !type.GetInterfaces().Any(t => interfaceServiceTypes.Contains(t)))));

        var list = new List<Type>(interfaceServiceTypes).Concat(classServiceTypes);
        return list.Distinct().ToList();
    }

    internal List<Type> GetImplementationTypes(Type serviceType)
    {
        return _allTypes.Where(type =>
            IsAssignableFrom(serviceType, type) &&
            ((type != serviceType && serviceType.IsInterface) || (type == serviceType && serviceType.IsClass))).ToList();
    }

    private static bool IsAssignableFrom(Type serviceType, Type implementationType)
    {
        switch (serviceType.IsGenericType)
        {
            case true when !implementationType.IsGenericType:
            case false when implementationType.IsGenericType:
            case true when serviceType.GetTypeInfo().GenericTypeParameters.Length !=
                           implementationType.GetTypeInfo().GenericTypeParameters.Length:
                return false;
            case true:
                return implementationType.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == serviceType);
            default:
                return serviceType.IsAssignableFrom(implementationType);
        }
    }
}
