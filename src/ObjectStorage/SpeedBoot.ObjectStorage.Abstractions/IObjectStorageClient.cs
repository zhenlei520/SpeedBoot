// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

public interface IObjectStorageClient
{
    #region sync

    /// <summary>
    /// upload files
    /// 上传文件
    /// </summary>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="data">数据流</param>
    /// <returns></returns>
    void Put(
        string bucketName,
        string objectName,
        Stream data);

    /// <summary>
    /// determine whether the file exists
    /// 判断文件是否存在
    /// </summary>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectName">file name（文件名）</param>
    /// <returns></returns>
    bool Exists(
        string bucketName,
        string objectName);

    /// <summary>
    /// delete Files
    /// 删除文件
    /// </summary>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectName">file name（文件名）</param>
    /// <returns></returns>
    void Delete(
        string bucketName,
        string objectName);

    /// <summary>
    /// batch delete file
    /// 批量删除文件
    /// </summary>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectNames">A collection of filenames to be deleted（待删除的文件集合）</param>
    /// <returns></returns>
    void DeleteRange(
        string bucketName,
        IEnumerable<string> objectNames);

    #endregion

    #region async

    /// <summary>
    /// upload files
    /// 上传文件
    /// </summary>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="data">数据流</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PutAsync(
        string bucketName,
        string objectName,
        Stream data,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// determine whether the file exists
    /// 判断文件是否存在
    /// </summary>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(
        string bucketName,
        string objectName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// delete Files
    /// 删除文件
    /// </summary>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(
        string bucketName,
        string objectName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// batch delete file
    /// 批量删除文件
    /// </summary>
    /// <param name="bucketName">bucket name（空间名称）</param>
    /// <param name="objectNames">A collection of filenames to be deleted（待删除的文件集合）</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteRangeAsync(
        string bucketName,
        IEnumerable<string> objectNames,
        CancellationToken cancellationToken = default);

    #endregion

}
