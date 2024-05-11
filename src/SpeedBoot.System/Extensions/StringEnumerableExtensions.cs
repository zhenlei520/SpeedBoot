// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System;

public static partial class EnumerableExtensions
{
    public static List<string> ToStringList(this IEnumerable<string> source)
    {
        return [..source];
    }

    public static List<short> ToShortList(this IEnumerable<string> source)
    {
        return [..source.Select(short.Parse)];
    }

    public static List<int> ToIntList(this IEnumerable<string> source)
    {
        return [..source.Select(int.Parse)];
    }

    public static List<long> ToLongList(this IEnumerable<string> source)
    {
        return [..source.Select(long.Parse)];
    }

    public static List<float> ToFloatList(this IEnumerable<string> source)
    {
        return [..source.Select(float.Parse)];
    }

    public static List<decimal> ToDecimalList(this IEnumerable<string> source)
    {
        return [..source.Select(decimal.Parse)];
    }

    public static List<double> ToDoubleList(this IEnumerable<string> source)
    {
        return [..source.Select(double.Parse)];
    }

    public static List<bool> ToBoolList(this IEnumerable<string> source)
    {
        return [..source.Select(bool.Parse)];
    }

    public static List<double> ToCharList(this IEnumerable<string> source)
    {
        return [..source.Select(char.Parse)];
    }

    public static List<byte> ToByteList(this IEnumerable<string> source)
    {
        return [..source.Select(byte.Parse)];
    }

    public static List<sbyte> ToSByteList(this IEnumerable<string> source)
    {
        return [..source.Select(sbyte.Parse)];
    }

    public static List<ushort> ToUShortList(this IEnumerable<string> source)
    {
        return [..source.Select(ushort.Parse)];
    }

    public static List<uint> ToUIntList(this IEnumerable<string> source)
    {
        return [..source.Select(uint.Parse)];
    }

    public static List<ulong> ToULongList(this IEnumerable<string> source)
    {
        return [..source.Select(ulong.Parse)];
    }

    public static List<Guid> ToGuidList(this IEnumerable<string> source)
    {
        return [..source.Select(Guid.Parse)];
    }

    public static List<DateTime> ToDateTimeList(this IEnumerable<string> source)
    {
        return [..source.Select(DateTime.Parse)];
    }

#if NET6_0_OR_GREATER
    public static List<DateOnly> ToDateOnlyList(this IEnumerable<string> source)
    {
        return [..source.Select(DateOnly.Parse)];
    }

    public static List<TimeOnly> ToTimeOnlyList(this IEnumerable<string> source)
    {
        return [..source.Select(TimeOnly.Parse)];
    }
#endif

    public static string[] ToStringArray(this IEnumerable<string> source)
    {
        return [..source];
    }

    public static short[] ToShortArray(this IEnumerable<string> source)
    {
        return [..source.Select(short.Parse)];
    }

    public static int[] ToIntArray(this IEnumerable<string> source)
    {
        return [..source.Select(int.Parse)];
    }

    public static long[] ToLongArray(this IEnumerable<string> source)
    {
        return [..source.Select(long.Parse)];
    }

    public static float[] ToFloatArray(this IEnumerable<string> source)
    {
        return [..source.Select(float.Parse)];
    }

    public static decimal[] ToDecimalArray(this IEnumerable<string> source)
    {
        return [..source.Select(decimal.Parse)];
    }

    public static double[] ToDoubleArray(this IEnumerable<string> source)
    {
        return [..source.Select(double.Parse)];
    }

    public static bool[] ToBoolArray(this IEnumerable<string> source)
    {
        return [..source.Select(bool.Parse)];
    }

    public static double[] ToCharArray(this IEnumerable<string> source)
    {
        return [..source.Select(char.Parse)];
    }

    public static byte[] ToByteArray(this IEnumerable<string> source)
    {
        return [..source.Select(byte.Parse)];
    }

    public static sbyte[] ToSByteArray(this IEnumerable<string> source)
    {
        return [..source.Select(sbyte.Parse)];
    }

    public static ushort[] ToUShortArray(this IEnumerable<string> source)
    {
        return [..source.Select(ushort.Parse)];
    }

    public static uint[] ToUIntArray(this IEnumerable<string> source)
    {
        return [..source.Select(uint.Parse)];
    }

    public static ulong[] ToULongArray(this IEnumerable<string> source)
    {
        return [..source.Select(ulong.Parse)];
    }

    public static Guid[] ToGuidArray(this IEnumerable<string> source)
    {
        return [..source.Select(Guid.Parse)];
    }

    public static DateTime[] ToDateTimeArray(this IEnumerable<string> source)
    {
        return [..source.Select(DateTime.Parse)];
    }

#if NET6_0_OR_GREATER
    public static DateOnly[] ToDateOnlyArray(this IEnumerable<string> source)
    {
        return [..source.Select(DateOnly.Parse)];
    }

    public static TimeOnly[] ToTimeOnlyArray(this IEnumerable<string> source)
    {
        return [..source.Select(TimeOnly.Parse)];
    }
#endif
}
