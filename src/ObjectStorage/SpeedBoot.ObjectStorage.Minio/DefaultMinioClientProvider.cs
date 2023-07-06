// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.ObjectStorage.Minio;

public class DefaultMinioClientProvider : IMinioClientProvider
{
    public MinioObjectStorageOptions MinioObjectStorageOptions { get; }

    public DefaultMinioClientProvider(MinioObjectStorageOptions minioObjectStorageOptions)
    {
        MinioObjectStorageOptions = minioObjectStorageOptions;
    }

    public CredentialsResponse GetCredentials()
    {
        return new CredentialsResponse(MinioObjectStorageOptions.AccessKeyId, MinioObjectStorageOptions.AccessKeySecret, null);
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
}
