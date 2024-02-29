// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

public class LazyService<TService> : ILazyService<TService>
    where TService : class
{
    private readonly Lazy<TService>? _lazyService;

    private bool _initialized;
    private TService? _service;

    public TService? Service
    {
        get
        {
            if (!_initialized)
            {
                _service = _lazyService?.Value;
            }

            return _service;
        }
    }

    public TService RequiredService
    {
        get
        {
            var service = Service;
            SpeedArgumentException.ThrowIfNull(service);
            return service!;
        }
    }

    public LazyService(Lazy<TService>? lazyService = null)
    {
        _lazyService = lazyService;
    }
}
