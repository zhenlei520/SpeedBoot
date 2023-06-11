// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NETCOREAPP3_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace Speed.System.Exceptions
{
    public class SpeedArgumentException : SpeedExceptionBase
    {
        public string? ParamName { get; private set; }

        protected SpeedArgumentException(long code, string? paramName, params object[] parameters) : base(code, parameters)
        {
            ParamName = paramName;
        }

        public SpeedArgumentException(string message, string paramName, Exception? innerException = null)
            : base(message, innerException)
        {
            ParamName = paramName;
        }

        public SpeedArgumentException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }

        public static void ThrowIfNullOrEmptyCollection<T>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1
            [NotNull]
#endif
            IEnumerable<T>? arguments,
#if NETCOREAPP3_0_OR_GREATER
            [CallerArgumentExpression("arguments")]
#endif
            string? paramName = null)
        {
            ThrowIf(arguments is null || !arguments.Any(),
                ExceptionCode.NOT_NULL_AND_EMPTY_COLLECTION,
                paramName);
        }

        public static void ThrowIfNull(object? argument,
#if NETCOREAPP3_0_OR_GREATER
[CallerArgumentExpression("argument")]
#endif
            string? paramName = null)
        {
            ThrowIf(argument is null,
                ExceptionCode.NOT_NULL,
                paramName);
        }

        public static void ThrowIfNullOrEmpty(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1
            [NotNull]
#endif
            object? argument,
#if NETCOREAPP3_0_OR_GREATER
[CallerArgumentExpression("argument")]
#endif
            string? paramName = null)
        {
            ThrowIf(string.IsNullOrEmpty(argument?.ToString()),
                ExceptionCode.NOT_NULL_AND_EMPTY,
                paramName);
        }

        public static void ThrowIfNullOrWhiteSpace(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1
            [NotNull]
#endif
            object? argument,
#if NETCOREAPP3_0_OR_GREATER
            [CallerArgumentExpression("argument")]
#endif
            string? paramName = null)
        {
            ThrowIf(string.IsNullOrWhiteSpace(argument?.ToString()),
                ExceptionCode.NOT_NULL_AND_WHITESPACE,
                paramName);
        }

        public static void ThrowIfGreaterThan<T>(T argument,
            T maxValue,
#if NETCOREAPP3_0_OR_GREATER
            [CallerArgumentExpression("argument")]
#endif
            string? paramName = null) where T : IComparable
        {
            ThrowIf(argument.CompareTo(maxValue) > 0,
                ExceptionCode.LESS_THAN_OR_EQUAL,
                paramName,
                maxValue);
        }

        public static void ThrowIfGreaterThanOrEqual<T>(T argument,
            T maxValue,
#if NETCOREAPP3_0_OR_GREATER
            [CallerArgumentExpression("argument")]
#endif
            string? paramName = null) where T : IComparable
        {
            ThrowIf(argument.CompareTo(maxValue) >= 0,
                ExceptionCode.LESS_THAN,
                paramName,
                maxValue);
        }

        public static void ThrowIfLessThan<T>(T argument,
            T minValue,
#if NETCOREAPP3_0_OR_GREATER
            [CallerArgumentExpression("argument")]
#endif
            string? paramName = null) where T : IComparable
        {
            ThrowIf(argument.CompareTo(minValue) < 0,
                ExceptionCode.GREATER_THAN_OR_EQUAL,
                paramName,
                minValue);
        }

        public static void ThrowIfLessThanOrEqual<T>(T argument,
            T minValue,
#if NETCOREAPP3_0_OR_GREATER
            [CallerArgumentExpression("argument")]
#endif
            string? paramName = null) where T : IComparable
        {
            ThrowIf(argument.CompareTo(minValue) <= 0,
                ExceptionCode.GREATER_THAN,
                paramName,
                minValue);
        }

        public static void ThrowIfOutOfRange<T>(T argument,
            T minValue,
            T maxValue,
#if NETCOREAPP3_0_OR_GREATER
            [CallerArgumentExpression("argument")]
#endif
            string? paramName = null) where T : IComparable
        {
            ThrowIf(argument.CompareTo(minValue) < 0 || argument.CompareTo(maxValue) > 0,
                ExceptionCode.OUT_OF_RANGE,
                paramName,
                minValue,
                maxValue);
        }

        public static void ThrowIfContain(string? argument,
            string parameter,
#if NETCOREAPP3_0_OR_GREATER
            [CallerArgumentExpression("argument")]
#endif
            string? paramName = null)
            => ThrowIfContain(argument, parameter, StringComparison.OrdinalIgnoreCase, paramName);

        public static void ThrowIfContain(
            string? argument,
            string parameter,
            StringComparison stringComparison,
#if NETCOREAPP3_0_OR_GREATER
            [CallerArgumentExpression("argument")]
#endif
            string? paramName = null)
        {
            if (argument != null)
            {
                ThrowIf(argument.Contains(parameter, stringComparison), ExceptionCode.NOT_CONTAIN, paramName, argument);
            }
        }

        public static void ThrowIf(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1
            [DoesNotReturnIf(true)]
#endif
            bool condition,
            long errorCode,
            string? paramName,
            params object[] parameters)
        {
            if (condition) Throw(errorCode, paramName, parameters);
        }

        public static void ThrowIf(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1
            [DoesNotReturnIf(true)]
#endif
            bool condition,
            string message,
            string paramName)
        {
            if (condition) Throw(message, paramName);
        }

#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1
        [DoesNotReturn]
#endif
        private static void Throw(
            long errorCode,
            string? paramName,
            params object[] parameters) =>
            throw new SpeedArgumentException(errorCode, paramName, parameters);

#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1
        [DoesNotReturn]
#endif
        private static void Throw(
            string message,
            string paramName) =>
            throw new SpeedArgumentException(message, paramName);

        protected override object[] GetParameters()
        {
            var parameters = new List<object>()
            {
                ParamName!
            };
            if (Parameters != null) parameters.AddRange(Parameters);
            return parameters.ToArray();
        }
    }
}
