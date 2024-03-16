// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Security.Cryptography;

internal static class EncodingExtensions
{
    public static Encoding GetSafeEncoding(this Encoding? encoding)
        => encoding ?? GlobalCryptographyConfig.DefaultEncoding;
}
