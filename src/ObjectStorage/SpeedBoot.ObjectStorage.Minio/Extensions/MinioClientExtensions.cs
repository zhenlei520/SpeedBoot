// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Minio;

public static class MinioClientExtensions
{
    /// <summary>
    /// Check if a private bucket with the given name exists.
    ///
    /// 检查bucket是否存在
    /// </summary>
    /// <param name="minioClient"></param>
    /// <param name="bucketName">bucket name（桶名称、空间名）</param>
    /// <param name="cancellationToken">Optional cancellation token to cancel the operation</param>
    /// <returns></returns>
    public static Task<bool> BucketExistsAsync(this MinioClient minioClient, string bucketName, CancellationToken cancellationToken)
    {
        var bucketExistsArgs = new BucketExistsArgs()
        {
            IsBucketCreationRequest = false
        }.WithBucket(bucketName);

        return minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);
    }
}
