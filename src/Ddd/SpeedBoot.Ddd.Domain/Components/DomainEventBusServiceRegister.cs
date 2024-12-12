// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Aliyun;

/// <summary>
/// aliyun Object Storage Registrar
/// </summary>
public class DomainEventBusServiceRegister : ServiceRegisterComponentBase
{
    public override void ConfigureServices(ConfigureServiceContext context)
        => context.Services.AddDomainEventBus();
}
