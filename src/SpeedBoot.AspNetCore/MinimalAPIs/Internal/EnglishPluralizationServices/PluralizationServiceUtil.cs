// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Internal.EnglishPluralizationServices;

internal static class PluralizationServiceUtil
{
    internal static bool DoesWordContainSuffix(string word, IEnumerable<string> suffixes, CultureInfo culture)
    {
        return suffixes.Any(s => word.EndsWith(s, true, culture));
    }

    internal static bool TryInflectOnSuffixInWord(string word, IEnumerable<string> suffixes, Func<string, string> operationOnWord,
        CultureInfo culture, out string? newWord)
    {
        newWord = null;

        if (DoesWordContainSuffix(word, suffixes, culture))
        {
            newWord = operationOnWord(word);
            return true;
        }
        return false;
    }
}
