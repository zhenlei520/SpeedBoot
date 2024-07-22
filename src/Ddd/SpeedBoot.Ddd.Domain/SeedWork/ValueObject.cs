// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Ddd.Domain.SeedWork;

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityValues();

    public override bool Equals(object? obj)
    {
        if (this is null ^ obj is null) return false;

        if (obj is ValueObject entity)
        {
            return entity.GetEqualityValues().SequenceEqual(GetEqualityValues());
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
#if NETSTANDARD2_0
        unchecked
        {
            return GetEqualityValues().Aggregate(17, (current, value) => current * 31 + (value?.GetHashCode() ?? 0));
        }
#else
        return GetEqualityValues().Aggregate(0, (hashCode, next) => HashCode.Combine(hashCode, next));
#endif
    }

    public static bool operator ==(ValueObject x, ValueObject y)
    {
        return x.Equals(y);
    }

    public static bool operator !=(ValueObject x, ValueObject y)
    {
        return !x.Equals(y);
    }
}
