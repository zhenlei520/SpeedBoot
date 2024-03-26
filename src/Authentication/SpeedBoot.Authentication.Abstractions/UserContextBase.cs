// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Authentication.Abstractions;

public abstract class UserContextBase<TIdentityUser, TUserId> : IUserContext<TIdentityUser, TUserId>, IUserSetter<TIdentityUser, TUserId>
    where TIdentityUser : class, IIdentityUser<TUserId>, new()
    where TUserId : struct, IComparable, IEquatable<TUserId>
{
    public bool IsAuthenticated { get; }

    public TUserId? UserId => User?.Id;

    private bool _isInitialized;
    private TIdentityUser? _currentUser;

    public TIdentityUser? User
    {
        get
        {
            if (_isInitialized)
                return _currentUser;

            _currentUser = GetUser();
            _isInitialized = true;
            return _currentUser;
        }
    }

    protected abstract TIdentityUser? GetUser();

    public IDisposable Change(TIdentityUser? identityUser)
    {
        var lastUser = _currentUser;
        _currentUser = identityUser;
        return new DisposeAction(() => _currentUser = lastUser);
    }
}

public abstract class UserContextBase<TIdentityUser> : UserContextBase<TIdentityUser, Guid>
    where TIdentityUser : class, IIdentityUser<Guid>, new()
{
}
