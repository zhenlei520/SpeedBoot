﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.BackgroundJob.Abstractions;

/// <summary>
/// Background Task Handler
/// </summary>
/// <typeparam name="TArgs"></typeparam>
public interface IBackgroundJob<in TArgs>
{
    Task ExecuteAsync(TArgs args);
}
