// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Security.Cryptography;

/// <summary>
/// Hash algorithm encryption SHA256
/// </summary>
// ReSharper disable once InconsistentNaming
public class SHA256Utils : HashAlgorithmBase
{
    /// <summary>
    /// Encrypt string with SHA256
    /// </summary>
    /// <param name="content">String to be encrypted</param>
    /// <param name="isToLower">Whether to convert the encrypted string to lowercase</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <returns>encrypted result</returns>
    public static string Encrypt(string content, bool isToLower = true, Encoding? encoding = null)
        => Encrypt(EncryptType.Sha256, content, isToLower, encoding);
}
