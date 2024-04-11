// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public abstract class EventBusInterceptorBase : IEventBusInterceptor
{
    public abstract void OnActionExecuting(ActionExecutingContext context);

    public abstract void OnActionExecuted(ActionExecutingContext context);
}
