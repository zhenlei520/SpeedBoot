// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.IdGenerator.Snowflake;

public class DefaultWorkerProvider : IWorkerProvider
{
    private readonly long _workerId;

    public DefaultWorkerProvider(long workerId)
    {
        _workerId = workerId;
    }

    public Task<long> GetWorkerIdAsync()
    {
        var workerIdStr = Environment.GetEnvironmentVariable(SnowflakeIdGeneratorConfig.DefaultWorkerIdKey);
        return Task.FromResult(!workerIdStr.IsNullOrWhiteSpace() ? workerIdStr.ToLong(_workerId) : _workerId);
    }
}
