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

    #region Byte数组保存为文件

    /// <summary>
    /// save large files
    ///
    /// 大文件保存
    /// </summary>
    /// <param name="byteArray">file byte array（文件字节数组）</param>
    /// <param name="filePath">full file path（完整文件地址）</param>
    /// <param name="chunkSize">default: 4096 bytes（4k）</param>
    public static void SaveToBigFile(this byte[] byteArray, string filePath, int chunkSize = 4096)
    {
        using var fs = new FileStream(filePath, FileMode.Create);
        using var ms = new MemoryStream(byteArray);
        var buffer = new byte[chunkSize];
        int bytesRead;

        while ((bytesRead = ms.Read(buffer, 0, buffer.Length)) > 0)
        {
            fs.Write(buffer, 0, bytesRead);
        }
    }

#if NETCOREAPP3_0_OR_GREATER
    /// <summary>
    /// save large files
    ///
    /// 大文件保存
    /// </summary>
    /// <param name="byteArray">file byte array（文件字节数组）</param>
    /// <param name="filePath">full file path（完整文件地址）</param>
    /// <param name="chunkSize">default: 4096 bytes（4k）</param>
    public static async Task SaveToBigFileAsync(this byte[] byteArray, string filePath, int chunkSize = 4096)
    {
        using var fs = new FileStream(filePath, FileMode.Create);
        using var ms = new MemoryStream(byteArray);
        var buffer = new byte[chunkSize];
        int bytesRead;

        while ((bytesRead = await ms.ReadAsync(buffer)) > 0)
        {
            await fs.WriteAsync(buffer.AsMemory(0, bytesRead));
        }
    }
#endif

    /// <summary>
    /// Save the small file
    ///
    /// 保存小文件
    /// </summary>
    /// <param name="byteArray">file byte array（文件字节数组）</param>
    /// <param name="filePath">full file path（完整文件地址）</param>
    public static void SaveToFile(this byte[] byteArray, string filePath)
    {
        using var fs = new FileStream(filePath, FileMode.Create);
        fs.Write(byteArray, 0, byteArray.Length);
    }

#if NETCOREAPP3_0_OR_GREATER

    /// <summary>
    /// Save the small file
    ///
    /// 保存小文件
    /// </summary>
    /// <param name="byteArray">file byte array（文件字节数组）</param>
    /// <param name="filePath">full file path（完整文件地址）</param>
    public static async Task SaveToFileAsync(this byte[] byteArray, string filePath)
    {
        using var fs = new FileStream(filePath, FileMode.Create);
        await fs.WriteAsync(byteArray);
    }

#endif

    #endregion
}
