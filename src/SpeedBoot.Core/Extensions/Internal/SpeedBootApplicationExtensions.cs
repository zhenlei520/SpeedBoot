// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

internal static class SpeedBootApplicationExtensions
{
    public static SpeedBootApplication AddServiceRegisterComponents(
        this SpeedBootApplication application,
        Assembly[]? assemblies)
    {
        var serviceRegisterStartup = new ServiceRegisterStartup(application.Services, assemblies ?? GlobalConfig.DefaultAssemblies, null);
        application.Startups.Add(serviceRegisterStartup);
        return application;
    }
}
