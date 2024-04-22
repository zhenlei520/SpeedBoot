// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class LocalEventBus: ILocalEventBus
{
    private readonly ILogger? _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILocalEventBusMesh _localEventBusMesh;

    public LocalEventBus(
        ILocalEventBusMesh localEventBusMesh,
        IServiceProvider serviceProvider,
        ILogger? logger = null)
    {
        _localEventBusMesh = localEventBusMesh;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        SpeedArgumentException.ThrowIfNull(@event);

        var eventType = @event.GetType();
        if (!_localEventBusMesh.MeshData.TryGetValue(eventType, out var eventHandlers))
            throw new InvalidOperationException($"The {eventType.FullName} handler method was not found. Ensure the event has a handler or the handler's assembly is loaded by AppDomain");

        var isCancel = false;
        foreach (var handler in eventHandlers.Handlers)
        {
            handler.SyncExecuteAction(_serviceProvider, @event);
        }

    }

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
    {
        SpeedArgumentException.ThrowIfNull(@event);

        var eventType = @event.GetType();
        if (!_localEventBusMesh.MeshData.TryGetValue(eventType, out var eventHandlers))
            throw new InvalidOperationException($"The {eventType.FullName} handler method was not found. Ensure the event has a handler or the handler's assembly is loaded by AppDomain");

        var isCancel = false;
        foreach (var handler in eventHandlers.Handlers)
        {
            await handler.ExecuteActionAsync(_serviceProvider, @event, cancellationToken);
        }
    }
}
