// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Tests.Services;

public class TestService : ServiceBase
{
    [RoutePattern(HttpMethod = "Get")]
    public string Ping()
    {
        return "Healthy";
    }
}
