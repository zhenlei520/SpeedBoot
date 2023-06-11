// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace
namespace System;

[Serializable]
public abstract class SpeedExceptionBase : Exception
{
    /// <summary>
    /// 异常码
    /// 默认异常消息是硬编码的提示信息，但通过Code可以做到支持本地化消息的异常
    /// </summary>
    public long? Code { get; private set; }

    /// <summary>
    /// 自定义异常参数
    /// </summary>
    public object[]? Parameters { get; private set; }

    protected SpeedExceptionBase(string message)
        : base(message)
    {
    }

    protected SpeedExceptionBase(string message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected SpeedExceptionBase(long code, params object[] parameters)
    {
        Code = code;
        Parameters = parameters;
    }

    protected SpeedExceptionBase(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }

    public override string Message
    {
        get
        {
            if (Code == null)
            {
                return base.Message;
            }
            var parameters = GetParameters();
            return parameters != null ? ExceptionResource.T(Code.Value, parameters) : ExceptionResource.T(Code.Value);
        }
    }

    protected virtual object[]? GetParameters() => Parameters;
}
