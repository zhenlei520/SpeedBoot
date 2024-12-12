// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if  NET7_0_OR_GREATER

namespace SpeedBoot.AspNetCore;

internal static class EndpointFilterHelper
{
    public static void RegisterEndpointFilter(
        RouteHandlerBuilder routeHandlerBuilder,
        IEnumerable<EndpointFilterBaseAttribute> endpointFilterAttributeWithMethod,
        IEnumerable<EndpointFilterBaseAttribute> endpointFilterAttributeWithClass)
    {
        var tempEndpointFilterAttributes = endpointFilterAttributeWithClass
            .Where(attribute => !endpointFilterAttributeWithMethod.Any(a => a.GetType() == attribute.GetType()))
            .ToList();

        var allEndpointFilterAttributes = endpointFilterAttributeWithMethod.Union(tempEndpointFilterAttributes).OrderBy(attribute => attribute.Order).ToList();
        foreach (var customFilterAttribute in allEndpointFilterAttributes)
        {
            routeHandlerBuilder.AddEndpointFilter((invocationContext, next) =>
            {
                var actionFilterProvider = invocationContext.HttpContext.RequestServices.GetService(customFilterAttribute.ServiceType) as IEndpointFilterProvider;
                SpeedArgumentException.ThrowIfNull(actionFilterProvider);
                return actionFilterProvider.HandlerAsync(invocationContext, next);
            });
        }
    }
}

#endif
