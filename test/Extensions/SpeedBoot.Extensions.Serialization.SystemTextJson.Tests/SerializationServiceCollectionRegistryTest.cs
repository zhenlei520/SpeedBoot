// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.Serialization.SystemTextJson.Tests;

[TestClass]
public class SerializationServiceCollectionRegistryTest: TestBase
{
    [TestMethod]
    public void TestTryAdd()
    {
        Assert.AreEqual(true,SerializationServiceCollectionRegistry.TryAdd(Services, "test"));
        Assert.AreEqual(false,SerializationServiceCollectionRegistry.TryAdd(Services, "test"));
        Services.AddSingleton<User>();
        Assert.AreEqual(false,SerializationServiceCollectionRegistry.TryAdd(Services, "test"));

        Assert.AreEqual(true,SerializationServiceCollectionRegistry.TryAdd(new ServiceCollection(), "test"));
    }
}
