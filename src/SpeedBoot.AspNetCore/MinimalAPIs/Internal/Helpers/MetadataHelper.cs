// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET7_0_OR_GREATER
namespace SpeedBoot.AspNetCore;

public static class MetadataHelper
{
    public static void CompletionMetadata(
        RouteHandlerBuilder routeHandlerBuilder,
        IEnumerable<IMetadataAttribute> endpointFilterAttributeWithMethod,
        IEnumerable<IMetadataAttribute> endpointFilterAttributeWithClass)
    {
        var customAttributes = endpointFilterAttributeWithClass
            .Where(attribute => !endpointFilterAttributeWithMethod.Any(a => a.GetType() == attribute.GetType())).ToList();
        foreach (var attribute in customAttributes)
        {
            routeHandlerBuilder.WithMetadata(attribute);
        }
    }
}
#endif
