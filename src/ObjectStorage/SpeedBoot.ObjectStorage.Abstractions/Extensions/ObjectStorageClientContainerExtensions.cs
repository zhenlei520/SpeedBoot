// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

public static class ObjectStorageClientContainerExtensions
{
    #region download file（下载文件）

    #region async（同步）

    /// <summary>
    /// download file（The download method is automatically selected according to the file size. If the file size exceeds <paramref>GlobalObjectStorageConfig.BigFileLength</paramref>, download in chunks, otherwise use a small file to save）
    ///
    /// 下载文件（根据文件大小自动选择下载方式，文件大小超过 <paramref>GlobalObjectStorageConfig.BigFileLength</paramref> 使用分块下载，否则使用小文件保存）
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖，当为 null 且使用分块下载时使用<paramref>GlobalObjectStorageConfig.EnableOverwrite</paramref></param>
    /// <param name="chunkSize">chunk size（分块大小，当为 null 且使用分块下载时使用<paramref>GlobalObjectStorageConfig.BigFileLength</paramref>）</param>
    public static void DownloadFile(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName,
        string fileFullPath,
        bool? enableOverwrite = null,
        int? chunkSize = null)
    {
        var actualEnableOverwrite = enableOverwrite ?? GlobalObjectStorageConfig.EnableOverwrite;
        ObjectStorageUtils.CheckFileExist(fileFullPath, actualEnableOverwrite);

        var objectInfoResponse = objectStorageClientContainer.GetObject(objectName);
        if (ObjectStorageUtils.IsBigFile(objectInfoResponse.ContentLength))
            objectInfoResponse.Stream.SaveToBigFile(fileFullPath, chunkSize ?? GlobalObjectStorageConfig.ChunkSize, actualEnableOverwrite);
        else
            objectInfoResponse.Stream.SaveToSmallFile(fileFullPath, actualEnableOverwrite);
    }

    /// <summary>
    /// download Big file
    ///
    /// 下载大文件
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    /// <param name="chunkSize">chunk size（分块大小，当为 null 且使用分块下载时使用<paramref>GlobalObjectStorageConfig.BigFileLength</paramref>）</param>
    public static void DownloadBigFile(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName,
        string fileFullPath,
        bool? enableOverwrite = null,
        int? chunkSize = null)
    {
        var actualEnableOverwrite = enableOverwrite ?? GlobalObjectStorageConfig.EnableOverwrite;
        ObjectStorageUtils.CheckFileExist(fileFullPath, actualEnableOverwrite);

        var objectInfoResponse = objectStorageClientContainer.GetObject(objectName);
        objectInfoResponse.Stream.SaveToBigFile(fileFullPath, chunkSize ?? GlobalObjectStorageConfig.ChunkSize, actualEnableOverwrite);
    }

    /// <summary>
    /// download Small file
    ///
    /// 下载小文件
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    public static void DownloadSmallFile(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName,
        string fileFullPath,
        bool enableOverwrite = true)
    {
        ObjectStorageUtils.CheckFileExist(fileFullPath, enableOverwrite);

        var objectInfoResponse = objectStorageClientContainer.GetObject(objectName);
        objectInfoResponse.Stream.SaveToSmallFile(fileFullPath, enableOverwrite);
    }

    #endregion

    #region async（异步）

