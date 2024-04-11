// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class LocalEventHandlerAttribute : Attribute
{
    public int Order { get; set; }

    public LocalEventHandlerAttribute() : this(999)
    {
    }

    public LocalEventHandlerAttribute(int order)
    {
        Order = order;
    }
}
