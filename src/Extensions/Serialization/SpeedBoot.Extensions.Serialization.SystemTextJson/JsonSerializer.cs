// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.Serialization.SystemTextJson;

public class JsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions? _jsonSerializerOptions;

    public JsonSerializer(JsonSerializerOptions? jsonSerializerOptions = null)
    {
        _jsonSerializerOptions = jsonSerializerOptions;
    }

    public string Serialize<T>(T obj)
    {
        return MsJsonSerializer.Serialize(obj, _jsonSerializerOptions);
    }

    public T? Deserialize<T>(string json)
    {
        return MsJsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
    }
}
