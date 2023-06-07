// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

// ReSharper disable once CheckNamespace

using System.Collections.Generic;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace System
{
    internal static class ExceptionResource
    {
        public static II18n<SpeedExceptionBase>? I18N { get; set; }

        static readonly Dictionary<long, string> Data = new Dictionary<long, string>();

        /// <summary>
        /// Gets the string resource with the given name.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <returns></returns>
        public static string T(long name) => I18N?.T(GetKey(name)) ?? GetMessage(name);

        /// <summary>
        /// Gets the string resource with the given name and formatted with the supplied arguments.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <param name="arguments">The values to format the string with.</param>
        public static string T(long name, params object[] arguments) => I18N?.T(GetKey(name), arguments) ?? GetMessage(name, arguments);

        private static string GetKey(long name) => $"exception.{name}";

        private static string GetMessage(long name, params object[]? arguments)
        {
            return arguments!=null ? string.Format(CultureInfo.CurrentCulture, Data[name], arguments) : string.Format(CultureInfo.CurrentCulture, Data[name]);

        }
    }
}
