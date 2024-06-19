// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.Serialization.SystemTextJson;

public class JsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions? _jsonSerializerOptions;

    public string Key { get; private set; }

    public JsonSerializer(string key, JsonSerializerOptions? jsonSerializerOptions)
    {
        Key = key;
        _jsonSerializerOptions = jsonSerializerOptions;
    }

    public string Serialize<T>(T obj)
    {
        if (_jsonSerializerOptions != null)
            return MsJsonSerializer.Serialize(obj, _jsonSerializerOptions);

        return MsJsonSerializer.Serialize(obj);
    }

    public T? Deserialize<T>(string json)
    {
        if (_jsonSerializerOptions != null)
            return MsJsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);

        return MsJsonSerializer.Deserialize<T>(json);
    }
}
