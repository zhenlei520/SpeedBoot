// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.Serialization.SystemTextJson.Tests;

[TestClass]
public class JsonSerializerTest : TestBase
{
    [TestMethod]
    public void TestSerialize()
    {
        Build();
        var speedBoot = new User()
        {
            Name = "speedBoot"
        };
        var jsonSerializer = App.Instance.GetRequiredSingletonService<IKeydSingletonService<IJsonSerializer>>().GetService(SystemTextJsonConfig.DefaultKey);
        Assert.IsNotNull(jsonSerializer);
        var content = jsonSerializer.Serialize(speedBoot);
        Assert.AreEqual(true,content.Contains("name"));
    }

    [TestMethod]
    public void TestCustomSerialize()
    {
        Services.AddSystemTextJson();
        Build();
        var speedBoot = new User()
        {
            Name = "speedBoot"
        };
        var jsonSerializer = App.Instance.GetRequiredSingletonService<IKeydSingletonService<IJsonSerializer>>().GetService(SystemTextJsonConfig.DefaultKey);
        Assert.IsNotNull(jsonSerializer);
        var content = jsonSerializer.Serialize(speedBoot);
        Assert.AreEqual(true,content.Contains("Name"));
    }
}
