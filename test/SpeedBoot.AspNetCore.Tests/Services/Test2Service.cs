// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Tests.Services;

[Route("/api/[service]/[action]")]
[Route("/api2/[service]/[action]")]
[CustomAttribute]
public class Test2Service : TestServiceBase
{
    [Internal(1)]
    [RouteIgnore]
    public string GetPing()
    {
        return "Healthy";
    }

    [HttpGet("test21")]
    public string Test()
    {
        return "test2/test";
    }

    [HttpGet("/test22")]
    [HttpPost("/test22")]
    public string Test2()
    {
        return "test2/test2";
    }

    [HttpGet]
    [Route("/test23")]
    [Route("test23_copy")]
    public string Test3()
    {
        return "test2/test3";
    }
}
