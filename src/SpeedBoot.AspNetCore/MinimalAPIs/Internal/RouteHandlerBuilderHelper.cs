// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER

using Microsoft.AspNetCore.Builder;

namespace SpeedBoot.AspNetCore;

internal static class RouteHandlerBuilderHelper
{
    public static List<Action<MethodInfo, RouteHandlerBuilder>> GetRouteHandlerBuilderActions(
        GlobalServiceRouteOptions globalServiceRouteOptions,
        ServiceRouteOptions serviceRouteOptions)
    {
        List<Action<MethodInfo, RouteHandlerBuilder>> routeHandlerBuilderActions = new();
        var routeHandlerBuilderAction = serviceRouteOptions.RouteHandlerBuilderAction ?? globalServiceRouteOptions.RouteHandlerBuilderAction;
        var whereIfRouteHandlerBuilderAction = serviceRouteOptions.WhereIfRouteHandlerBuilderAction ?? globalServiceRouteOptions.WhereIfRouteHandlerBuilderAction;
        if(routeHandlerBuilderAction != null)
        {
            routeHandlerBuilderActions.Add((_, routeHandlerBuilder) => routeHandlerBuilderAction.Invoke(routeHandlerBuilder));
        }
        if (whereIfRouteHandlerBuilderAction != null)
        {
            routeHandlerBuilderActions.Add((methodInfo, routeHandlerBuilder) =>
            {
                if (whereIfRouteHandlerBuilderAction.Value.Item1.Invoke(methodInfo))
                {
                    whereIfRouteHandlerBuilderAction.Value.Item2.Invoke(routeHandlerBuilder);
                }
            });
        }
        return routeHandlerBuilderActions;
    }
}

#endif
