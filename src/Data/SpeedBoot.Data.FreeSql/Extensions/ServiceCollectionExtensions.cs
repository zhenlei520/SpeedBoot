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

        SpeedDbContextHelper.Register<TDbContext>();
        var speedDbContextOptionsBuilder = new SpeedDbContextOptionsBuilder(typeof(TDbContext));
        optionsAction?.Invoke(speedDbContextOptionsBuilder);
        services.TryAddScoped<TDbContext>(serviceProvider =>
        {
            var freeSqlBuilder = new FreeSqlBuilder();
            var constructorType = SpeedDbContextHelper.GetConstructorType<TDbContext>();
            if (constructorType == ConstructorType.Zero)
            {
                return SpeedDbContextHelper.CreateInstance<TDbContext>();
            }

            speedDbContextOptionsBuilder.OptionsAction?.Invoke(serviceProvider, freeSqlBuilder);
            var freeSql = freeSqlBuilder.Build();
            speedDbContextOptionsBuilder.FreeSqlOptionsAction?.Invoke(freeSql);
            switch (constructorType)
            {
                case ConstructorType.One:
                    return SpeedDbContextHelper.CreateInstance<TDbContext>(freeSql);
                case ConstructorType.OneWithServiceProvider:
                    return SpeedDbContextHelper.CreateInstance<TDbContext>(freeSql, serviceProvider);
            }

            var dbContextOptions = new DbContextOptions();
            speedDbContextOptionsBuilder.DbContextOptionsAction?.Invoke(dbContextOptions);
            return constructorType switch
            {
                ConstructorType.Two => SpeedDbContextHelper.CreateInstance<TDbContext>(freeSql, dbContextOptions),
                ConstructorType.TwoWithServiceProvider => SpeedDbContextHelper.CreateInstance<TDbContext>(freeSql, serviceProvider),
                ConstructorType.Three => SpeedDbContextHelper.CreateInstance<TDbContext>(freeSql, dbContextOptions, serviceProvider),
                _ => throw new NotSupportedException()
            };
        });

        return services;
    }
}
