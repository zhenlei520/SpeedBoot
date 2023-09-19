// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.EntityFrameworkCore;

public class SpeedDbContextOptions : DbContextOptions
{
    public readonly IServiceProvider? ServiceProvider;
    protected Type DbContextType { get; }

    protected readonly DbContextOptions OriginOptions;

#if NET6_0_OR_GREATER
private protected SpeedDbContextOptions(
        IServiceProvider? serviceProvider,
        Type dbContextType,
        DbContextOptions originOptions) : base()
    {
        ServiceProvider = serviceProvider;
        DbContextType = dbContextType;

        OriginOptions = originOptions;
    }
#else
    private protected SpeedDbContextOptions(
        IServiceProvider? serviceProvider,
        Type dbContextType,
        DbContextOptions originOptions) : base(new Dictionary<Type, IDbContextOptionsExtension>())
    {
        ServiceProvider = serviceProvider;
        DbContextType = dbContextType;

        OriginOptions = originOptions;
    }
#endif


    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override Type ContextType => OriginOptions.ContextType;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override bool IsFrozen => OriginOptions.IsFrozen;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override IEnumerable<IDbContextOptionsExtension> Extensions => OriginOptions.Extensions;

#if NET6_0_OR_GREATER
    protected override ImmutableSortedDictionary<Type, (IDbContextOptionsExtension Extension, int Ordinal)> ExtensionsMap
        => OriginOptions.GetExtensionsMap();
#endif

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="TExtension"></typeparam>
    /// <param name="extension"></param>
    /// <returns></returns>
    public override DbContextOptions WithExtension<TExtension>(TExtension extension)
        => OriginOptions.WithExtension(extension);

    public override TExtension? FindExtension<TExtension>() where TExtension : class
        => OriginOptions.FindExtension<TExtension>();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void Freeze() => OriginOptions.Freeze();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="TExtension"></typeparam>
    /// <returns></returns>
    public override TExtension GetExtension<TExtension>()
        => OriginOptions.GetExtension<TExtension>();

#if NET6_0_OR_GREATER
    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        foreach (var dbContextOptionsExtension in ExtensionsMap)
        {
            hashCode.Add(dbContextOptionsExtension.Key);
            hashCode.Add(dbContextOptionsExtension.Value.Extension.Info.GetServiceProviderHashCode());
        }

        return hashCode.ToHashCode();
    }
#endif

    public override bool Equals(object? obj)
        => ReferenceEquals(this, obj)
            || (obj is DbContextOptions otherOptions && Equals(otherOptions));
}
