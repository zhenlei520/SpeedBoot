// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

using System;
using System.Runtime.Serialization;

namespace Speed.System.Exceptions
{
    [Serializable]
    public class SpeedException : SpeedExceptionBase
    {
        public SpeedException(string message)
            : base(message)
        {
        }

        protected SpeedException(long code, params object[] parameters) : base(code, parameters)
        {
        }

        public SpeedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SpeedException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}
