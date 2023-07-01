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

    public ObjectInfoResponse GetObject(string objectName)
        => _objectStorageClient.GetObject(new GetObjectInfoRequest(BucketName, objectName));

    public void Put(string objectName, Stream data)
        => Put(objectName, data, null);

    public void Put(string objectName, Stream data, bool? enableOverwrite)
        => _objectStorageClient.Put(new PutObjectStorageRequest(BucketName, objectName, data, enableOverwrite));

    public bool Exists(string objectName)
        => _objectStorageClient.Exists(new ExistObjectStorageRequest(BucketName, objectName));

    public void Delete(string objectName)
        => _objectStorageClient.Delete(new DeleteObjectStorageRequest(BucketName, objectName));

    public void BatchDelete(IEnumerable<string> objectNames)
        => _objectStorageClient.BatchDelete(new BatchDeleteObjectStorageRequest(BucketName, objectNames.ToList()));

    public Task<ObjectInfoResponse> GetObjectAsync(string objectName, CancellationToken cancellationToken = default)
        => _objectStorageClient.GetObjectAsync(new GetObjectInfoRequest(BucketName, objectName), cancellationToken);

    public Task PutAsync(string objectName, Stream data, CancellationToken cancellationToken = default)
        => PutAsync(objectName, data, null, cancellationToken);

    public Task PutAsync(string objectName, Stream data, bool? enableOverwrite, CancellationToken cancellationToken = default)
        => _objectStorageClient.PutAsync(new PutObjectStorageRequest(BucketName, objectName, data, enableOverwrite), cancellationToken);

    public Task<bool> ExistsAsync(string objectName, CancellationToken cancellationToken = default)
        => _objectStorageClient.ExistsAsync(new ExistObjectStorageRequest(BucketName, objectName), cancellationToken);

    public Task DeleteAsync(string objectName, CancellationToken cancellationToken = default)
        => _objectStorageClient.DeleteAsync(new DeleteObjectStorageRequest(BucketName, objectName), cancellationToken);

    public Task BatchDeleteAsync(IEnumerable<string> objectNames, CancellationToken cancellationToken = default)
        => _objectStorageClient.BatchDeleteAsync(new BatchDeleteObjectStorageRequest(BucketName, objectNames.ToList()), cancellationToken);
}
