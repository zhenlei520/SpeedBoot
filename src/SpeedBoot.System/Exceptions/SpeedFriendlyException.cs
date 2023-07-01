// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System;

[Serializable]
public class SpeedFriendlyException : SpeedExceptionBase
{
    public SpeedFriendlyException(string message) : base(message)
    {
    }

    public SpeedFriendlyException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    public SpeedFriendlyException(long code, params object[] parameters) : base(code, parameters)
    {
    }

    public SpeedFriendlyException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
    {
    }
}