    /// <summary>
    /// download file（The download method is automatically selected according to the file size. If the file size exceeds <paramref>GlobalObjectStorageConfig.BigFileLength</paramref>, download in chunks, otherwise use a small file to save）
    ///
    /// 下载文件（根据文件大小自动选择下载方式，文件大小超过 <paramref>GlobalObjectStorageConfig.BigFileLength</paramref> 使用分块下载，否则使用小文件保存）
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    /// <param name="chunkSize">chunk size（分块大小，当为 null 且使用分块下载时使用<paramref>GlobalObjectStorageConfig.BigFileLength</paramref>）</param>
    /// <param name="cancellationToken"></param>
    public static async Task DownloadFileAsync(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName,
        string fileFullPath,
        bool? enableOverwrite = null,
        int? chunkSize = null,
        CancellationToken cancellationToken = default)
    {
        var actualEnableOverwrite = enableOverwrite ?? GlobalObjectStorageConfig.EnableOverwrite;
        ObjectStorageUtils.CheckFileExist(fileFullPath, actualEnableOverwrite);

        var objectInfoResponse = await objectStorageClientContainer.GetObjectAsync(objectName, cancellationToken);
        if (ObjectStorageUtils.IsBigFile(objectInfoResponse.ContentLength))
            await objectInfoResponse.Stream.SaveToBigFileAsync(fileFullPath, chunkSize ?? GlobalObjectStorageConfig.ChunkSize,
                actualEnableOverwrite,
                cancellationToken);
        else
            await objectInfoResponse.Stream.SaveToSmallFileAsync(fileFullPath, actualEnableOverwrite, cancellationToken);
    }

