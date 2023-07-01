// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

public static class ObjectStorageClientExtensions
{
    #region download file（下载文件）

    #region async（同步）

    /// <summary>
    /// download file（The download method is automatically selected according to the file size. If the file size exceeds <paramref>GlobalObjectStorageConfig.BigFileLength</paramref>, download in chunks, otherwise use a small file to save）
    ///
    /// 下载文件（根据文件大小自动选择下载方式，文件大小超过 <paramref>GlobalObjectStorageConfig.BigFileLength</paramref> 使用分块下载，否则使用小文件保存）
    /// </summary>
    /// <param name="objectStorageClient"></param>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    /// <param name="chunkSize">chunk size（分块大小，当为 null 且使用分块下载时使用<paramref>GlobalObjectStorageConfig.BigFileLength</paramref>）</param>
    public static void DownloadFile(
        this IObjectStorageClient objectStorageClient,
        string bucketName,
        string objectName,
        string fileFullPath,
        bool enableOverwrite = false,
        int? chunkSize = null)
    {
        var stream = objectStorageClient.GetObject(bucketName, objectName, out var contentLength);
        if (contentLength > GlobalObjectStorageConfig.BigFileLength)
            stream.SaveToBigFile(fileFullPath, chunkSize ?? GlobalObjectStorageConfig.ChunkSize, enableOverwrite);
        else
            stream.SaveToSmallFile(fileFullPath, enableOverwrite);
    }

    /// <summary>
    /// download Big file
    ///
    /// 下载大文件
    /// </summary>
    /// <param name="objectStorageClient"></param>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    /// <param name="chunkSize">chunk size（分块大小，当为 null 且使用分块下载时使用<paramref>GlobalObjectStorageConfig.BigFileLength</paramref>）</param>
    public static void DownloadBigFile(
        this IObjectStorageClient objectStorageClient,
        string bucketName,
        string objectName,
        string fileFullPath,
        bool enableOverwrite = false,
        int? chunkSize = null)
    {
        var stream = objectStorageClient.GetObject(bucketName, objectName);
        stream.SaveToBigFile(fileFullPath, chunkSize ?? GlobalObjectStorageConfig.ChunkSize, enableOverwrite);
    }

    /// <summary>
    /// download Small file
    ///
    /// 下载小文件
    /// </summary>
    /// <param name="objectStorageClient"></param>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    public static void DownloadSmallFile(
        this IObjectStorageClient objectStorageClient,
        string bucketName,
        string objectName,
        string fileFullPath,
        bool enableOverwrite = false)
    {
        var stream = objectStorageClient.GetObject(bucketName, objectName);
        stream.SaveToSmallFile(fileFullPath, enableOverwrite);
    }

    #endregion

    #region async（异步）

    /// <summary>
    /// download file（The download method is automatically selected according to the file size. If the file size exceeds <paramref>GlobalObjectStorageConfig.BigFileLength</paramref>, download in chunks, otherwise use a small file to save）
    ///
    /// 下载文件（根据文件大小自动选择下载方式，文件大小超过 <paramref>GlobalObjectStorageConfig.BigFileLength</paramref> 使用分块下载，否则使用小文件保存）
    /// </summary>
    /// <param name="objectStorageClient"></param>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    /// <param name="chunkSize">chunk size（分块大小，当为 null 且使用分块下载时使用<paramref>GlobalObjectStorageConfig.BigFileLength</paramref>）</param>
    /// <param name="cancellationToken"></param>
    public static async Task DownloadFileAsync(
        this IObjectStorageClient objectStorageClient,
        string bucketName,
        string objectName,
        string fileFullPath,
        bool enableOverwrite = false,
        int? chunkSize = null,
        CancellationToken cancellationToken = default)
    {
        var stream = await objectStorageClient.GetObjectAsync(bucketName, objectName, out var contentLength, cancellationToken);
        if (contentLength > GlobalObjectStorageConfig.BigFileLength)
            await stream.SaveToBigFileAsync(fileFullPath, chunkSize ?? GlobalObjectStorageConfig.ChunkSize, enableOverwrite,
                cancellationToken);
        else
            await stream.SaveToSmallFileAsync(fileFullPath, enableOverwrite, cancellationToken);
    }

    /// <summary>
    /// download Big file
    ///
    /// 下载大文件
    /// </summary>
    /// <param name="objectStorageClient"></param>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    /// <param name="chunkSize">chunk size（分块大小，当为 null 且使用分块下载时使用<paramref>GlobalObjectStorageConfig.BigFileLength</paramref>）</param>
    /// <param name="cancellationToken"></param>
    public static Task DownloadBigFileAsync(
        this IObjectStorageClient objectStorageClient,
        string bucketName,
        string objectName,
        string fileFullPath,
        bool enableOverwrite = false,
        int? chunkSize = null,
        CancellationToken cancellationToken = default)
    {
        var stream = objectStorageClient.GetObject(bucketName, objectName);
        return stream.SaveToBigFileAsync(fileFullPath, chunkSize ?? GlobalObjectStorageConfig.ChunkSize, enableOverwrite,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// download Small file
    ///
    /// 下载小文件
    /// </summary>
    /// <param name="objectStorageClient"></param>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    /// <param name="cancellationToken"></param>
    public static async Task DownloadSmallFileAsync(
        this IObjectStorageClient objectStorageClient,
        string bucketName,
        string objectName,
        string fileFullPath,
        bool enableOverwrite = false,
        CancellationToken cancellationToken = default)
    {
        var stream = await objectStorageClient.GetObjectAsync(bucketName, objectName, cancellationToken);
        await stream.SaveToSmallFileAsync(fileFullPath, enableOverwrite, cancellationToken);
    }

    #endregion

    #endregion
}
