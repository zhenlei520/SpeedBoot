// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

public abstract class ObjectStorageResponseBase
{
    /// <summary>
    /// request id（used to assist in troubleshooting requests）
    ///
    /// 请求id（用于协助排查请求问题）
    /// </summary>
    public string? RequestId { get; set; }
}
