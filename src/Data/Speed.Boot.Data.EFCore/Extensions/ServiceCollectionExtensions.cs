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
        Action<SpeedDbContextOptionsBuilder>? optionsAction = null)
        where TDbContext : DbContext, IDbContext
    {
        services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.TryAddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.TryAddScoped(typeof(IDbContext), typeof(TDbContext));
        services.TryAddScoped<IConnectionStringProvider, DefaultConnectionStringProvider>();

        //todo: 需要注册 IOptionsSnapshot<ConnectionStrings>
        var configuration = App.ApplicationExternal.GetConfiguration();
        services.Configure<ConnectionStrings>(Options.Options.DefaultName, connectionString =>
        {
            var configurationSection = configuration.GetSection(ConnectionStrings.DEFAULT_SECTION);
            configurationSection.Bind(connectionString);
        });

        var speedDbContextOptionsBuilder = new SpeedDbContextOptionsBuilder();
        optionsAction?.Invoke(speedDbContextOptionsBuilder);
        services.AddDbContext<TDbContext>((serviceProvider, builder) =>
        {
            speedDbContextOptionsBuilder.OptionsAction?.Invoke(serviceProvider, builder);
        });
        return services;
    }
}
