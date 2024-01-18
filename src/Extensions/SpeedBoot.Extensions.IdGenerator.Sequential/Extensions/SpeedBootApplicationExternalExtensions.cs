// ReSharper disable once CheckNamespace

namespace SpeedBoot.Extensions.IdGenerator;

public static class SpeedBootApplicationExternalExtensions
{
    public static IIdGenerator GetIdGenerator(this App app)
        => app.GetRequiredSingletonService<IIdGenerator>();
}
