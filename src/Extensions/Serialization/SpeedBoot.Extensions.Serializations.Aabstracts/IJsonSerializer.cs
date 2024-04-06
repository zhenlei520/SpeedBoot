// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.Serializations;

public interface IJsonSerializer
{
    string Serialize<T>(T obj);

    T? Deserialize<T>(string json);
}
