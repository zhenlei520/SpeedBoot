// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class RecordTimeActionFilterAttribute : EventBusActionFilterBaseAttribute<RecordTimeEventBusActionFilterProvider>
{
    public RecordTimeActionFilterAttribute() : this(999)
    {
    }

    public RecordTimeActionFilterAttribute(int order) : base(order)
    {
    }
}
