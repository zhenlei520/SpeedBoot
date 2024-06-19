// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local.Tests;

[TestClass]
public class EventBusTest
{
    private readonly IServiceProvider _rootServiceProvider;

    public EventBusTest()
    {
        var services = new ServiceCollection();

        //todo: Temporary solution, follow up according to the selected di
        App.Instance.RebuildRootServiceProvider ??= s => s.BuildServiceProvider();

        services.AddSpeedBoot().Build();
        // services.AddAutoInject();
        _rootServiceProvider = services.BuildServiceProvider();
        // App.Instance.GetSpeedBootApplication().SetServiceProvider(_rootServiceProvider);
    }

    [TestMethod]
    public void SendRegisterUserEvent()
    {
        using var scope = _rootServiceProvider.CreateScope();
        try
        {
            var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
            var @event = new RegisterUserEvent()
            {
                Name = "SpeedBoot",
                Age = 18,
            };
            eventBus.Publish(@event);
        }
        catch (Exception ex)
        {
            Assert.AreEqual("custom exception", ex.Message);
        }
    }
}
