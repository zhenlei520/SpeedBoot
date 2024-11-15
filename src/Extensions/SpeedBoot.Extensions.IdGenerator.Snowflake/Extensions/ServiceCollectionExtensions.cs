// ReSharper disable once CheckNamespace

namespace SpeedBoot.Extensions.IdGenerator;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSnowflakeIdGenerator(
        this IServiceCollection services,
        Action<SnowflakeIdBuilder>? action = null,
        string? key = null)
    {
        key ??= SnowflakeIdGeneratorConfig.DefaultKey;
        if (!IdGeneratorServiceCollectionRegistry.TryAdd(services, key))
            return services;

        var snowflakeIdBuilder = new SnowflakeIdBuilder();
        action?.Invoke(snowflakeIdBuilder);
        snowflakeIdBuilder.Initialize();

        services.TryAddSingleton<IWorkerProvider>(sp => new DefaultWorkerProvider(snowflakeIdBuilder.Options!.WorkerId!.Value));
        services.AddSingleton<IIdGenerator<long>, SnowflakeIdGenerator>();
        return services;
    }
}
