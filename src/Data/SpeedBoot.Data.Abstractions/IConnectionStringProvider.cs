﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.Abstractions;

public interface IConnectionStringProvider
{
    /// <summary>
    /// Get Database Connection Strings based on ConnectionName
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<string> GetConnectionStringAsync(string? name = null);

    /// <summary>
    /// Get Database Connection Strings based on ConnectionName
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    string GetConnectionString(string? name = null);
}
