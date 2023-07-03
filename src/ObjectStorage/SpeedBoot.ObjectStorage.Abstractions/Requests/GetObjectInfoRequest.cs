// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

public class GetObjectInfoRequest : ObjectStorageRequestBase
{
    public GetObjectInfoRequest() { }

    public GetObjectInfoRequest(string bucketName, string objectName)
        : base(bucketName, objectName)
    {
    }
}
