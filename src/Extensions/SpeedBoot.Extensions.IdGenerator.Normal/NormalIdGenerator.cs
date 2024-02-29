// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Extensions.IdGenerator;

public class NormalIdGenerator : IIdGenerator
{
    public string Key { get; private set; }

    public Guid Create() => Guid.NewGuid();

    public NormalIdGenerator(string? key = null)
    {
        Key = key ?? nameof(NormalIdGenerator);
    }
}
