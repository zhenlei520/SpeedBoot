// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot;

internal class DefaultEnvironment : IEnvironment
{
    public string EnvironmentName { get; }

    public DefaultEnvironment(string environmentName)
    {
        EnvironmentName = environmentName;
    }
}
