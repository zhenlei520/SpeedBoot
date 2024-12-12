// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql.DataFiltering.Internal;

internal sealed class NullDisposable : IDisposable
{
    public static NullDisposable Instance { get; } = new();

    /// <summary>
    /// no need to release resources
    /// </summary>
    public void Dispose()
    {
    }
}
