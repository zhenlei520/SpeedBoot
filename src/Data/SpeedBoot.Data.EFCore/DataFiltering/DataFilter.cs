// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.EFCore.DataFiltering;

public class DataFilter : IDataFilter
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CustomConcurrentDictionary<Type, object> _cache;

    public DataFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _cache = new();
    }

    public IDisposable Enable<TFilter>() where TFilter : class
        => GetFilter<TFilter>().Enable();

    public IDisposable Disable<TFilter>() where TFilter : class
        => GetFilter<TFilter>().Disable();

    public bool IsEnabled<TFilter>() where TFilter : class
        => GetFilter<TFilter>().Enabled;

    private DataFilter<TFilter> GetFilter<TFilter>()
        where TFilter : class
    {
        return (_cache.GetOrAdd(
            typeof(TFilter),
            _ => _serviceProvider.GetRequiredService(typeof(DataFilter<TFilter>))
        ) as DataFilter<TFilter>)!;//todo: Waiting for testing
    }
}

public class DataFilter<TFilter> where TFilter : class
{
    private readonly AsyncLocal<DataFilterState> _filter;

    public DataFilter() => _filter = new AsyncLocal<DataFilterState>();

    public bool Enabled
    {
        get
        {
            _filter.Value ??= new DataFilterState(true);

            return _filter.Value!.Enabled;
        }
    }

    public IDisposable Enable()
    {
        if (Enabled)
            return NullDisposable.Instance;

        _filter.Value!.Enabled = true;

        return new DisposeAction(() => Disable());
    }

    public IDisposable Disable()
    {
        if (!Enabled)
            return NullDisposable.Instance;

        _filter.Value!.Enabled = false;

        return new DisposeAction(() => Enable());
    }
}
