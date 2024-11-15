// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.IdGenerator.Snowflake;

public static class SnowflakeIdGeneratorConfig
{
    /// <summary>
    /// Default working cluster id key
    /// </summary>
    public static string DefaultWorkerIdKey { get; set; } = "WORKER_ID";

    public static string DefaultKey = nameof(SnowflakeIdGenerator);
}
