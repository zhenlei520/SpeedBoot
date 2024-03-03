// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER
using Microsoft.AspNetCore.Builder;

namespace SpeedBoot.AspNetCore;

public class ServiceRouteOptions
{
    public bool? DisablePluralizeServiceName { get; set; }

    public bool? DisableAutoMapRoute { get; set; }

    public List<string>? GetPrefixes { get; set; }

    public List<string>? PostPrefixes { get; set; }

    public List<string>? PutPrefixes { get; set; }

    public List<string>? DeletePrefixes { get; set; }

    public bool? DisableTrimMethodPrefix { get; set; }

    public bool? DisableTrimMethodSuffix { get; set; }

    public bool? DisableAutoAppendId { get; set; }

    public Action<RouteHandlerBuilder>? RouteHandlerBuilderAction { get; set; }

    public (Func<MethodInfo, bool>, Action<RouteHandlerBuilder>)? WhereIfRouteHandlerBuilderAction { get; set; }
}
#endif
