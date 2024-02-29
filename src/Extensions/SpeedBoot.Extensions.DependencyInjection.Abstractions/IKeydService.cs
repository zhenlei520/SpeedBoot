// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.DependencyInjection;

public interface IKeydService<TService> where TService : IService
{
    TService? GetService(string key);

    TService GetRequiredService(string key);
}
