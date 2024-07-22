// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public static class SpeedBootApplicationExternalExtensions
{
    public static IIdGenerator? GetIdGenerator(this App app)
        => app.GetSingletonServices<IIdGenerator>(true).FirstOrDefault();

    public static IIdGenerator? GetIdGenerator(this App app, string key)
        => app.GetSingletonService<IKeydSingletonService<IIdGenerator>>(true)?.GetService(key);

    public static IIdGenerator GetRequiredIdGenerator(this App app)
        => app.GetSingletonServices<IIdGenerator>(true).First();

    public static IIdGenerator GetRequiredIdGenerator(this App app, string key)
        => app.GetRequiredSingletonService<IKeydSingletonService<IIdGenerator>>(true).GetRequiredService(key);

    public static Guid GeneratorGuid(this App app)
        => app.GetRequiredIdGenerator().Create();
}
