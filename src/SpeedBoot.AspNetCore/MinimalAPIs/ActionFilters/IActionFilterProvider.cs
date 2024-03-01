// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET7_0_OR_GREATER
namespace SpeedBoot.AspNetCore;

public interface IActionFilterProvider
{
    int Order { get; }

    public ValueTask<object?> HandlerAsync(EndpointFilterInvocationContext invocationContext, EndpointFilterDelegate next);
}
#endif
