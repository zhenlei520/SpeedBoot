// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ActionNameAttribute : Attribute
{
    public string Name { get; set; }

    public ActionNameAttribute(string name)
    {
        Name = name;
    }
}
