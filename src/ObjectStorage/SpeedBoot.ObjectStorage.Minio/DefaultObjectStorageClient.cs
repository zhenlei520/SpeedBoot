// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.ObjectStorage.Minio;

public class DefaultObjectStorageClient : IObjectStorageClient
{
    private MinioClient? _minioClient;
    private MinioClient MinioClient => _minioClient ??= _minioClientProvider.GetClient();

    private readonly ILogger? _logger;
    private readonly IMinioClientProvider _minioClientProvider;

    public DefaultObjectStorageClient(
        IMinioClientProvider minioClientProvider,
        ILogger? logger = null)
    {
        _minioClientProvider = minioClientProvider;
        _logger = logger;
    }

    public CredentialsResponse GetCredentials()
        => _minioClientProvider.GetCredentials();

    public string GetToken() => throw new NotSupportedException("GetToken is not supported, please use GetSecurityToken");

    public ObjectInfoResponse GetObject(GetObjectInfoRequest request)
        => GetObjectAsync(request).ToSync();

    public ObjectInfoResponse GetObject(GetObjectInfoChunkRequest request)
        => GetObjectAsync(request).ToSync();

    public void Put(PutObjectStorageRequest request)
        => PutAsync(request).ToSync();

    public bool Exists(ExistObjectStorageRequest request)
        => ExistsAsync(request).ToSync();

    public void Delete(DeleteObjectStorageRequest request)
        => DeleteAsync(request).ToSync();

    public void BatchDelete(BatchDeleteObjectStorageRequest request)
        => BatchDeleteAsync(request).ToSync();

    public async Task<ObjectInfoResponse> GetObjectAsync(GetObjectInfoRequest request, CancellationToken cancellationToken = default)
    {
        var memoryStream = new MemoryStream();
        var getObjectArgs = GetObjectsArgs<GetObjectArgs>(request)
            .WithCallbackStream(stream =>
            {
                if (stream != null)
                {
                    stream.CopyTo(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                }
                else
                {
                    memoryStream = null;
                }
            });
        var objectStat = await MinioClient.GetObjectAsync(getObjectArgs, cancellationToken);
        return new ObjectInfoResponse()
        {
            RequestId = objectStat.VersionId,
            Stream = memoryStream!,
            ContentLength = objectStat.Size,
            ContentType = objectStat.ContentType,
            LastModified = objectStat.LastModified,
            ExpirationTime = objectStat.Expires,
            Expand = objectStat.ExtraHeaders.ToDictionary(item => item.Key, item => (object)item.Value)
        };
    }

    public async Task<ObjectInfoResponse> GetObjectAsync(GetObjectInfoChunkRequest request, CancellationToken cancellationToken = default)
    {
        Stream? objectStream = null;
        var getObjectArgs = GetObjectsArgs<GetObjectArgs>(request)
            .WithOffsetAndLength(request.Offset, request.Length)
            .WithCallbackStream(stream =>
            {
                objectStream = stream;
            });
        var objectStat = await MinioClient.GetObjectAsync(getObjectArgs, cancellationToken);
        return new ObjectInfoResponse()
        {
            RequestId = objectStat.VersionId,
            Stream = objectStream!,
            ContentLength = objectStat.Size,
            ContentType = objectStat.ContentType,
            LastModified = objectStat.LastModified,
            ExpirationTime = objectStat.Expires,
            Expand = objectStat.ExtraHeaders.ToDictionary(item => item.Key, item => (object)item.Value)
        };
    }

    public async Task PutAsync(PutObjectStorageRequest request, CancellationToken cancellationToken = default)
    {
        var putObjectArgs = GetObjectsArgs<PutObjectArgs>(request)
            .WithStreamData(request.Stream)
            .WithObjectSize(request.Stream.Length);

        if (!request.ContentType.IsNullOrWhiteSpace())
        {
            putObjectArgs = putObjectArgs.WithContentType(request.ContentType);
        }
        var putObjectResponse = await MinioClient.PutObjectAsync(putObjectArgs, cancellationToken);
    }

    public async Task<bool> ExistsAsync(ExistObjectStorageRequest request, CancellationToken cancellationToken = default)
    {
        if (!await MinioClient.BucketExistsAsync(request.BucketName, cancellationToken))
            return false;

        try
        {
            var statObjectArgs = GetObjectsArgs<StatObjectArgs>(request);
            var objectStat = await MinioClient.StatObjectAsync(statObjectArgs, cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            if (ex is ObjectNotFoundException)
            {
                return false;
            }

            throw;
        }
    }

    public Task DeleteAsync(DeleteObjectStorageRequest request, CancellationToken cancellationToken = default)
    {
        var removeObjectArgs = new RemoveObjectArgs()
            {
                IsBucketCreationRequest = false
            }
            .WithBucket(request.BucketName)
            .WithObject(request.ObjectName);
        return MinioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
    }

    public Task BatchDeleteAsync(BatchDeleteObjectStorageRequest request, CancellationToken cancellationToken = default)
    {
        var removeObjectsArgs = new RemoveObjectsArgs()
            {
                IsBucketCreationRequest = false
            }
            .WithBucket(request.BucketName)
            .WithObjects(request.ObjectNames);
        return MinioClient.RemoveObjectsAsync(removeObjectsArgs, cancellationToken);
    }

    private static TObjectsArgs GetObjectsArgs<TObjectsArgs>(ObjectStorageRequestBase requestBase)
        where TObjectsArgs : ObjectConditionalQueryArgs<TObjectsArgs>, new()
    {
        return new TObjectsArgs()
            {
                IsBucketCreationRequest = false
            }
            .WithBucket(requestBase.BucketName)
            .WithObject(requestBase.ObjectName);
    }
}
