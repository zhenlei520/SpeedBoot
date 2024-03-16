// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Security.Cryptography;

/// <summary>
/// Url Encode and Decode
/// </summary>
public class UrlUtils
{
    /// <summary>
    /// Base64 Encode
    /// </summary>
    /// <param name="content">String to be encrypted</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <returns>encrypted data</returns>
    public static string Encode(string content, Encoding? encoding = null)
        => HttpUtility.UrlEncode(content, encoding.GetSafeEncoding());

    /// <summary>
    /// Base64 Decode
    /// </summary>
    /// <param name="content">String to decrypt</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <returns>decrypted data</returns>
    public static string Decode(string content, Encoding? encoding = null)
        => HttpUtility.UrlDecode(content, encoding.GetSafeEncoding());
}
