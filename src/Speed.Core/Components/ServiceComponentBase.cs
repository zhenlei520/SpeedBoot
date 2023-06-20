﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed;

public abstract class ServiceComponentBase : IServiceComponent
{
    public abstract void ConfigureServices(IServiceCollection services);
}