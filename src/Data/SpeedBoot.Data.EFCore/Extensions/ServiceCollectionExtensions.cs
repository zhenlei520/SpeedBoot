// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加仓储，仅需要添加默认 DbContext的上下文即可
    /// </summary>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <typeparam name="TDbContext"></typeparam>
    /// <returns></returns>
    public static IServiceCollection AddSpeedDbContext<TDbContext>(
        this IServiceCollection services,
        Action<SpeedDbContextOptionsBuilder<TDbContext>>? optionsAction = null)
        where TDbContext : SpeedDbContext, IDbContext
    {
        GlobalDataConfig.RegisterDbContext<TDbContext>();
        services.TryAddScoped<IUnitOfWork, DefaultUnitOfWork>();
        services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.TryAddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.TryAddScoped(typeof(IDbContext), serviceProvider => serviceProvider.GetRequiredService(typeof(TDbContext)));
        services.TryAddScoped<IConnectionStringProvider, DefaultConnectionStringProvider>();
        services.TryAddScoped<UnitOfWorkWrapper>();

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

        var speedDbContextOptionsBuilder = new SpeedDbContextOptionsBuilder<TDbContext>(services);
        optionsAction?.Invoke(speedDbContextOptionsBuilder);
        services.AddDbContext<TDbContext>((serviceProvider, builder) =>
        {
            speedDbContextOptionsBuilder.OptionsAction?.Invoke(serviceProvider, builder);
        });
        return services;
    }
}
