// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.Abstractions;

public class SaveChangesCompletedEventData : DbContextEventDataBase
{
    public int Result { get; set; }

    /// <summary>
    /// execution time
    /// ms
    /// </summary>
    public long? Duration { get; set; }
}
