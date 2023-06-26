// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System.IO;

public static class StreamExtensions
{

    #region Convert stream to byte array（流转换为byte数组）

    /// <summary>
    /// Convert stream to byte array
    /// 流转换为byte数组
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static byte[] ToBytes(this Stream? stream)
    {
        if (stream?.CanRead != true) return Array.Empty<byte>();

        if (!stream.CanSeek) stream.Position = 0;

        var memoryStream = new MemoryStream();

        stream.CopyTo(memoryStream);

        stream.Position = 0;

        return memoryStream.ToArray();
    }

    /// <summary>
    /// Convert stream to byte array
    /// 流转换为byte数组
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static async Task<byte[]> ToBytesAsync(this Stream? stream)
    {
        if (stream?.CanRead != true) return Array.Empty<byte>();

        if (!stream.CanSeek) stream.Position = 0;

        var memoryStream = new MemoryStream();

        await stream.CopyToAsync(memoryStream);

        stream.Position = 0;

        return memoryStream.ToArray();
    }

    #endregion
}
