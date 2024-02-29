// ReSharper disable once CheckNamespace

namespace SpeedBoot.Extensions.IdGenerator;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSequentialIdGenerator(
        this IServiceCollection services,
        SequentialGuidType sequentialGuidType,
        string? key = null)
    {
        if (!ServiceCollectionUtils.TryAdd<SequentialIdGeneratorProvider>(services))
            return services;

        services.AddSingleton<IIdGenerator>(_ => new SequentialIdGenerator(sequentialGuidType, key));
        return services;
    }

    private sealed class SequentialIdGeneratorProvider
    {
    }
}
