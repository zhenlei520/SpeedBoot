// ReSharper disable once CheckNamespace

namespace SpeedBoot.Extensions.IdGenerator;

public static class SpeedBootApplicationExternalExtensions
{
    public static IIdGenerator GetIdGenerator(this SpeedBootApplicationBuilder applicationBuilder)
        => applicationBuilder.GetRequiredSingletonService<IIdGenerator>();
}