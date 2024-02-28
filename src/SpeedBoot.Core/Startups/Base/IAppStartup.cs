// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public interface IAppStartup
{
    int Order { get; }

    string Name { get; }

    void Initialize();
}
