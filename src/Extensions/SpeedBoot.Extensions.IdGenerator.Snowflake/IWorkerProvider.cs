// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.IdGenerator.Snowflake;

public interface IWorkerProvider
{
    /// <summary>
    /// Working machine id
    /// </summary>
    Task<long> GetWorkerIdAsync();
}
