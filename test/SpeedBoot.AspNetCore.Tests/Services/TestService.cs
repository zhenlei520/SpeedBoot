﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Tests.Services;

[Internal]
public class TestService : TestServiceBase
{
    [Internal(1)]
    [RoutePattern(HttpMethod = "Get")]
    public string Ping()
    {
        return "Healthy";
    }
}
