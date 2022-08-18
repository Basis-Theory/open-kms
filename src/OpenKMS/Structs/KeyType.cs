using OpenKMS.Constants;
using OpenKMS.Models;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace OpenKMS.Structs;

/// <summary>
/// <see cref="JsonWebKey"/> key types.
/// </summary>
public readonly struct KeyType : IEquatable<KeyType>
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyType"/> structure.
    /// </summary>
    /// <param name="value">The string value of the instance.</param>
    public KeyType(string value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// An Elliptic Curve Cryptographic (ECC) algorithm.
    /// </summary>
    public static KeyType EC { get; } = new KeyType(KeyTypes.Ec);

    /// <summary>
    /// An RSA cryptographic algorithm.
    /// </summary>
    public static KeyType RSA { get; } = new KeyType(KeyTypes.Rsa);

    /// <summary>
    /// An AES cryptographic algorithm.
    /// </summary>
    public static KeyType OCT { get; } = new KeyType(KeyTypes.Oct);

    /// <summary>
    /// Determines if two <see cref="KeyType"/> values are the same.
    /// </summary>
    /// <param name="left">The first <see cref="KeyType"/> to compare.</param>
    /// <param name="right">The second <see cref="KeyType"/> to compare.</param>
    /// <returns>True if <paramref name="left"/> and <paramref name="right"/> are the same; otherwise, false.</returns>
    public static bool operator ==(KeyType left, KeyType right) => left.Equals(right);

    /// <summary>
    /// Determines if two <see cref="KeyType"/> values are different.
    /// </summary>
    /// <param name="left">The first <see cref="KeyType"/> to compare.</param>
    /// <param name="right">The second <see cref="KeyType"/> to compare.</param>
    /// <returns>True if <paramref name="left"/> and <paramref name="right"/> are different; otherwise, false.</returns>
    public static bool operator !=(KeyType left, KeyType right) => !left.Equals(right);

    /// <summary>
    /// Converts a string to a <see cref="KeyType"/>.
    /// </summary>
    /// <param name="value">The string value to convert.</param>
    public static implicit operator KeyType(string value) => new KeyType(value);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is KeyType other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(KeyType other) => string.Equals(_value, other._value, StringComparison.Ordinal);

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;

    /// <inheritdoc/>
    public override string ToString() => _value;
}
