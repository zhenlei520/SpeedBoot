// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Abstracts;

public class EventBus(IServiceProvider serviceProvider) : IEventBus
{
    private ILocalEventBus? _localEventBus;
    private IIntegrationEventBus? _integrationEventBus;

    public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        if (@event is IIntegrationEvent integrationEvent)
            GetIntegrationEventBus().Publish(integrationEvent);

        else GetLocalEventBus().Publish(@event);
    }

    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
    {
        if (@event is IIntegrationEvent integrationEvent)
            return GetIntegrationEventBus().PublishAsync(integrationEvent, cancellationToken);

        return GetLocalEventBus().PublishAsync(@event, cancellationToken);
    }

    ILocalEventBus GetLocalEventBus() =>
        serviceProvider.GetRequiredService<ILocalEventBus>();

    IIntegrationEventBus GetIntegrationEventBus()=>
        serviceProvider.GetRequiredService<IIntegrationEventBus>();
}
