// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class ActionExecutingContext
{
    public bool Continued { get; private set; }

    public bool Finished { get; private set; }

    public MethodInfo MethodInfo { get; set; }

    public IEvent Event { get; private set; }

    public ActionExecutingContext(IEvent @event, MethodInfo methodInfo)
    {
        Event = @event;
        MethodInfo = methodInfo;
    }
}
