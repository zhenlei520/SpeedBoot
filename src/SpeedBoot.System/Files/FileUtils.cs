// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.System;

/// <summary>
/// 文件帮助类
/// </summary>
public static class FileUtils
{
    #region get file stream（得到文件流）

    /// <summary>
    /// get file stream（small file）
    ///
    /// 得到文件流（小文件）
    /// </summary>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <returns></returns>
    public static Stream GetSmallFileStream(string fileFullPath)
    {
        var fileStream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read);
        return fileStream;
    }

    /// <summary>
    /// get file stream（big file）
    ///
    /// 得到文件流（大文件）
    /// </summary>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="chunkSize">chunk size（分块大小，当为 null 且使用分块下载时使用）</param>
    /// <returns></returns>
    public static Stream GetBigFileStream(string fileFullPath, int chunkSize = 4096)
    {
        ParameterCheck.CheckChunkSize(chunkSize);
        var fileStream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read, chunkSize);
        return fileStream;
    }

    #endregion

    #region 得到文件大小

    /// <summary>
    /// get file content-length（unit：bytes。1 KB -> 1024 bytes）
    /// only local files are supported
    ///
    /// 得到文件大小（单位：字节。1 KB -> 1024 bytes）
    /// 仅支持本地文件
    /// </summary>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <returns></returns>
    public static long GetFileContentLength(string fileFullPath)
    {
        var fileInfo = new FileInfo(fileFullPath);
        return fileInfo.Length;
    }

    #endregion

}
