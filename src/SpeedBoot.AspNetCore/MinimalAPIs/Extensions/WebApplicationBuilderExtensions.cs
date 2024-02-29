// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER
using Microsoft.AspNetCore.Builder;
using SpeedBoot.AspNetCore.Options;

namespace SpeedBoot.AspNetCore;

public static class WebApplicationBuilderExtensions
{
    public static WebApplication AddMinimalAPIs(
        this WebApplicationBuilder builder,
        Assembly[]? assemblies = null)
    {
        return builder.AddMinimalAPIs(options =>
        {
            options.AdditionalAssemblies = assemblies;
        });
    }

    public static WebApplication AddMinimalAPIs(
        this WebApplicationBuilder builder,
        Action<GlobalServiceRouteOptions> action)
    {
        return builder.Services.AddMinimalAPIs(builder, action);
    }
}
#endif
