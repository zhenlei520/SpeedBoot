// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.DependencyInjection.Tests;

[TestClass]
public class DefaultAutoInjectProviderTest
{
    private readonly DefaultAutoInjectProvider _autoInjectProvider;

    public DefaultAutoInjectProviderTest()
    {
        var assemblies = new List<Assembly>()
        {
            typeof(DefaultAutoInjectProviderTest).Assembly
        };
        _autoInjectProvider = new DefaultAutoInjectProvider(assemblies);
    }

    [TestMethod]
    public void TestGetServiceTypes()
    {
        var serviceTypes = _autoInjectProvider.GetServiceTypes(typeof(IScopedDependency));

        Assert.AreEqual(3, serviceTypes.Count);
        Assert.IsTrue(serviceTypes.Contains(typeof(IRepository<>)));
        Assert.IsTrue(serviceTypes.Contains(typeof(IUserRepository)));
        Assert.IsTrue(serviceTypes.Contains(typeof(CustomService)));
    }

    [TestMethod]
    public void TestGetServiceDescriptors()
    {
        var serviceDescriptors = _autoInjectProvider.GetServiceDescriptors(typeof(IScopedDependency), ServiceLifetime.Scoped);
        Assert.AreEqual(3, serviceDescriptors.Count);

        Assert.IsTrue(serviceDescriptors.Any(des =>
            des.ServiceType == typeof(IRepository<>) && des.ImplementationType == typeof(Repository<>) &&
            des.Lifetime == ServiceLifetime.Scoped));
        Assert.IsTrue(serviceDescriptors.Any(des =>
            des.ServiceType == typeof(IUserRepository) && des.ImplementationType == typeof(UserRepository) &&
            des.Lifetime == ServiceLifetime.Scoped));
        Assert.IsTrue(serviceDescriptors.Any(des =>
            des.ServiceType == typeof(CustomService) && des.ImplementationType == typeof(CustomService) &&
            des.Lifetime == ServiceLifetime.Scoped));
    }

    [TestMethod]
    public void TestAutoInject()
    {
        var services = new ServiceCollection();
        services.AddAutoInject(AppDomain.CurrentDomain.GetAssemblies());

        Assert.AreEqual(4, services.Count);
    }
}
