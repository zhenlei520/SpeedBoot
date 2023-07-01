// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.ObjectStorage.Aliyun;

public class DefaultObjectStorageClient : IObjectStorageClient
{
    private IOss? _oss;
    private IOss Oss => _oss ??= _aliyunClientProvider.GetClient();

    private readonly IAliyunClientProvider _aliyunClientProvider;
    private readonly ILogger? _logger;

    public DefaultObjectStorageClient(
        IAliyunClientProvider aliyunClientProvider,
        ILogger? logger)
    {
        _aliyunClientProvider = aliyunClientProvider;
        _logger = logger;
    }

    public CredentialsResponse GetCredentials() => _aliyunClientProvider.GetCredentials();

    public string GetToken() => throw new NotSupportedException("GetToken is not supported, please use GetSecurityToken");

    public Stream GetObject(string bucketName, string objectName)
        => GetObject(bucketName, objectName, out _);

    public Stream GetObject(
        string bucketName,
        string objectName,
        out long contentLength)
    {
        var result = Oss.GetObject(bucketName, objectName);

        _logger?.LogDebug("----- Get {ObjectName} from {BucketName} - ({Result})",
            objectName,
            bucketName,
            result);

        contentLength = result.ContentLength;
        return result.Content;
    }

    public Stream GetObject(string bucketName, string objectName, long offset, long length)
    {
        if (length < 0 && length != -1)
            throw new ArgumentOutOfRangeException(nameof(length), $"{length} should be greater than 0 or -1");

        var request = new GetObjectRequest(bucketName, objectName);
        request.SetRange(offset, length > 0 ? offset + length : length);
        var result = Oss.GetObject(request);

        _logger?.LogDebug("----- Get {ObjectName} from {BucketName} - ({Result})",
            objectName,
            bucketName,
            result);

        return result.Content;
    }

    public void Put(string bucketName, string objectName, Stream data)
    {
        var objectMetadata = _aliyunClientProvider.BuildCallbackMetadata();

        var result = _aliyunClientProvider.EnableResumableUpload(data.Length) ?
            Oss.PutObject(bucketName, objectName, data, objectMetadata) :
            Oss.ResumableUploadObject(new UploadObjectRequest(bucketName, objectName, data)
            {
                PartSize = _aliyunClientProvider.AliyunObjectStorageOptions.PartSize,
                Metadata = objectMetadata
            });

        _logger?.LogDebug("----- Upload {ObjectName} from {BucketName} - ({Result})",
            objectName,
            bucketName,
            result);
    }

    public bool Exists(string bucketName, string objectName)
    {
        return Oss.DoesObjectExist(bucketName, objectName);
    }

    public void Delete(string bucketName, string objectName)
    {
        var result = Oss.DeleteObject(bucketName, objectName);
        _logger?.LogDebug("----- Delete {ObjectName} from {BucketName} - ({Result})",
            objectName,
            bucketName,
            result);
    }

    public void DeleteRange(string bucketName, IEnumerable<string> objectNames)
    {
        var result = Oss.DeleteObjects(new DeleteObjectsRequest(bucketName, objectNames.ToList(),
            _aliyunClientProvider.AliyunObjectStorageOptions.Quiet));
        _logger?.LogDebug("----- Delete {ObjectNames} from {BucketName} - ({Result})",
            objectNames,
            bucketName,
            result);
    }

    public Task<Stream> GetObjectAsync(string bucketName, string objectName, CancellationToken cancellationToken = default)
        => Task.FromResult(GetObject(bucketName, objectName));

    public Task<Stream> GetObjectAsync(
        string bucketName,
        string objectName,
        out long contentLength,
        CancellationToken cancellationToken = default)
        => Task.FromResult(GetObject(bucketName, objectName, out contentLength));

    public Task<Stream> GetObjectAsync(string bucketName,
        string objectName,
        long offset,
        long length,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(GetObject(bucketName, objectName, offset, length));
    }

    public Task PutAsync(string bucketName, string objectName, Stream data, CancellationToken cancellationToken = default)
    {
        Put(bucketName, objectName, data);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(string bucketName, string objectName, CancellationToken cancellationToken = default)
    {
        var res = Exists(bucketName, objectName);
        return Task.FromResult(res);
    }

    public Task DeleteAsync(string bucketName, string objectName, CancellationToken cancellationToken = default)
    {
        Delete(bucketName, objectName);
        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(string bucketName, IEnumerable<string> objectNames, CancellationToken cancellationToken = default)
    {
        DeleteRange(bucketName, objectNames);
        return Task.CompletedTask;
    }
}
