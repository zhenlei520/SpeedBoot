// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local.Tests.Events;

public class RegisterUserEvent: Event
{
    public string Name { get; set; }

    public int Age { get; set; }
}
