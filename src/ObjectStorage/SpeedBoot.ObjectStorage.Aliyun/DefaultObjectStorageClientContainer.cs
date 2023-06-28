// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.ObjectStorage.Aliyun;

public class DefaultObjectStorageClientContainer : IObjectStorageClientContainer
{
    private readonly IObjectStorageClient _objectStorageClient;
    private readonly IAliyunClientProvider _aliyunClientProvider;

    private string BucketName => _aliyunClientProvider.AliyunObjectStorageOptions.BucketName;

    public DefaultObjectStorageClientContainer(
        IObjectStorageClient objectStorageClient,
        IAliyunClientProvider aliyunClientProvider)
    {
        _objectStorageClient = objectStorageClient;
        _aliyunClientProvider = aliyunClientProvider;
    }

    public void GetObject(
        string objectName,
        Action<Stream> callback)
        => _objectStorageClient.GetObject(BucketName, objectName, callback);

    public void GetObject(
        string objectName,
        long offset,
        long length,
        Action<Stream> callback)
        => _objectStorageClient.GetObject(BucketName, objectName, offset, length, callback);

    public void Put(string objectName, Stream data)
        => _objectStorageClient.Put(BucketName, objectName, data);

    public bool Exists(string objectName)
        => _objectStorageClient.Exists(BucketName, objectName);

    public void Delete(string objectName)
        => _objectStorageClient.Delete(BucketName, objectName);

    public void DeleteRange(IEnumerable<string> objectNames)
        => _objectStorageClient.DeleteRange(BucketName, objectNames);

    public Task GetObjectAsync(
        string objectName,
        Action<Stream> callback,
        CancellationToken cancellationToken = default)
        => _objectStorageClient.GetObjectAsync(BucketName, objectName, callback, cancellationToken);

    public Task GetObjectAsync(
        string objectName,
        long offset,
        long length,
        Action<Stream> callback,
        CancellationToken cancellationToken = default)
        => _objectStorageClient.GetObjectAsync(BucketName, objectName, offset, length, callback, cancellationToken);

    public Task PutAsync(string objectName, Stream data, CancellationToken cancellationToken = default)
        => _objectStorageClient.PutAsync(BucketName, objectName, data, cancellationToken);

    public Task<bool> ExistsAsync(string objectName, CancellationToken cancellationToken = default)
        => _objectStorageClient.ExistsAsync(BucketName, objectName, cancellationToken);

    public Task DeleteAsync(string objectName, CancellationToken cancellationToken = default)
        => _objectStorageClient.DeleteAsync(BucketName, objectName, cancellationToken);

    public Task DeleteRangeAsync(IEnumerable<string> objectNames, CancellationToken cancellationToken = default)
        => _objectStorageClient.DeleteRangeAsync(BucketName, objectNames, cancellationToken);
}
