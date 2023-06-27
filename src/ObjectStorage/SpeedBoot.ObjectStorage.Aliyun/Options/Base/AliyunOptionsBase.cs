// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Aliyun.Options;

/// <summary>
/// aliyun Options
/// 阿里云配置
/// </summary>
public class AliyunOptions
{
    /// <summary>
    /// account AccessKey ID（optional）
    /// 访问秘钥id（可选）
    /// </summary>
    public string AccessKeyId { get; set; }

    /// <summary>
    /// account AccessKey Secret（optional）
    /// 访问秘钥密码（可选）
    /// </summary>
    public string AccessKeySecret { get; set; }
}
