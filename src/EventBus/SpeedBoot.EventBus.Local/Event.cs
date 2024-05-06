// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class Event : IEvent
{
    private string _id = Guid.NewGuid().ToString();

    private DateTime _createTime = DateTime.UtcNow;

    public string GetId() => _id;

    public void SetId(string id) => _id = id;

    public DateTime GetCreationTime() => _createTime;

    public DateTime SetCreationTime(DateTime creationTime) => _createTime = creationTime;
}

public abstract class Event<TResponse> : Event, IEvent<TResponse>
{
    public TResponse? Result { get; set; }
}
