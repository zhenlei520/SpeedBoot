// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.System;

/// <summary>
/// parameter check
///
/// 参数检查
/// </summary>
internal static class ParameterCheck
{
    /// <summary>
    /// Check Chunk Size
    ///
    /// 检查块大小
    /// </summary>
    /// <param name="chunkSize">default: 4096 bytes（4k）</param>
    internal static void CheckChunkSize(int chunkSize = 4096)
    {
#if NETCOREAPP3_0_OR_GREATER
        SpeedArgumentException.ThrowIfLessThanOrEqual(chunkSize, 0);
#else
        SpeedArgumentException.ThrowIfLessThanOrEqual(chunkSize, 0, nameof(chunkSize));
#endif
    }
}
