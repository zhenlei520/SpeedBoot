﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

/// <summary>
/// Object Storage Client
///
/// 对象存储客户端
/// </summary>
public interface IObjectStorageClient
{
    #region sync

    /// <summary>
    /// get file information
    ///
    /// 得到文件信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    ObjectInfoResponse GetObject(GetObjectInfoRequest request);

    /// <summary>
    /// get file information
    ///
    /// 得到文件信息（范围下载）
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    ObjectInfoResponse GetObject(GetObjectInfoChunkRequest request);

    /// <summary>
    /// upload files
    ///
    /// 上传文件
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    void Put(PutObjectStorageRequest request);

    /// <summary>
    /// determine whether the file exists
    ///
    /// 判断文件是否存在
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    bool Exists(ExistObjectStorageRequest request);

    /// <summary>
    /// delete Files
    ///
    /// 删除文件
    /// </summary>
    /// <param name="request">bucket name（桶名（空间名））</param>
    /// <returns></returns>
    void Delete(DeleteObjectStorageRequest request);

    /// <summary>
    /// batch delete file
    ///
    /// 批量删除文件
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    void BatchDelete(BatchDeleteObjectStorageRequest request);

    /// <summary>
    /// get credentials
    ///
    /// 获取凭证
    /// </summary>
    /// <returns></returns>
    CredentialsResponse GetCredentials();

    /// <summary>
    /// get credentials
    ///
    /// 获取凭证
    /// </summary>
    /// <returns></returns>
    string GetToken(CredentialsRequestBase credentialsRequest);

    #endregion

    #region async

    /// <summary>
    /// get file information
    ///
    /// 得到文件信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectInfoResponse> GetObjectAsync(GetObjectInfoRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// get file information（range download）
    ///
    /// 得到文件信息（范围下载）
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectInfoResponse> GetObjectAsync(GetObjectInfoChunkRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// upload files
    ///
    /// 上传文件
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PutAsync(PutObjectStorageRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// determine whether the file exists
    /// 判断文件是否存在
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(ExistObjectStorageRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// delete Files
    ///
    /// 删除文件
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(DeleteObjectStorageRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// batch delete file
    ///
    /// 批量删除文件
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task BatchDeleteAsync(BatchDeleteObjectStorageRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// get credentials
    ///
    /// 获取凭证
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<CredentialsResponse> GetCredentialsAsync(CancellationToken cancellationToken = default);

    Task<string> GetTokenAsync(CredentialsRequestBase credentialsRequest, CancellationToken cancellationToken = default);

    #endregion

}
