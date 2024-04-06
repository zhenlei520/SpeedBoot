// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Tests.Services;

public class ValidationService :ServiceBase
{
    public ValidationService()
    {
    }

    /// <summary>
    /// /api/v1/validations/user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AutoValidation]
    public IResult AddUser(AddUserRequest request)
    {
        // 到这里的时候，已经通过了参数验证
        return Results.Ok();
    }
}
