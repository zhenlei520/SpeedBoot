// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.EFCore;

public abstract class SpeedDbContext : DbContext, IDbContext
{
    private IServiceProvider? _currentServiceProvider;
    private bool _isInitialized;
    protected IServiceProvider? CurrentServiceProvider
    {
        get
        {
            if (_isInitialized)
                return _currentServiceProvider;

            if (_currentServiceProvider == null)
            {
                var rootServiceProvider = App.Instance.RootServiceProvider;
                SpeedArgumentException.ThrowIfNull(rootServiceProvider);
                _currentServiceProvider = rootServiceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.RequestServices;
            }

            _isInitialized = true;
            return _currentServiceProvider;
        }
    }

    /// <summary>
    /// The interceptor may not take effect when currentServiceProvider is null
    /// </summary>
    /// <param name="currentServiceProvider"></param>
    public SpeedDbContext(IServiceProvider? currentServiceProvider = null)
    {
        _currentServiceProvider = currentServiceProvider;
    }

    /// <summary>
    /// The interceptor may not take effect when currentServiceProvider is null
    /// </summary>
    public SpeedDbContext(DbContextOptions options, IServiceProvider? currentServiceProvider = null) : base(options)
    {
        _currentServiceProvider = currentServiceProvider;
    }

    /// <summary>
    /// The interceptor may not take effect when currentServiceProvider is null
    /// </summary>
    public SpeedDbContext(DbContextOptions<SpeedDbContext> options, IServiceProvider? currentServiceProvider = null) : base(options)
    {
        _currentServiceProvider = currentServiceProvider;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (CurrentServiceProvider != null)
        {
            // When a non-Http request comes in and the constructor does not inject IServiceProvider, there may be an interceptor that is not supported.
#if NET5_0_OR_GREATER
            optionsBuilder.AddInterceptors(new EFCoreSaveChangesInterceptor(CurrentServiceProvider));
#endif
#if NETCOREAPP3_0_OR_GREATER
            optionsBuilder.AddInterceptors(new EFCoreDbTransactionInterceptor(CurrentServiceProvider));
#endif
        }
        base.OnConfiguring(optionsBuilder);
    }
}
