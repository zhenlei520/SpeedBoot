// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Minio;

internal static class ObjectStorageOptionsUtils
{
    public static string? GetBucketName(string? bucketName, string? defaultBucketName)
    {
        if (!bucketName.IsNullOrWhiteSpace())
            return bucketName;

        return defaultBucketName;
    }
}
