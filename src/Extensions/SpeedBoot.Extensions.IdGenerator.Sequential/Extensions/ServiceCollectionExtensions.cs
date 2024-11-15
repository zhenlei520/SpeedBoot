// ReSharper disable once CheckNamespace

namespace SpeedBoot.Extensions.IdGenerator;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSequentialIdGenerator(
        this IServiceCollection services,
        SequentialGuidType sequentialGuidType,
        string? key = null)
    {
        key ??= SequentialIdGeneratorConfig.DefaultKey;
        if (!IdGeneratorServiceCollectionRegistry.TryAdd(services, key))
            return services;

        services.AddSingleton<IIdGenerator<Guid>, IIdGenerator>();
        services.AddSingleton<IIdGenerator>(_ => new SequentialIdGenerator(sequentialGuidType, key));
        return services;
    }
}
