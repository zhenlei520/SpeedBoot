// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.IdGenerator.Abstracts;

public interface IIdGenerator<T> : IService
    where T : struct
{
    T Create();
}
