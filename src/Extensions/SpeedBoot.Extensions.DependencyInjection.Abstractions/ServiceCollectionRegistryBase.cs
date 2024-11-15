// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.DependencyInjection.Abstractions;

public abstract class ServiceCollectionRegistryBase<TService> where TService : IService
{
    private static Dictionary<IntPtr, List<string>> _keyValuePairs = new();

    public static bool TryAdd(IServiceCollection services, string key)
    {
        if (ContainsKey(services, key, out var keys, out var address))
            return false;

        if (keys == null)
        {
            keys = new List<string>();
            _keyValuePairs.Add(address, keys);
        }

        keys.Add(key);
        return true;
    }

    public static bool TryRemove(IServiceCollection services, string key)
    {
        if (!ContainsKey(services, key, out var keys, out _))
            return false;

        keys.Remove(key);
        return true;
    }

    public static bool TryRemove(IServiceCollection services)
    {
        var address = GetObjectAddress(services);
        _keyValuePairs.Remove(address);
        return true;
    }

    private static bool ContainsKey(
        IServiceCollection services, object key,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out List<string>? keys, out IntPtr address)
    {
        address = GetObjectAddress(services);
        return _keyValuePairs.TryGetValue(address, out keys) && keys.Contains(key);
    }

    private static unsafe IntPtr GetObjectAddress(object services)
    {
        TypedReference reference = __makeref(services);
        return **(IntPtr**)&reference;
    }
}
