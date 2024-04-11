// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class RecordTimeAttribute : EventBusActionFilterBaseAttribute<RecordTimeEventBusInterceptor>
{
    public RecordTimeAttribute() : this(999)
    {
    }

    public RecordTimeAttribute(int order) : base(order)
    {
    }
}
