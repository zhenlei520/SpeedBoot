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

    public ObjectInfoResponse GetObject(GetObjectInfoRequest request)
    {
        var result = Oss.GetObject(request.BucketName, request.ObjectName);

        _logger?.LogDebug("----- Get {ObjectName} from {BucketName} - ({Result})",
            request.BucketName,
            request.ObjectName,
            result);

        return new ObjectInfoResponse()
        {
            RequestId = result.RequestId,
            Stream = result.Content,
            ContentLength = result.ContentLength,
            ContentMd5 = result.Metadata.ContentMd5,
            ContentType = result.Metadata.ContentType,
            ContentEncoding = result.Metadata.ContentEncoding,
            LastModified = result.Metadata.LastModified,
            ExpirationTime = result.Metadata.ExpirationTime,
            Expand = GetExpand()
        };

        Dictionary<string, object> GetExpand()
        {
            var expand = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in result.Metadata.HttpMetadata)
            {
                if (expand.ContainsKey(item.Key))
                    continue;

                expand.Add(item.Key, item.Value);
            }
            return expand;
        }
    }

    public ObjectInfoResponse GetObject(GetObjectInfoChunkRequest request)
    {
        if (request.Length < 0 && request.Length != -1)
            throw new ArgumentOutOfRangeException(nameof(request.Length), $"{request.Length} should be greater than 0 or -1");

        var objectRequest = new GetObjectRequest(request.BucketName, request.ObjectName);
        objectRequest.SetRange(request.Offset, request.Length > 0 ? request.Offset + request.Length : request.Length);
        var result = Oss.GetObject(objectRequest);

        _logger?.LogDebug("----- Get {ObjectName} from {BucketName} - ({Result})",
            request.ObjectName,
            request.BucketName,
            result);

        return new ObjectInfoResponse()
        {
            Stream = result.Content,
            ContentLength = result.ContentLength
        };
    }

    public void Put(PutObjectStorageRequest request)
    {
        var objectMetadata = _aliyunClientProvider.BuildCallbackMetadata();

        var result = _aliyunClientProvider.EnableResumableUpload(request.Stream.Length) ?
            Oss.PutObject(request.BucketName, request.ObjectName, request.Stream, objectMetadata) :
            Oss.ResumableUploadObject(new UploadObjectRequest(request.BucketName, request.ObjectName, request.Stream)
            {
                PartSize = _aliyunClientProvider.AliyunObjectStorageOptions.PartSize,
                Metadata = objectMetadata
            });

        _logger?.LogDebug("----- Upload {ObjectName} from {BucketName} - ({Result})",
            request.ObjectName,
            request.BucketName,
            result);
    }

    public bool Exists(ExistObjectStorageRequest request)
    {
        return Oss.DoesObjectExist(request.BucketName, request.ObjectName);
    }

    public void Delete(DeleteObjectStorageRequest request)
    {
        var result = Oss.DeleteObject(request.BucketName, request.ObjectName);
        _logger?.LogDebug("----- Delete {ObjectName} from {BucketName} - ({Result})",
            request.ObjectName,
            request.BucketName,
            result);
    }

    public void BatchDelete(BatchDeleteObjectStorageRequest request)
    {
        var result = Oss.DeleteObjects(new DeleteObjectsRequest(request.BucketName, request.ObjectNames,
            _aliyunClientProvider.AliyunObjectStorageOptions.Quiet));
        _logger?.LogDebug("----- Delete {ObjectNames} from {BucketName} - ({Result})",
            request.ObjectNames,
            request.BucketName,
            result);
    }

    public Task<ObjectInfoResponse> GetObjectAsync(GetObjectInfoRequest request, CancellationToken cancellationToken = default)
        => Task.FromResult(GetObject(request));

    public Task<ObjectInfoResponse> GetObjectAsync(GetObjectInfoChunkRequest request,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(GetObject(request));
    }

    public Task PutAsync(PutObjectStorageRequest request, CancellationToken cancellationToken = default)
    {
        Put(request);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(ExistObjectStorageRequest request, CancellationToken cancellationToken = default)
    {
        var res = Exists(request);
        return Task.FromResult(res);
    }

    public Task DeleteAsync(DeleteObjectStorageRequest request, CancellationToken cancellationToken = default)
    {
        Delete(request);
        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(BatchDeleteObjectStorageRequest request, CancellationToken cancellationToken = default)
    {
        BatchDelete(request);
        return Task.CompletedTask;
    }
}
