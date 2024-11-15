// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.IdGenerator.Snowflake;

public class SnowflakeIdBuilder
{
    public IdGeneratorOptions? Options { get; set; }

    internal void Initialize()
    {
        var defaultWorkerId = 1;
        if (Options == null)
        {
            Options = new IdGeneratorOptions()
            {
                WorkerId = defaultWorkerId
            };
        }
        else
        {
            Options.WorkerId ??= defaultWorkerId;
        }
    }
}
