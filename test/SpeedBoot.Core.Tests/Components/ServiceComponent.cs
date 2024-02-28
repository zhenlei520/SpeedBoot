// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Core.Tests.Components;

public class ServiceComponent: ServiceRegisterComponentBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<AppOptions>();
    }
}