    /// <summary>
    /// download Big file
    ///
    /// 下载大文件
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: true</param>
    /// <param name="chunkSize">chunk size（分块大小，当为 null 且使用分块下载时使用<paramref>GlobalObjectStorageConfig.BigFileLength</paramref>）</param>
    /// <param name="cancellationToken"></param>
    public static async Task DownloadBigFileAsync(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName,
        string fileFullPath,
        bool? enableOverwrite = null,
        int? chunkSize = null,
        CancellationToken cancellationToken = default)
    {
        var actualEnableOverwrite = enableOverwrite ?? GlobalObjectStorageConfig.EnableOverwrite;
        ObjectStorageUtils.CheckFileExist(fileFullPath, actualEnableOverwrite);

        var objectInfoResponse = await objectStorageClientContainer.GetObjectAsync(objectName, cancellationToken);
        await objectInfoResponse.Stream.SaveToBigFileAsync(
            fileFullPath,
            chunkSize ?? GlobalObjectStorageConfig.ChunkSize,
            actualEnableOverwrite,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// download Small file
    ///
    /// 下载小文件
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: true</param>
    /// <param name="cancellationToken"></param>
    public static async Task DownloadSmallFileAsync(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName,
        string fileFullPath,
        bool? enableOverwrite = null,
        CancellationToken cancellationToken = default)
    {
        var actualEnableOverwrite = enableOverwrite ?? GlobalObjectStorageConfig.EnableOverwrite;
        ObjectStorageUtils.CheckFileExist(fileFullPath, actualEnableOverwrite);

        var objectInfoResponse = await objectStorageClientContainer.GetObjectAsync(objectName, cancellationToken);
        await objectInfoResponse.Stream.SaveToSmallFileAsync(fileFullPath, actualEnableOverwrite, cancellationToken);
    }

    #endregion

    #endregion

    #region upload file（上传文件）

    #region sync

    /// <summary>
    /// upload file（上传文件）
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖）</param>
    public static void UploadFile(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName,
        string fileFullPath,
        bool? enableOverwrite = null)
    {
        var contentLength = FileUtils.GetFileContentLength(fileFullPath);
        using var stream = ObjectStorageUtils.IsBigFile(contentLength) ?
            FileUtils.GetBigFileStream(fileFullPath) :
            FileUtils.GetSmallFileStream(fileFullPath);
        objectStorageClientContainer.Put(objectName, stream, enableOverwrite);
    }

    /// <summary>
    /// upload small file（上传小文件）
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖）</param>
    public static void UploadSmallFile(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName,
        string fileFullPath,
        bool? enableOverwrite = null)
    {
        using var stream = FileUtils.GetSmallFileStream(fileFullPath);
        objectStorageClientContainer.Put(objectName, stream, enableOverwrite);
    }

    /// <summary>
    /// upload big file（上传大文件）
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖）</param>
    public static void UploadBigFile(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName,
        string fileFullPath,
        bool? enableOverwrite = null)
    {
        using var stream = FileUtils.GetBigFileStream(fileFullPath);
        objectStorageClientContainer.Put(objectName, stream, enableOverwrite);
    }

    #endregion

    #region async

    /// <summary>
    /// upload file（上传文件）
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖）</param>
    /// <param name="cancellationToken"></param>
    public static Task UploadFileAsync(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName,
        string fileFullPath,
        bool? enableOverwrite = null,
        CancellationToken cancellationToken = default)
    {
        var contentLength = FileUtils.GetFileContentLength(fileFullPath);
        using var stream = ObjectStorageUtils.IsBigFile(contentLength) ?
            FileUtils.GetBigFileStream(fileFullPath) :
            FileUtils.GetSmallFileStream(fileFullPath);
        return objectStorageClientContainer.PutAsync(objectName, stream, enableOverwrite, cancellationToken);
    }

    /// <summary>
    /// upload small file（上传小文件）
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖）</param>
    /// <param name="cancellationToken"></param>
    public static Task UploadSmallFileAsync(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName,
        string fileFullPath,
        bool? enableOverwrite = null,
        CancellationToken cancellationToken = default)
    {
        using var stream = FileUtils.GetSmallFileStream(fileFullPath);
        return objectStorageClientContainer.PutAsync(objectName, stream, enableOverwrite, cancellationToken);
    }

    /// <summary>
    /// upload big file（上传大文件）
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="fileFullPath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖）</param>
    /// <param name="cancellationToken"></param>
    public static Task UploadBigFileAsync(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName,
        string fileFullPath,
        bool? enableOverwrite = null,
        CancellationToken cancellationToken = default)
    {
        using var stream = FileUtils.GetBigFileStream(fileFullPath);
        return objectStorageClientContainer.PutAsync(objectName, stream, enableOverwrite, cancellationToken);
    }

    #endregion

    private static PutObjectStorageRequest GetPutObjectStorageRequest(
        string bucketName,
        string objectName,
        Stream stream,
        bool? enableOverwrite)
        => new(bucketName, objectName, stream, enableOverwrite);

    #endregion

    #region exist （文件是否存在）

    #region sync（同步）

    /// <summary>
    /// Does the file exist
    ///
    /// 文件是否存在
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <returns></returns>
    public static bool Exists(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName)
        => objectStorageClientContainer.Exists(objectName);

    #endregion

    #region async（异步）

    /// <summary>
    /// Does the file exist
    ///
    /// 文件是否存在
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <returns></returns>
    public static Task<bool> ExistsAsync(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName)
        => objectStorageClientContainer.ExistsAsync(objectName);

    #endregion

    #endregion

    #region delete （删除文件）

    #region sync（同步）

    /// <summary>
    /// delete file
    ///
    /// 删除文件
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <returns></returns>
    public static void Delete(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName)
        => objectStorageClientContainer.Delete(objectName);

    /// <summary>
    /// Batch delete files
    ///
    /// 批量删除文件
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectNames">collection of files to be deleted（待删除的文件集合）</param>
    /// <returns></returns>
    public static void BatchDelete(
        this IObjectStorageClientContainer objectStorageClientContainer,
        params string[] objectNames)
        => objectStorageClientContainer.BatchDelete(objectNames);

    #endregion

    #region async（异步）

    /// <summary>
    /// Does the file exist
    ///
    /// 文件是否存在
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectName">file name（文件名）</param>
    /// <returns></returns>
    public static Task DeleteAsync(
        this IObjectStorageClientContainer objectStorageClientContainer,
        string objectName)
        => objectStorageClientContainer.DeleteAsync(objectName);

    /// <summary>
    /// Batch delete files
    ///
    /// 批量删除文件
    /// </summary>
    /// <param name="objectStorageClientContainer"></param>
    /// <param name="objectNames">collection of files to be deleted（待删除的文件集合）</param>
    /// <returns></returns>
    public static Task BatchDeleteAsync(
        this IObjectStorageClientContainer objectStorageClientContainer,
        params string[] objectNames)
        => objectStorageClientContainer.BatchDeleteAsync(objectNames);

    #endregion

    #endregion

}
