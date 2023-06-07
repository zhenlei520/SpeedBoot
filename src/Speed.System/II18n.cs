// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace
namespace System
{
    // ReSharper disable once InconsistentNaming
    public interface II18n
    {
        /// <summary>
        /// Gets the string resource with the given name.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <returns></returns>
        string T(string name);

        /// <summary>
        /// Gets the string resource with the given name.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <param name="returnKey">Return Key when key does not exist, default: true</param>
        /// <returns></returns>
        string T(string name, bool returnKey);

        /// <summary>
        /// Gets the string resource with the given name and formatted with the supplied arguments.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <param name="arguments">The values to format the string with.</param>
        string T(string name, params object[] arguments);

        /// <summary>
        /// Gets the string resource with the given name and formatted with the supplied arguments.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <param name="returnKey">Return Key when key does not exist, default: true</param>
        /// <param name="arguments">The values to format the string with.</param>
        string T(string name, bool returnKey, params object[] arguments);
    }
}
