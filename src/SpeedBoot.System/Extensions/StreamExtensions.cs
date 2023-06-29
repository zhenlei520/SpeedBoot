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
    /// 流转换为 byte 数组
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

    #region 文件流保存到本地

    /// <summary>
    /// save large files
    ///
    /// 保存大文件
    /// </summary>
    /// <param name="stream">file Stream（文件流）</param>
    /// <param name="filePath">full file path（完整文件地址）</param>
    /// <param name="chunkSize">default: 4096 bytes（4k）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    public static void SaveToBigFile(this Stream stream, string filePath, int chunkSize = 4096, bool enableOverwrite = false)
    {
        CheckFileExist(filePath, enableOverwrite);

        using var fs = File.Open(filePath, FileMode.Create);
        var buffer = new byte[chunkSize];
        int count;
        while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
            fs.Write(buffer, 0, count);
        fs.Flush();
    }

    /// <summary>
    /// save large files
    ///
    /// 保存大文件
    /// </summary>
    /// <param name="stream">file Stream（文件流）</param>
    /// <param name="filePath">full file path（完整文件地址）</param>
    /// <param name="chunkSize">default: 4096 bytes（4k）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    public static async Task SaveToBigFileAsync(this Stream stream, string filePath, int chunkSize = 4096, bool enableOverwrite = false)
    {
        CheckFileExist(filePath, enableOverwrite);

        using var fs = File.Open(filePath, FileMode.Create);
        var buffer = new byte[chunkSize];
        int count;
        while ((count = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            await fs.WriteAsync(buffer, 0, count);
        fs.Flush();
    }

    /// <summary>
    /// Save the file (no more than 2G)
    ///
    /// 保存文件（不得超过2G）
    /// </summary>
    /// <param name="stream">file Stream（文件流）</param>
    /// <param name="filePath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    public static void SaveToFile(this Stream stream, string filePath, bool enableOverwrite = false)
    {
        CheckFileExist(filePath, enableOverwrite);

        var data = new byte[stream.Length];
        _ = stream.Read(data, 0, (int)stream.Length);
        File.WriteAllBytes(filePath, data);
    }

    /// <summary>
    /// Save the file (no more than 2G)
    ///
    /// 保存文件（不得超过2G）
    /// </summary>
    /// <param name="stream">file Stream（文件流）</param>
    /// <param name="filePath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    public static async Task SaveToFileAsync(this Stream stream, string filePath, bool enableOverwrite = false)
    {
        CheckFileExist(filePath, enableOverwrite);
        var data = new byte[stream.Length];
        _ = await stream.ReadAsync(data, 0, (int)stream.Length);
        File.WriteAllBytes(filePath, data);
    }

    #endregion

    /// <summary>
    /// Check whether the file exists, and if it exists and allow overwriting is not enabled, an exception is thrown
    ///
    /// 检查文件是否存在，如果存在且未开启允许覆盖，则抛出异常
    /// </summary>
    /// <param name="filePath">full file path（完整文件地址）</param>
    /// <param name="enableOverwrite">enable file overwrite（启用文件覆盖） default: false</param>
    /// <exception cref="SpeedFriendlyException"></exception>
    private static void CheckFileExist(string filePath, bool enableOverwrite = false)
    {
        if (File.Exists(filePath) && !enableOverwrite)
            throw new SpeedFriendlyException("The file already exists");
    }
}
