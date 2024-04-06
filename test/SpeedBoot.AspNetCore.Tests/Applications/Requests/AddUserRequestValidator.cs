// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Tests.Applications.Requests;

public class AddUserRequestValidator: AbstractValidator<AddUserRequest>
{
    public AddUserRequestValidator()
    {
        RuleFor(r => r.Name).NotNull().NotEmpty().MaximumLength(20);
    }
}
