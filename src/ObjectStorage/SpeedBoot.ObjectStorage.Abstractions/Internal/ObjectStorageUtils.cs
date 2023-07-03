// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

/// <summary>
/// Object Storage Utils
/// 对象存储工具类
/// </summary>
internal static class ObjectStorageUtils
{
    /// <summary>
    /// is big file
    /// 是大文件
    /// </summary>
    /// <param name="contentLength">content length（内容大小）</param>
    /// <returns></returns>
    public static bool IsBigFile(long contentLength) => contentLength > GlobalObjectStorageConfig.BigFileLength;

    /// <summary>
    /// Check whether the file exists, and if it exists and allow overwriting is not enabled, an exception is thrown
    ///
    /// 检查文件是否存在，如果存在且未开启允许覆盖，则抛出异常
    /// </summary>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    /// <exception cref="SpeedFriendlyException"></exception>
    public static void CheckFileExist(string fileFullPath, bool enableOverwrite = false)
    {
        if (File.Exists(fileFullPath) && !enableOverwrite)
            throw new SpeedFriendlyException("The file already exists");
    }
}
