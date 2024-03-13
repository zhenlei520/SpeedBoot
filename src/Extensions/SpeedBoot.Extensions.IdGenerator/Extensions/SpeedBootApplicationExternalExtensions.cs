// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Extensions.IdGenerator;

public static class SpeedBootApplicationExternalExtensions
{
    public static IIdGenerator? GetIdGenerator(this App app)
        => app.GetSingletonService<IIdGenerator>(true);

    public static IIdGenerator GetRequiredIdGenerator(this App app)
        => app.GetRequiredSingletonService<IIdGenerator>(true);

    public static Guid GeneratorGuid(this App app)
        => app.GetRequiredIdGenerator().Create();
}
