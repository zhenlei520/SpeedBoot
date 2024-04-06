// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Validation.FluentValidation.Components;

public class FluentValidationServiceRegister: ServiceRegisterComponentBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddFluentAutoValidation();
    }
}
