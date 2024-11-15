// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.IdGenerator.Snowflake;

public class SnowflakeIdGenerator : IIdGenerator<long>
{
    private readonly IWorkerProvider _workerProvider;

    public string Key { get; } = SnowflakeIdGeneratorConfig.DefaultKey;

    public SnowflakeIdGenerator(IWorkerProvider workerProvider)
    {
        _workerProvider = workerProvider;
    }

    public long Create()
    {
        //todo: await
        return 0;
    }
}
