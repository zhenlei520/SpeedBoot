﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

/// <summary>
/// Global Object Storage Configuration
///
/// 全局对象存储配置
/// </summary>
public class GlobalObjectStorageConfig
{
    /// <summary>
    /// large file length
    /// unit: Byte
    /// default: 10M
    ///
    /// 大文件长度
    /// 单位：字节
    /// 默认：10M
    /// </summary>
    public static long BigFileLength = 10 * (long)Math.Pow(1024, 2);

    /// <summary>
    /// enable file overwrite
    ///
    /// 启用文件覆盖
    /// </summary>
    public static bool EnableOverwrite = true;

    /// <summary>
    /// default: 4096 bytes（4k）
    ///
    /// 块大小：默认 4k
    /// </summary>
    public static int ChunkSize = 1024 * 4;
}
