// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public static class SpeedBootApplicationExtensions
{
    internal static SpeedBootApplication AddServiceRegisterComponents(
        this SpeedBootApplication application,
        params Assembly[] assemblies)
    {
        var serviceRegisterStartup = new ServiceRegisterStartup(application.Services, assemblies, null);
        application.Startups.Add(serviceRegisterStartup);
        return application;
    }
}
