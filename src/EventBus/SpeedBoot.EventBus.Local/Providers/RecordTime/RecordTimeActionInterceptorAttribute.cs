// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class RecordTimeActionInterceptorAttribute : EventBusActionInterceptorBaseAttribute<RecordTimeEventBusActionInterceptor>
{
    public RecordTimeActionInterceptorAttribute() : this(999)
    {
    }

    public RecordTimeActionInterceptorAttribute(int order) : base(order)
    {
    }
}
