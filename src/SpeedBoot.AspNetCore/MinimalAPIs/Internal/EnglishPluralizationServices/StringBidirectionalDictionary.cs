// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Internal.EnglishPluralizationServices;

internal class StringBidirectionalDictionary : BidirectionalDictionary<string, string>
{
    internal StringBidirectionalDictionary() : base() { }

    internal StringBidirectionalDictionary(Dictionary<string, string> firstToSecondDictionary)
        : base(firstToSecondDictionary)
    {
    }

    internal override bool ExistsInFirst(string value)
    {
        return base.ExistsInFirst(value.ToLowerInvariant());
    }

    internal override bool ExistsInSecond(string value)
    {
        return base.ExistsInSecond(value.ToLowerInvariant());
    }

    internal override string? GetFirstValue(string value)
    {
        return base.GetFirstValue(value.ToLowerInvariant());
    }

    internal override string? GetSecondValue(string value)
    {
        return base.GetSecondValue(value.ToLowerInvariant());
    }

}
