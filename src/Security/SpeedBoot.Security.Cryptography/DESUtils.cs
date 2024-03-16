// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Security.Cryptography;

/// <summary>
/// DES symmetric encryption and decryption
/// </summary>
// ReSharper disable once InconsistentNaming
// ReSharper disable once ClassNeverInstantiated.Global
public class DESUtils : EncryptBase
{
    /// <summary>
    /// Default encryption key
    /// </summary>
    private static readonly string DefaultEncryptKey = GlobalCryptographyConfig.DefaultDesKey;

    /// <summary>
    /// Default encryption iv
    /// </summary>
    private static readonly string DefaultEncryptIv = GlobalCryptographyConfig.DefaultDesIv;

    /// <summary>
    /// Des encrypted string
    /// </summary>
    /// <param name="content">encrypted string</param>
    /// <param name="isToLower">Whether to convert the encrypted string to lowercase</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <returns>encrypted result</returns>
    public static string Encrypt(
        string content,
        bool isToLower = true,
        Encoding? encoding = null)
        => Encrypt(content, DefaultEncryptKey, isToLower, encoding);

    /// <summary>
    /// Des encrypted string
    /// </summary>
    /// <param name="content">String to be encrypted</param>
    /// <param name="key">8-bit length key or complement by fillPattern to calculate an 8-bit string</param>
    /// <param name="isToLower">Whether to convert the encrypted string to lowercase</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <returns>encrypted result</returns>
    public static string Encrypt(
        string content,
        string key,
        bool isToLower = true,
        Encoding? encoding = null)
        => Encrypt(content, key, DefaultEncryptIv, isToLower, FillPattern.Right, ' ', encoding);

    /// <summary>
    /// Des encrypted string
    /// </summary>
    /// <param name="content">String to be encrypted</param>
    /// <param name="key">8-bit length key or complement by fillPattern to calculate an 8-bit string</param>
    /// <param name="iv">8-bit length key or complement by fillPattern to calculate an 8-bit string</param>
    /// <param name="isToLower">Whether to convert the encrypted string to lowercase</param>
    /// <param name="fillPattern">Do you supplement key and iv? default: no fill(Only supports 8-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <returns>encrypted result</returns>
    public static string Encrypt(
        string content,
        string key,
        string iv,
        bool isToLower = true,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null)
    {
        var currentEncoding = encoding.GetSafeEncoding();
        using var memoryStream = new MemoryStream();
        var buffer = currentEncoding.GetBytes(content);
        var des = DES.Create();
        var keyBuffer = GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter);
        var ivBuffer = GetIvBuffer(iv, currentEncoding, fillPattern, fillCharacter);
        using var cs = new CryptoStream(memoryStream, des.CreateEncryptor(keyBuffer, ivBuffer), CryptoStreamMode.Write);
        cs.Write(buffer, 0, buffer.Length);
        cs.FlushFinalBlock();

        StringBuilder stringBuilder = new();
        foreach (var b in memoryStream.ToArray())
        {
            stringBuilder.AppendFormat(isToLower ? $"{b:x2}" : $"{b:X2}");
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// DES decryption with default key
    /// </summary>
    /// <param name="content">String to be decrypted</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <returns>decrypted result</returns>
    public static string Decrypt(string content,
        Encoding? encoding = null)
        => Decrypt(content, DefaultEncryptKey, encoding);

    /// <summary>
    /// DES decryption
    /// </summary>
    /// <param name="content">String to be decrypted</param>
    /// <param name="key">8-bit length key</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <returns>decrypted result</returns>
    public static string Decrypt(
        string content,
        string key,
        Encoding? encoding = null)
        => Decrypt(content, key, DefaultEncryptIv, FillPattern.Right, ' ', encoding);

    /// <summary>
    /// DES decryption
    /// </summary>
    /// <param name="content">String to be decrypted</param>
    /// <param name="key">8-bit length key or complement by fillPattern to calculate an 8-bit string</param>
    /// <param name="iv">8-bit length key or complement by fillPattern to calculate an 8-bit string</param>
    /// <param name="fillPattern">Do you supplement key and iv? default: no fill(Only supports 8-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <returns>decrypted result</returns>
    public static string Decrypt(
        string content,
        string key,
        string iv,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null)
    {
        var currentEncoding = encoding.GetSafeEncoding();
        using var ms = new MemoryStream();
        var buffers = content.FromBase64String();

        using var des = DES.Create();
        var keyBuffer = GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter);
        var ivBuffer = GetIvBuffer(iv, currentEncoding, fillPattern, fillCharacter);
        using (var cs = new CryptoStream(ms, des.CreateDecryptor(keyBuffer, ivBuffer), CryptoStreamMode.Write))
        {
            cs.Write(buffers, 0, buffers.Length);
            cs.FlushFinalBlock();
        }

        return currentEncoding.GetString(ms.ToArray());
    }

