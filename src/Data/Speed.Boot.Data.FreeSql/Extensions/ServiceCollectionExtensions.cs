// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpeedDbContext<TDbContext>(
        this IServiceCollection services,
        Action<SpeedDbContextOptionsBuilder>? optionsAction = null)
        where TDbContext : SpeedDbContext, IDbContext//, new()
    {
        services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.TryAddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.TryAddScoped(typeof(IDbContext), serviceProvider => serviceProvider.GetRequiredService(typeof(TDbContext)));
        services.TryAddScoped<IConnectionStringProvider, DefaultConnectionStringProvider>();
        services.AddSpeedDbContextCore();

        var configuration = App.ApplicationExternal.GetConfiguration();
        if (configuration != null)
        {
            services.Configure<ConnectionStrings>(Options.Options.DefaultName, connectionString =>
            {
                var configurationSection = configuration.GetSection(ConnectionStrings.DEFAULT_SECTION);
                configurationSection.Bind(connectionString);
            });
        }

        // SpeedDbContextHelper.Register<TDbContext>();
        var speedDbContextOptionsBuilder = new SpeedDbContextOptionsBuilder(typeof(TDbContext));
        optionsAction?.Invoke(speedDbContextOptionsBuilder);
        services.TryAddScoped<TDbContext>(serviceProvider =>
        {
            var freeSqlBuilder = new FreeSqlBuilder();
            speedDbContextOptionsBuilder.OptionsAction?.Invoke(serviceProvider, freeSqlBuilder);
            var freeSql = freeSqlBuilder.Build();
            var speedDbContext = SpeedDbContextHelper.CreateInstance<TDbContext>(freeSql, new DbContextOptions());

            // var dbContext = new TDbContext();
            // SpeedDbContextHelper.SetDbContext(dbContext, freeSqlBuilder.Build());
            // var speedDbContext= (dbContext as TDbContext)!;
            // // speedDbContext.SetOnConfiguring();
            return speedDbContext;
        });

        return services;
    }
}
