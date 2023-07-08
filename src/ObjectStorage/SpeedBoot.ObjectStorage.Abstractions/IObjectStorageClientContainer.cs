// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

/// <summary>
/// Object Storage Client Container
///
/// 对象存储客户端容器
/// </summary>
public interface ObjectStorageClientContainer
{
    #region sync

    /// <summary>
    /// 得到文件信息
    ///
    /// get file information
    /// </summary>
    /// <param name="objectName">file name（文件名）</param>
    /// <returns></returns>
    ObjectInfoResponse GetObject(string objectName);

    /// <summary>
    /// upload files
    /// 上传文件
    /// </summary>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="data">数据流</param>
    /// <returns></returns>
    void Put(
        string objectName,
        Stream data);

    /// <summary>
    /// upload files
    /// 上传文件
    /// </summary>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="data">数据流</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖）</param>
    /// <returns></returns>
    void Put(
        string objectName,
        Stream data,
        bool? enableOverwrite);

    /// <summary>
    /// determine whether the file exists
    /// 判断文件是否存在
    /// </summary>
    /// <param name="objectName">file name（文件名）</param>
    /// <returns></returns>
    bool Exists(string objectName);

    /// <summary>
    /// delete Files
    /// 删除文件
    /// </summary>
    /// <param name="objectName">file name（文件名）</param>
    /// <returns></returns>
    void Delete(string objectName);

    /// <summary>
    /// batch delete file
    /// 批量删除文件
    /// </summary>
    /// <param name="objectNames">A collection of filenames to be deleted（待删除的文件集合）</param>
    /// <returns></returns>
    void BatchDelete(IEnumerable<string> objectNames);

    #endregion

    #region async

    /// <summary>
    /// 得到文件信息
    ///
    /// get file information
    /// </summary>
    /// <param name="objectName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectInfoResponse> GetObjectAsync(
        string objectName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// upload files
    /// 上传文件
    /// </summary>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="data">数据流</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PutAsync(
        string objectName,
        Stream data,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// upload files
    /// 上传文件
    /// </summary>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="data">数据流</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖）</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PutAsync(
        string objectName,
        Stream data,
        bool? enableOverwrite,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// determine whether the file exists
    /// 判断文件是否存在
    /// </summary>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(string objectName, CancellationToken cancellationToken = default);

    /// <summary>
    /// delete Files
    /// 删除文件
    /// </summary>
    /// <param name="objectName">file name（文件名）</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(
        string objectName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// batch delete file
    /// 批量删除文件
    /// </summary>
    /// <param name="objectNames">A collection of filenames to be deleted（待删除的文件集合）</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task BatchDeleteAsync(
        IEnumerable<string> objectNames,
        CancellationToken cancellationToken = default);

    #endregion
}
