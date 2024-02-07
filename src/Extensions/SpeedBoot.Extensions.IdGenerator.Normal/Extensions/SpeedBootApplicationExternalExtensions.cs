// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public static class SpeedBootApplicationExternalExtensions
{
    public static IIdGenerator GetNormalIdGenerator(this App app)
        => app.GetRequiredSingletonService<IIdGenerator>(nameof(NormalIdGenerator));
}
