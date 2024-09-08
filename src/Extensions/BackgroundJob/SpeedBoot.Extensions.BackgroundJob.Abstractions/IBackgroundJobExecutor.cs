// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.BackgroundJob.Abstractions;

public interface IBackgroundJobExecutor
{
    Task ExecuteAsync(JobContext context, CancellationToken cancellationToken = default);
}
