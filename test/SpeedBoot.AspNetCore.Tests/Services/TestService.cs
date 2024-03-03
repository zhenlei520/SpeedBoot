// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Tests.Services;

[Internal]
public class TestService : TestServiceBase
{
    public TestService()
    {
        RouteOptions.AddWhereIfRouteHandlerBuilderAction((method => method.Name == "GetPing", builder =>
        {
            builder.WithDescription("ping");
        }));
    }

    [Internal(1)]
    public string GetPing()
    {
        return "Healthy";
    }

    [HttpGet]
    public string Test()
    {
        return "test";
    }
}
