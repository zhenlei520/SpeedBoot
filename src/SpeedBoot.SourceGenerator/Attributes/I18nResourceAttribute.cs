// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class I18nResourceAttribute : Attribute
{
    public string Describe { get; set; }

    public string Culture { get; set; }

    public I18nResourceAttribute(string describe, string? culture = null)
    {
        TrySet(culture, item => { Culture = item; }, () => CultureInfo.CurrentUICulture.Name);
        Describe = describe;
        return;

        void TrySet(string? name, Action<string> action, Func<string> defaultCultureFunc)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                action.Invoke(name!);
            }

            action.Invoke(defaultCultureFunc.Invoke());
        }
    }
}
