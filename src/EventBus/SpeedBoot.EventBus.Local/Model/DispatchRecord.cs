// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class DispatchRecord
{
    public List<LocalEventHandlerAttribute> Handlers { get; set; }

    public List<LocalEventHandlerAttribute> CancelHandlers { get; set; }

    public DispatchRecord()
    {
        Handlers = new();
        CancelHandlers = new();
    }

    internal void AddHandlers(LocalEventHandlerAttribute handler)
    {
        if (handler.IsCancel)
            CancelHandlers.Add(handler);
        else
            Handlers.Add(handler);
    }
}
