// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using Minio.DataModel;

namespace SpeedBoot.ObjectStorage.Minio;

public class DefaultMinioClientProvider : IMinioClientProvider
{
    public MinioObjectStorageOptions MinioObjectStorageOptions { get; }

    public DefaultMinioClientProvider(MinioObjectStorageOptions minioObjectStorageOptions)
    {
        MinioObjectStorageOptions = minioObjectStorageOptions;
    }

    public MinioClient GetClient()
    {
        var credential = GetCredentials();

        var minioClient = new MinioClient()
            .WithEndpoint(MinioObjectStorageOptions.Endpoint)
            .WithCredentials(credential.AccessKeyId, credential.AccessKeySecret);

        if (MinioObjectStorageOptions.EnableHttps) minioClient = minioClient.WithSSL();

        if (MinioObjectStorageOptions.WebProxy != null) minioClient = minioClient.WithProxy(MinioObjectStorageOptions.WebProxy);

        if (MinioObjectStorageOptions.Timeout != null) minioClient = minioClient.WithTimeout(MinioObjectStorageOptions.Timeout.Value);

        if (!MinioObjectStorageOptions.Region.IsNullOrWhiteSpace()) minioClient = minioClient.WithRegion(MinioObjectStorageOptions.Region);

        return minioClient.Build();
    }

    private CredentialsResponse GetCredentials()
        => new(MinioObjectStorageOptions.AccessKeyId, MinioObjectStorageOptions.AccessKeySecret, null);

    /// <summary>
    /// 得到下载凭证
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<string> GetDownloadCredentialAsync(DownloadCredentialRequest request, CancellationToken cancellationToken = default)
    {
        var expiration = request.Expiry ?? MinioStorageConstant.DEFAULT_PUT_OBJECT_EXPIRATION_TIME;
        var bucketName = ObjectStorageOptionsUtils.GetBucketName(request.BucketName,
            MinioObjectStorageOptions.BucketName);
        SpeedArgumentException.ThrowIfNullOrWhiteSpace(bucketName);
        var presignedGetObjectArgs = request.GetObjectsArgs<PresignedGetObjectArgs>()
            .WithBucket(bucketName)
            .WithObject(request.ObjectName);

        if (expiration != -1)
        {
            presignedGetObjectArgs = presignedGetObjectArgs.WithExpiry(expiration);
        }
        return GetClient().PresignedGetObjectAsync(presignedGetObjectArgs);
    }

    /// <summary>
    /// 得到上传凭证
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<string> GetUploadCredentialAsync(UploadCredentialRequest request, CancellationToken cancellationToken = default)
    {
        var expiration = request.Expiry ?? MinioStorageConstant.DEFAULT_PUT_OBJECT_EXPIRATION_TIME;
        var bucketName = ObjectStorageOptionsUtils.GetBucketName(request.BucketName,
            MinioObjectStorageOptions.BucketName);
        SpeedArgumentException.ThrowIfNullOrWhiteSpace(bucketName);
        var presignedPutObjectArgs = request.GetObjectsArgs<PresignedPutObjectArgs>()
            .WithBucket(bucketName)
            .WithObject(request.ObjectName);
        presignedPutObjectArgs = presignedPutObjectArgs.WithExpiry(expiration != -1 ? expiration : MinioStorageConstant.DEFAULT_PUT_OBJECT_MAX_EXPIRATION_TIME);
        return GetClient().PresignedPutObjectAsync(presignedPutObjectArgs);
    }
}
