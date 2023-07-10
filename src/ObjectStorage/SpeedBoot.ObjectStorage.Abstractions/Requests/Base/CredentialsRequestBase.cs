// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

/// <summary>
/// 凭证
/// </summary>
public abstract class CredentialsRequestBase : ObjectStorageRequestBase
{
    protected CredentialsRequestBase(string? bucketName, string objectName)
        : base(bucketName, objectName)
    {

    }
}
