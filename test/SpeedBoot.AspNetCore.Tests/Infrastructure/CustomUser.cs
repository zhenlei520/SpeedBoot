// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Tests.Infrastructure;

public class CustomUser: IIdentityUser<int>
{
    public int Id { get; set; }

    public string Name { get; set; }
}
