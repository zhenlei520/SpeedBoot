// ReSharper disable once CheckNamespace

namespace SpeedBoot.Extensions.IdGenerator;

public static class SpeedBootApplicationExternalExtensions
{
    public static IIdGenerator GetSequentialIdGenerator(this App app)
        => app.GetRequiredSingletonService<IIdGenerator>(nameof(SequentialIdGenerator));
}
