// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Minio;

public static class ObjectStorageRequestBaseExtensions
{
    public static TObjectsArgs GetObjectsArgs<TObjectsArgs>(this CredentialsRequestBase credentialsRequest)
        where TObjectsArgs : ObjectConditionalQueryArgs<TObjectsArgs>, new()
    {
        return new TObjectsArgs()
            {
                IsBucketCreationRequest = false
            }
            .WithBucket(credentialsRequest.BucketName)
            .WithObject(credentialsRequest.ObjectName);
    }

    public static TObjectsArgs GetObjectsArgs<TObjectsArgs>(this ObjectStorageRequestBase requestBase)
        where TObjectsArgs : ObjectArgs<TObjectsArgs>, new()
    {
        return new TObjectsArgs()
            {
                IsBucketCreationRequest = false
            }
            .WithBucket(requestBase.BucketName)
            .WithObject(requestBase.ObjectName);
    }
}
