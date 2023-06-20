// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

[assembly: InternalsVisibleTo("Speed.System")]

// ReSharper disable once CheckNamespace
namespace System;

/// <summary>
/// Exception Information Multilingual Resources
/// </summary>
internal static class ExceptionConstant
{
    [Description("'{0}' cannot contain {1}.")]
    public const long NOT_CONTAIN = 1;

    [Description("'{0}' must be greater than or equal to '{1}' and less than or equal to '{2}'.")]
    public const long OUT_OF_RANGE = 2;

    [Description("'{0}' must be greater than '{1}'.")]
    public const long GREATER_THAN = 3;

    [Description("'{0}' must be greater than or equal to '{1}'.")]
    public const long GREATER_THAN_OR_EQUAL = 4;

    [Description("'{0}' must be less than '{1}'.")]
    public const long LESS_THAN = 5;

    [Description("'{0}' must be less than or equal to '{1}'.")]
    public const long LESS_THAN_OR_EQUAL = 6;

    [Description("'{0}' cannot be Null or whitespace.")]
    public const long NOT_NULL_AND_WHITESPACE = 7;

    [Description("'{0}' cannot be null and empty.")]
    public const long NOT_NULL_AND_EMPTY = 8;

    [Description("'{0}' must not be empty.")]
    public const long NOT_NULL = 9;

    [Description("'{0}' cannot be Null or empty collection.")]
    public const long NOT_NULL_AND_EMPTY_COLLECTION = 10;
}
