// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.System;

public static class ByteExtensions
{

    #region Convert byte array to Base64 string（字节数组转换成Base64字符串）

    /// <summary>
    /// Convert byte array to Base64 string
    /// 字节数组转换成Base64字符串
    /// </summary>
    /// <param name="inArray">the byte array to convert</param>
    /// <returns></returns>
    public static string ToBase64String(this byte[] inArray)
        => Convert.ToBase64String(inArray);

    #endregion

    #region convert byte array to string（byte数组转换为字符串）

    /// <summary>
    /// convert byte array to string（Default encoding format: UTF-8）
    /// byte数组转换为字符串（默认编码格式：UTF-8）
    /// </summary>
    /// <param name="inArray">the byte array to convert</param>
    /// <returns></returns>
    public static string ToString(this byte[] inArray)
        => inArray.ToString(Encoding.UTF8);

    /// <summary>
    /// convert byte array to string（Default encoding format: UTF-8）
    /// byte数组转换为字符串（默认编码格式：UTF-8）
    /// </summary>
    /// <param name="inArray">the byte array to convert</param>
    /// <param name="encoding">encoding format</param>
    /// <returns></returns>
    public static string ToString(this byte[] inArray, Encoding encoding)
        => encoding.GetString(inArray);

    #endregion
}
