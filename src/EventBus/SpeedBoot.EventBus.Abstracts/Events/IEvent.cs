// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Abstracts;

public interface IEvent
{
    public string GetId();

    public void SetId(string id);

    public DateTime GetCreationTime();

    public DateTime SetCreationTime(DateTime creationTime);
}
