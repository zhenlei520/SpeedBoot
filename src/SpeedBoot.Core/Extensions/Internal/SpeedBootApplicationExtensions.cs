// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

internal static class SpeedBootApplicationExtensions
{
    public static SpeedBootApplication AddCompletionAppStartup(
        this SpeedBootApplication application)
    {
        var serviceRegisterStartup = new CompletionAppStartup( application.Assemblies, new Lazy<ILogger?>(() => App.Instance.GetSingletonService<ILogger>(), LazyThreadSafetyMode.ExecutionAndPublication));
        application.Startups.Add(serviceRegisterStartup);
        return application;
    }

    public static SpeedBootApplication AddServiceRegisterStartup(
        this SpeedBootApplication application)
    {
        var serviceRegisterStartup = new ServiceRegisterStartup(
            application.Services,
            application.Assemblies,
            new Lazy<ILogger?>(() => App.Instance.GetSingletonService<ILogger>(), LazyThreadSafetyMode.ExecutionAndPublication));
        application.Startups.Add(serviceRegisterStartup);
        return application;
    }
}
