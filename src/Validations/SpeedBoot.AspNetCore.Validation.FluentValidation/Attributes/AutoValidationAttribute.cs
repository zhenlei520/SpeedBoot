// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Validation;

/// <summary>
/// Only supports minimalAPI
/// </summary>
public class AutoValidationAttribute : EndpointFilterBaseAttribute<AutoValidationEndpointFilterProvider>
{
    public AutoValidationAttribute(int order = 999) : base(order)
    {
    }
}
