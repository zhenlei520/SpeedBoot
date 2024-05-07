// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class LocalEventBus : ILocalEventBus
{
    private readonly IStrategyExecutor _strategyExecutor;
    private readonly ILocalEventBusMesh _localEventBusMesh;

    public LocalEventBus(
        ILocalEventBusMesh localEventBusMesh,
        IStrategyExecutor strategyExecutor)
    {
        _localEventBusMesh = localEventBusMesh;
        _strategyExecutor = strategyExecutor;
    }

    public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        var dispatchRecord = GetDispatchRecord(@event);
        _strategyExecutor.Execute(dispatchRecord, @event);
    }

    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
    {
        var dispatchRecord = GetDispatchRecord(@event);
        return _strategyExecutor.ExecuteAsync(dispatchRecord, @event, cancellationToken);
    }

    private DispatchRecord GetDispatchRecord<TEvent>(TEvent @event)
    {
        SpeedArgumentException.ThrowIfNull(@event);

        var eventType = @event.GetType();
        if (!_localEventBusMesh.MeshData.TryGetValue(eventType, out var dispatchRecord))
            throw new InvalidOperationException(
                $"The {eventType.FullName} handler method was not found. Ensure the event has a handler or the handler's assembly is loaded by AppDomain");

        return dispatchRecord;
    }
}
