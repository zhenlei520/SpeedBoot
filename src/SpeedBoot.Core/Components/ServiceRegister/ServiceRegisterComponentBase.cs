// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public abstract class ServiceRegisterComponentBase : OrderBase, IServiceComponent
{
    public abstract void ConfigureServices(ConfigureServiceContext context);
}
