// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace SpeedBoot.AspNetCore.Tests.Controllers;

public class UserController : ControllerBase
{
    public UserController()
    {
    }

    [Microsoft.AspNetCore.Mvc.Route("User")]
    [Microsoft.AspNetCore.Mvc.HttpGet]
    public string GetAsync()
    {
        return "Hello User";
    }
}

public class User2Controller : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public User2Controller(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var userController = _serviceProvider.GetService<UserController>();
    }

    [Microsoft.AspNetCore.Mvc.HttpGet]
    [Microsoft.AspNetCore.Mvc.Route("User2")]
    public string GetAsync()
    {
        return "Hello User2";
    }
}
