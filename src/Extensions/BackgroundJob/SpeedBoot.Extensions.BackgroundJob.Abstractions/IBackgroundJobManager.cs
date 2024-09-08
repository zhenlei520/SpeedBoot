// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.BackgroundJob.Abstractions;

public interface IBackgroundJobManager
{
    /// <summary>
    /// Execute only one time
    /// </summary>
    /// <param name="args"></param>
    /// <param name="delay"></param>
    /// <typeparam name="TArgs"></typeparam>
    /// <returns></returns>
    Task<string> EnqueueAsync<TArgs>(TArgs args, TimeSpan? delay = null);
}
