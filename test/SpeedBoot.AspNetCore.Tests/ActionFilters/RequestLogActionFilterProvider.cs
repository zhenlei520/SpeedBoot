﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Tests.ActionFilters;

public class RequestLogActionFilterProvider: ActionFilterProviderBase
{
    public override ValueTask<object?> HandlerAsync(EndpointFilterInvocationContext invocationContext, EndpointFilterDelegate next)
    {
        return next(invocationContext);
    }
}
