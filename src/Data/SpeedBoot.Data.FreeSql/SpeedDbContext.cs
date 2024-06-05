// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql;

public abstract class SpeedDbContext : DbContext, IDbContext
{
    private IServiceProvider? _currentServiceProvider;
    private bool _isInitialized;

    protected bool EnabledInterceptor { get; }

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
    /// 使用此构造函数，则必须重写 OnConfiguring 方法
    /// </summary>
    protected SpeedDbContext() : base()
    {
    }

    /// <summary>
    /// 使用此构造函数，则必须重写 OnConfiguring 方法
    /// </summary>
    protected SpeedDbContext(IServiceProvider currentServiceProvider) : base()
    {
        _currentServiceProvider = currentServiceProvider;
    }

    protected SpeedDbContext(IFreeSql freeSql)
        : this(freeSql, new DbContextOptions())
    {
    }

    protected SpeedDbContext(IFreeSql freeSql, DbContextOptions dbContextOptions)
        : base(freeSql, dbContextOptions)
    {
        OnEntityChange(dbContextOptions);
        CurdAfter(freeSql);
    }

    protected SpeedDbContext(IFreeSql freeSql, IServiceProvider currentServiceProvider)
        : this(freeSql, new DbContextOptions(), currentServiceProvider)
    {
    }

    protected SpeedDbContext(IFreeSql freeSql, DbContextOptions dbContextOptions, IServiceProvider currentServiceProvider)
        : base(freeSql, dbContextOptions)
    {
        _currentServiceProvider = currentServiceProvider;
        OnEntityChange(dbContextOptions);
        CurdAfter(freeSql);
    }

    protected virtual void OnEntityChange(DbContextOptions dbContextOptions)
    {
        if (!GetEnabledInterceptor())
            return;

        dbContextOptions.OnEntityChange = report =>
        {
            var auditInterceptor = CurrentServiceProvider!.GetRequiredService<AuditInterceptor>();
            report.ForEach(changeInfo => auditInterceptor.SetEntity(changeInfo));
        };
    }

    protected virtual void CurdAfter(IFreeSql freeSql)
    {
        if (!GetEnabledInterceptor())
            return;

        freeSql.Aop.CurdAfter += (obj, args) =>
        {
            if (CurrentServiceProvider != null)
            {
                SaveChangesInterceptor.CurdAfter(freeSql, CurrentServiceProvider, args);
            }
        };
    }

    private bool GetEnabledInterceptor() => EnabledInterceptor && CurrentServiceProvider != null;
}
