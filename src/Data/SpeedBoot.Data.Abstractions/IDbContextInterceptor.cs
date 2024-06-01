// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.Abstractions;

public interface IDbContextInterceptor : IOrder
{
    void SaveSucceed(SaveSucceedDbContextEventData eventData);

    Task SaveSucceedAsync(SaveSucceedDbContextEventData eventData, CancellationToken cancellationToken);

    void SaveFailed(SaveFailedDbContextEventData eventData);

    Task SaveFailedAsync(SaveFailedDbContextEventData eventData, CancellationToken cancellationToken);
}
