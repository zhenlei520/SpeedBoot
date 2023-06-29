// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

public static class ObjectStorageClientExtensions
{
    /// <summary>
    /// download file
    ///
    /// 下载文件
    /// </summary>
    /// <param name="objectStorageClient"></param>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="filePath">full file path（完整文件地址）</param>
    public static void DownloadFile(this IObjectStorageClient objectStorageClient, string bucketName, string objectName, string filePath)
    {
        var stream = objectStorageClient.GetObject(bucketName, objectName, out var contentLength);
        if (contentLength > GlobalObjectStorageConfig.BigFileLength)
            stream.SaveToBigFile(filePath);
        else
            stream.SaveToFile(filePath);
    }
}