    /// <summary>
    /// DES encrypts the file stream and outputs the encrypted file
    /// </summary>
    /// <param name="fileStream">file input stream</param>
    /// <param name="key">8-bit length key or complement by fillPattern to calculate an 8-bit string</param>
    /// <param name="outFilePath">file output path</param>
    /// <param name="fillPattern">Do you supplement key and iv? default: no fill(Only supports 8-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    public static void EncryptFile(
        FileStream fileStream,
        string key,
        string outFilePath,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null)
        => EncryptOrDecryptFile(fileStream, key, outFilePath, true, fillPattern, fillCharacter, encoding);

    /// <summary>
    /// DES encrypts the file stream and outputs the encrypted file
    /// </summary>
    /// <param name="fileStream">file input stream</param>
    /// <param name="key">8-bit length key or complement by fillPattern to calculate an 8-bit string</param>
    /// <param name="iv">8-bit length key or complement by fillPattern to calculate an 8-bit string</param>
    /// <param name="outFilePath">file output path</param>
    /// <param name="fillPattern">Do you supplement key and iv? default: no fill(Only supports 8-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    public static void EncryptFile(
        FileStream fileStream,
        string key,
        string iv,
        string outFilePath,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null)
        => EncryptOrDecryptFile(fileStream, key, iv, true, outFilePath, fillPattern, fillCharacter, encoding);

    /// <summary>
    /// DES encrypts the file stream and outputs the encrypted file
    /// </summary>
    /// <param name="fileStream">file input stream</param>
    /// <param name="key">8-bit length key</param>
    /// <param name="ivBuffer">8-bit length key</param>
    /// <param name="outFilePath">file output path</param>
    /// <param name="fillPattern">Do you supplement key and iv? default: no fill(Only supports 8-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    public static void EncryptFile(
        FileStream fileStream,
        string key,
        byte[] ivBuffer,
        string outFilePath,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null)
    {
        var currentEncoding = encoding.GetSafeEncoding();
        EncryptOrDecryptFile(
            fileStream,
            GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter),
            ivBuffer,
            true,
            outFilePath);
    }

    /// <summary>
    /// DES decrypts the file stream and outputs the source file
    /// </summary>
    /// <param name="fileStream">input file stream to be decrypted</param>
    /// <param name="key">decryption key or complement by fillPattern to calculate an 8-bit string</param>
    /// <param name="outFilePath">file output path</param>
    /// <param name="fillPattern">Do you supplement key and iv? default: no fill(Only supports 8-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    public static void DecryptFile(
        FileStream fileStream,
        string key,
        string outFilePath,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null)
        => EncryptOrDecryptFile(fileStream, key, outFilePath, false, fillPattern, fillCharacter, encoding);

    /// <summary>
    /// DES decrypts the file stream and outputs the source file
    /// </summary>
    /// <param name="fileStream">input file stream to be decrypted</param>
    /// <param name="key">decryption key or complement by fillPattern to calculate an 8-bit string</param>
    /// <param name="iv">8-bit length key or complement by fillPattern to calculate an 8-bit string</param>
    /// <param name="outFilePath">file output path</param>
    /// <param name="fillPattern">Do you supplement key and iv? default: no fill(Only supports 8-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    public static void DecryptFile(
        FileStream fileStream,
        string key,
        string iv,
        string outFilePath,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null)
        => EncryptOrDecryptFile(fileStream, key, iv, false, outFilePath, fillPattern, fillCharacter, encoding);

    /// <summary>
    /// DES decrypts the file stream and outputs the source file
    /// </summary>
    /// <param name="fileStream">input file stream to be decrypted</param>
    /// <param name="key">decryption key or complement by fillPattern to calculate an 8-bit string</param>
    /// <param name="ivBuffer"></param>
    /// <param name="outFilePath">file output path</param>
    /// <param name="fillPattern">Do you supplement key and iv? default: no fill(Only supports 8-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    public static void DecryptFile(
        FileStream fileStream,
        string key,
        byte[] ivBuffer,
        string outFilePath,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null)
    {
        var currentEncoding = encoding.GetSafeEncoding();
        EncryptOrDecryptFile(
            fileStream,
            GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter),
            ivBuffer,
            false,
            outFilePath);
    }

    private static void EncryptOrDecryptFile(
        FileStream fileStream,
        string key,
        string outFilePath,
        bool isEncrypt,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null)
        => EncryptOrDecryptFile(fileStream, key, DefaultEncryptKey, isEncrypt, outFilePath, fillPattern, fillCharacter, encoding);

    private static void EncryptOrDecryptFile(
        FileStream fileStream,
        string key,
        string iv,
        bool isEncrypt,
        string outFilePath,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null)
    {
        var currentEncoding = encoding.GetSafeEncoding();
        EncryptOrDecryptFile(
            fileStream,
            GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter),
            GetIvBuffer(iv, currentEncoding, fillPattern, fillCharacter),
            isEncrypt,
            outFilePath);
    }

    private static void EncryptOrDecryptFile(
        FileStream fileStream,
        byte[] keyBuffer,
        byte[] ivBuffer,
        bool isEncrypt,
        string outFilePath)
    {
        using var fileStreamOut = new FileStream(outFilePath, FileMode.Create);
        using var des = DES.Create();
        var transform = GetCryptoTransform(des, keyBuffer, ivBuffer, isEncrypt);
        EncryptOrDecryptFile(fileStream, fileStreamOut, transform);
    }

    private static byte[] GetKeyBuffer(string key,
        Encoding encoding,
        FillPattern fillPattern,
        char fillCharacter)
        => GetBytes(
            key,
            encoding,
            fillPattern,
            fillCharacter,
            nameof(key),
            $"Please enter a 8-bit DES key or allow {nameof(fillPattern)} to Left or Right");

    private static byte[] GetIvBuffer(string iv,
        Encoding encoding,
        FillPattern fillPattern,
        char fillCharacter)
        => GetBytes(
            iv,
            encoding,
            fillPattern,
            fillCharacter,
            nameof(iv),
            $"Please enter a 8-bit DES iv or allow {nameof(fillPattern)} to Left or Right");

    private static byte[] GetBytes(string str,
        Encoding encoding,
        FillPattern fillPattern,
        char fillCharacter,
        string paramName,
        string message)
    {
        return GetBytes(
            str,
            encoding,
            fillPattern,
            fillCharacter,
            8,
            () => new ArgumentException(message, paramName));
    }
}
