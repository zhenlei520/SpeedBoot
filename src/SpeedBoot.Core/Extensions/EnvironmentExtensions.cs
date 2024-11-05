// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot;

public static class EnvironmentExtensions
{
    public static bool IsDevelopment(this IEnvironment environment)
    {
        return environment.IsEnvironment("Development");
    }

    public static bool IsProduction(this IEnvironment environment)
    {
        return environment.IsEnvironment("Production");
    }

    public static bool IsStaging(this IEnvironment environment)
    {
        return environment.IsEnvironment("Staging");
    }

    public static bool IsEnvironment(this IEnvironment environment, string environmentName)
    {
        return string.Equals(environment.EnvironmentName, environmentName, StringComparison.OrdinalIgnoreCase);
    }
}
