// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class LocalEventBusOptions
{
    public Assembly[]? Assemblies { get; set; }

    public ServiceLifetime EventbusLifetime { get; set; } = ServiceLifetime.Scoped;

    public ServiceLifetime HandlerInstanceLifetime { get; set; } = ServiceLifetime.Scoped;
}
