// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Extensions.IdGenerator;

public static class SpeedBootApplicationExternalExtensions
{
    public static IIdGenerator GetIdGenerator(this SpeedBootApplicationExternal speedBootApplicationExternal)
        => speedBootApplicationExternal.GetRequiredRootServiceProvider().GetRequiredService<IIdGenerator>();

    public static Guid GeneratorId(this SpeedBootApplicationExternal speedBootApplicationExternal)
        => speedBootApplicationExternal.GetIdGenerator().Create();
}
