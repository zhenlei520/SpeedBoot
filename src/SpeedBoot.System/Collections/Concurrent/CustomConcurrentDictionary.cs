// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.System.Collections.Concurrent;

public class CustomConcurrentDictionary<TKey, TValue> : IDisposable where TKey : notnull
{
    private readonly ConcurrentDictionary<TKey, Lazy<TValue>> _dicCache;

    public ICollection<TKey> Keys => _dicCache.Keys;

    public IEnumerable<TValue> Values => _dicCache.Values.Select(value => value.Value);

    public TValue? this[TKey key] => _dicCache.TryGetValue(key, out var lazyValue) ? lazyValue.Value : default;

    private readonly LazyThreadSafetyMode _defaultLazyThreadSafetyMode;

    public CustomConcurrentDictionary() : this(LazyThreadSafetyMode.ExecutionAndPublication)
    {
    }

    public CustomConcurrentDictionary(IEqualityComparer<TKey>? comparer) : this(LazyThreadSafetyMode.ExecutionAndPublication, comparer)
    {
    }

    public CustomConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer) : this(
        collection, LazyThreadSafetyMode.ExecutionAndPublication, comparer)
    {
    }

    public CustomConcurrentDictionary(LazyThreadSafetyMode lazyThreadSafetyMode, IEqualityComparer<TKey>? comparer = null)
    {
        _defaultLazyThreadSafetyMode = lazyThreadSafetyMode;
        _dicCache = comparer != null
            ? new ConcurrentDictionary<TKey, Lazy<TValue>>(comparer)
            : new ConcurrentDictionary<TKey, Lazy<TValue>>();
    }

    public CustomConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection,
        LazyThreadSafetyMode lazyThreadSafetyMode = LazyThreadSafetyMode.ExecutionAndPublication, IEqualityComparer<TKey>? comparer = null)
    {
        _defaultLazyThreadSafetyMode = lazyThreadSafetyMode;
        var data = collection.Select(kv =>
            new KeyValuePair<TKey, Lazy<TValue>>(kv.Key, new Lazy<TValue>(() => kv.Value, lazyThreadSafetyMode)));
        _dicCache = comparer != null
            ? new ConcurrentDictionary<TKey, Lazy<TValue>>(data, comparer)
            : new ConcurrentDictionary<TKey, Lazy<TValue>>(data);
    }

    public TValue? Get(TKey key)
    {
        return this[key];
    }

    public bool TryGet(TKey key,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out TValue? value)
    {
        var result = _dicCache.TryGetValue(key, out var lazyValue);

        if (result)
        {
            value = lazyValue == null ? default : lazyValue.Value;
        }
        else
        {
            value = default;
        }

        return result;
    }

    public bool TryAdd(TKey key, Func<TKey, TValue> valueFactory)
        => TryAdd(key, valueFactory, _defaultLazyThreadSafetyMode);

    public bool TryAdd(TKey key, Func<TKey, TValue> valueFactory, LazyThreadSafetyMode lazyThreadSafetyMode)
    {
        return _dicCache.TryAdd(key, new Lazy<TValue>(() => valueFactory(key), lazyThreadSafetyMode));
    }

    public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        => GetOrAdd(key, valueFactory, _defaultLazyThreadSafetyMode);

    public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory, LazyThreadSafetyMode lazyThreadSafetyMode)
    {
        if (!_dicCache.TryGetValue(key, out var lazyValue))
        {
            lazyValue = _dicCache.GetOrAdd(key, k => new Lazy<TValue>(() => valueFactory(k), lazyThreadSafetyMode));
        }

        return lazyValue.Value;
    }

    public bool TryUpdate(TKey key, Func<TKey, TValue> valueFactory, TValue comparisonValue)
        => TryUpdate(key, valueFactory, comparisonValue, _defaultLazyThreadSafetyMode);

    public bool TryUpdate(TKey key, Func<TKey, TValue> valueFactory, TValue comparisonValue, LazyThreadSafetyMode lazyThreadSafetyMode)
    {
        return _dicCache.TryUpdate(
            key,
            new Lazy<TValue>(() => valueFactory(key), lazyThreadSafetyMode),
            new Lazy<TValue>(() => comparisonValue, lazyThreadSafetyMode)
        );
    }

    public TValue AddOrUpdate(TKey key, Func<TKey, TValue> valueFactory)
        => AddOrUpdate(key, valueFactory, _defaultLazyThreadSafetyMode);

    public TValue AddOrUpdate(TKey key, Func<TKey, TValue> valueFactory, LazyThreadSafetyMode lazyThreadSafetyMode)
    {
        return _dicCache.AddOrUpdate
        (
            key,
            k => new Lazy<TValue>(() => valueFactory(k), lazyThreadSafetyMode),
            (oldkey, _) => new Lazy<TValue>(() => valueFactory(oldkey), lazyThreadSafetyMode)
        ).Value;
    }

    public bool Remove(TKey key)
        => TryRemove(key, out _);

    public bool TryRemove(TKey key,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out TValue? value)
    {
        if (_dicCache.TryRemove(key, out var val))
        {
            value = val.Value;
            return true;
        }

        value = default;
        return false;
    }

    public bool ContainsKey(TKey key)
    {
        return _dicCache.ContainsKey(key);
    }

    public void Clear()
    {
        _dicCache.Clear();
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> GetEnumerable()
        => _dicCache.Select(x=> new KeyValuePair<TKey, TValue>(x.Key, x.Value.Value));

    public void Dispose()
    {
        _dicCache.Clear();
    }
}
