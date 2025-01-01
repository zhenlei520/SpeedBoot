// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.EFCore;

public abstract class SpeedDbContext : DbContext, IDbContext
{
    private IServiceProvider? _currentServiceProvider;
    private bool _isInitialized;

    #region Filter

    private IDataFilter? _dataFilter;

    protected IDataFilter? DataFilter
    {
        get
        {
            TryInitialize();
            return _dataFilter;
        }
    }

    protected virtual bool IsSoftDeleteFilterEnabled => DataFilter?.IsEnabled<ISoftDelete>() ?? false;

    #endregion

    protected IServiceProvider? CurrentServiceProvider
    {
        get
        {
            if (_currentServiceProvider == null)
                TryInitialize();

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingConfigureGlobalFilters(modelBuilder);
    }

    #region Initialize

    protected virtual void TryInitialize()
    {
        if (!_isInitialized)
            Initialize();
    }

    protected virtual void Initialize()
    {
        _currentServiceProvider = GetCurrentServiceProvider();
        _dataFilter = _currentServiceProvider?.GetService<IDataFilter>();
        _isInitialized = true;
    }

    private IServiceProvider? GetCurrentServiceProvider()
    {
        if (_currentServiceProvider != null)
            return _currentServiceProvider;

        var rootServiceProvider = App.Instance.RootServiceProvider;
        SpeedArgumentException.ThrowIfNull(rootServiceProvider);
        return rootServiceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.RequestServices;
    }

    #endregion

    #region Configure Filters

    protected virtual void OnModelCreatingConfigureGlobalFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            ConfigureGlobalFilters(modelBuilder, entityType);
        }
    }

    protected virtual void ConfigureGlobalFilters(ModelBuilder modelBuilder, IMutableEntityType entityType)
    {
        if (entityType.BaseType != null) return;

        var filterExpression = CreateFilterExpression(entityType.ClrType);
        if (filterExpression != null)
        {
            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filterExpression);
        }
    }

    protected virtual LambdaExpression? CreateFilterExpression(Type entityType)
    {
        var parameter = Expression.Parameter(entityType, "entity");
        Expression body = Expression.Constant(true);
        body = CreateSoftDeleteFilterExpression(entityType, body, parameter);
        return Expression.Lambda(body, parameter);
    }

    protected virtual Expression CreateSoftDeleteFilterExpression(Type entityType, Expression body, ParameterExpression parameter)
    {
        if (!typeof(ISoftDelete).IsAssignableFrom(entityType))
            return body;


        var isDeletedProperty = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
        var isDeletedCondition = Expression.Equal(isDeletedProperty, Expression.Constant(false));
        var isSoftDeleteFilterEnabled = Expression.Property(Expression.Constant(this), nameof(IsSoftDeleteFilterEnabled));
        var condition = Expression.OrElse(Expression.Not(isSoftDeleteFilterEnabled), isDeletedCondition);
        body = Expression.AndAlso(body, condition);

        return body;
    }

    #endregion
}
