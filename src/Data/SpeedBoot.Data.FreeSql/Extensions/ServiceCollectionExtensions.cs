// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpeedDbContext<TDbContext>(
        this IServiceCollection services,
        Action<SpeedDbContextOptionsBuilder>? optionsAction = null)
        where TDbContext : SpeedDbContext, IDbContext //, new()
    {
        GlobalDataConfig.RegisterDbContext<TDbContext>();
        services.TryAddScoped<SpeedBoot.Data.Abstractions.IUnitOfWork, DefaultUnitOfWork>();
        services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.TryAddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.TryAddScoped(typeof(IDbContext), serviceProvider => serviceProvider.GetRequiredService(typeof(TDbContext)));
        services.TryAddScoped<IConnectionStringProvider, DefaultConnectionStringProvider>();
        services.TryAddScoped<ITableRelationProvider, DefaultTableRelationProvider>();
        services.AddSpeedDbContextCore();

        var configuration = App.Instance.GetConfiguration(true);
        if (configuration != null)
        {
            services.Configure<ConnectionStrings>(Options.Options.DefaultName, connectionString =>
            {
                var configurationSection = configuration.GetSection(GlobalDataConfig.ConnectionString.DefaultSection);
                configurationSection.Bind(connectionString);
            });
        }

        SpeedDbContextHelper<TDbContext>.Register();
        var speedDbContextOptionsBuilder = new SpeedDbContextOptionsBuilder(typeof(TDbContext));
        optionsAction?.Invoke(speedDbContextOptionsBuilder);
        services.TryAddScoped<TDbContext>(serviceProvider => SpeedDbContextHelper<TDbContext>.Execute(serviceProvider, speedDbContextOptionsBuilder));
        services.TryAddScoped<FreeSqlAuditInterceptor>();
        services.TryAddScoped<FreeSqlSaveChangesInterceptor>();
        return services;
    }
}
