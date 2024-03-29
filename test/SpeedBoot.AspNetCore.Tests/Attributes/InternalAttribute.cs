﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Tests.Attributes;

public class InternalAttribute : EndpointFilterBaseAttribute<InternalEndpointFilterProvider>
{
    public InternalAttribute() : base()
    {

    }

    public InternalAttribute(int order) : base(order)
    {

    }
}
