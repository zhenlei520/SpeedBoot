// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class LocalEventHandlerAttribute : LocalEventHandlerBaseAttribute
{
    /// <summary>
    /// Used to cancel or compensate
    /// </summary>
    public bool IsCancel { get; set; }

    public LocalEventHandlerAttribute() : this(999)
    {
    }

    public LocalEventHandlerAttribute(int order) : base(order)
    {
        Order = order;
    }

    internal List<LocalEventHandlerAttribute> GetCancelList(List<LocalEventHandlerAttribute> cancelHandlers)
    {
        var startCancelOrder = FailureLevel switch
        {
            FailureLevel.Throw => Order - 1,
            FailureLevel.ThrowAndCancel => Order,
            _ => throw new NotImplementedException()
        };

        return cancelHandlers.Where(cancelHandler => cancelHandler.Order <= startCancelOrder).ToList();
    }

    internal void SyncExecuteAction<TEvent>(
        IServiceProvider serviceProvider,
        TEvent @event)
        where TEvent : IEvent
    {
        var actionExecutingContext = new ActionExecutingContext(@event, MethodInfo);

        if (EventBusActionInterceptorTypes.Any())
        {
            foreach (var eventBusActionInterceptorType in EventBusActionInterceptorTypes)
            {
                var eventBusInterceptor = serviceProvider.GetRequiredService(eventBusActionInterceptorType) as IEventBusActionInterceptor;
                eventBusInterceptor!.OnActionExecuting(actionExecutingContext);
                SyncInvokeDelegate!.Invoke(serviceProvider.GetRequiredService(InstanceType), GetParameters(serviceProvider, @event));
                eventBusInterceptor!.OnActionExecuted(actionExecutingContext);
            }
        }
        else
        {
            SyncInvokeDelegate!.Invoke(serviceProvider.GetRequiredService(InstanceType), GetParameters(serviceProvider, @event));
        }
    }

    internal async Task ExecuteActionAsync<TEvent>(IServiceProvider serviceProvider, TEvent @event, CancellationToken cancellationToken)
        where TEvent : IEvent
    {
        var actionExecutingContext = new ActionExecutingContext(@event, MethodInfo);

        if (EventBusActionInterceptorTypes.Any())
        {
            foreach (var eventBusActionFilterType in EventBusActionInterceptorTypes)
            {
                var eventBusInterceptor = serviceProvider.GetRequiredService(eventBusActionFilterType) as IEventBusActionInterceptor;
                eventBusInterceptor!.OnActionExecuting(actionExecutingContext);
                await TaskInvokeDelegate!.Invoke(serviceProvider.GetRequiredService(InstanceType), GetParameters(serviceProvider, @event, cancellationToken));
                eventBusInterceptor!.OnActionExecuted(actionExecutingContext);
            }
        }
        else
        {
            await TaskInvokeDelegate!.Invoke(serviceProvider.GetRequiredService(InstanceType), GetParameters(serviceProvider, @event, cancellationToken));
        }
    }
}
