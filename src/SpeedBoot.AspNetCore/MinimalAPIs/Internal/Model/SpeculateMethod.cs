// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore;

internal class SpeculateMethod
{
    public string[]? HttpMethods { get; }

    public Func<string> ActionNameFunc { get; }

    public SpeculateMethod(string[]? httpMethods, Func<string> actionNameFunc)
    {
        HttpMethods = httpMethods;
        ActionNameFunc = actionNameFunc;
    }
}
