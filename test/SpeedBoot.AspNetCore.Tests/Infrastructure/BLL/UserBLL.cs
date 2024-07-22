// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Tests.Infrastructure.BLL;

public class UserBLL: ISingletonDependency
{
    public string GetUserName()
    {
        return "zhenlei520";
    }
}
