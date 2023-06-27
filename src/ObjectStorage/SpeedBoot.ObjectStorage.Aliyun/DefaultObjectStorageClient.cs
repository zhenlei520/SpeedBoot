// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.ObjectStorage.Aliyun;

public class DefaultObjectStorageClient : IObjectStorageClient
{
    private readonly AliyunObjectStorageOptions _storageOptions;

    public DefaultObjectStorageClient(AliyunObjectStorageOptions storageOptions)
    {
        _storageOptions = storageOptions;
    }

    public void Put(string bucketName, string objectName, Stream data)
    {
        throw new NotImplementedException();
    }

    public bool Exists(string bucketName, string objectName)
    {
        throw new NotImplementedException();
    }

    public void Delete(string bucketName, string objectName)
    {
        throw new NotImplementedException();
    }

    public void DeleteRange(string bucketName, IEnumerable<string> objectNames)
    {
        throw new NotImplementedException();
    }

    public Task PutAsync(string bucketName, string objectName, Stream data, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(string bucketName, string objectName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string bucketName, string objectName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRangeAsync(string bucketName, IEnumerable<string> objectNames, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
