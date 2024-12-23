// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class Event : IEvent
{
    private string _id = Guid.NewGuid().ToString();

    private DateTime _createTime = DateTime.UtcNow;

    public virtual string GetId() => _id;

    public virtual void SetId(string id) => _id = id;

    public virtual DateTime GetCreationTime() => _createTime;

    public virtual DateTime SetCreationTime(DateTime creationTime) => _createTime = creationTime;
}

public abstract class Event<TResponse> : Event, IEvent<TResponse>
{
    public virtual TResponse? Result { get; set; }
}
