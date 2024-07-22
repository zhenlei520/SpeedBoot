// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local.Tests.EventHandlers;

public class UserHandler
{
    private readonly ILogger? _logger;

    public UserHandler(ILogger? logger = null)
    {
        _logger = logger;
    }

    [RecordTimeActionInterceptor]
    [LocalEventHandler(2)]
    public void AddRegisterUserEvent(RegisterUserEvent registerUserEvent)
    {
        _logger?.LogInformation($"AddRegisterUserEvent: {registerUserEvent.Name}");
    }

    [RecordTimeActionInterceptor]
    [LocalEventHandler(1)]
    public void SendEmail(RegisterUserEvent registerUserEvent)
    {
        _logger?.LogInformation("SendEmail: {UserName}", registerUserEvent.Name);
    }

    [LocalEventHandler(2)]
    public void SendCoupon(RegisterUserEvent registerUserEvent)
    {
        throw new SpeedFriendlyException("custom exception");

        _logger?.LogInformation("SendCoupon: {UserName}", registerUserEvent.Name);
    }

    [LocalEventHandler(1, IsCancel = true)]
    public void TrySendCoupon(RegisterUserEvent registerUserEvent)
    {
        _logger?.LogInformation("TrySendCoupon: {UserName}", registerUserEvent.Name);
    }

    [LocalEventHandler(0, IsCancel = true)]
    public void AddSendCouponFailed(RegisterUserEvent registerUserEvent)
    {
        _logger?.LogInformation("TrySendCouponFailed: {UserName}", registerUserEvent.Name);
    }
}
