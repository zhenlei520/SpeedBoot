﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.IdGenerator.Normal.Components;

public class IdGeneratorServiceRegister : ServiceRegisterComponentBase
{
    public override void ConfigureServices(ConfigureServiceContext context)
    {
        var key = App.Instance.GetConfiguration(true)?.GetSection("SpeedBoot").GetSection("IdGenerator").GetSection("Key").Value;
        context.Services.AddNormalIdGenerator(key);
    }
}
