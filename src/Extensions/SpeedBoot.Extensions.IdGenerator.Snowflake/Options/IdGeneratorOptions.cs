// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.IdGenerator.Snowflake.Options;

public class IdGeneratorOptions
{
    public long? WorkerId { get; set; }

    /// <summary>
    /// The number of digits the sequence occupies in the id
    /// default: 10
    /// </summary>
    public int SequenceBits { get; set; } = 12;

    /// <summary>
    /// The number of digits occupied by the working machines in the id
    /// </summary>
    public int WorkerIdBits { get; set; } = 10;

    /// <summary>
    /// Maximum supported worker machine id
    /// </summary>
    public long MaxWorkerId => ~(-1L << WorkerIdBits);
}
