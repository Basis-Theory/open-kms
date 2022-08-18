using OpenKMS.Constants;
using OpenKMS.Models;

namespace OpenKMS.Structs;

/// <summary>
/// <see cref="JsonWebEncryption"/> compression algorithm.
/// </summary>
public readonly struct CompressionAlgorithm : IEquatable<CompressionAlgorithm>
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompressionAlgorithm"/> structure.
    /// </summary>
    /// <param name="value">The string value of the instance.</param>
    public CompressionAlgorithm(string value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Compression with the DEFLATE [[RFC1951](https://datatracker.ietf.org/doc/html/rfc1951)] algorithm
    /// </summary>
    public static CompressionAlgorithm Deflate = new CompressionAlgorithm(CompressionAlgorithms.Deflate);

    /// <summary>
    /// Determines if two <see cref="CompressionAlgorithm"/> values are the same.
    /// </summary>
    /// <param name="left">The first <see cref="CompressionAlgorithm"/> to compare.</param>
    /// <param name="right">The second <see cref="CompressionAlgorithm"/> to compare.</param>
    /// <returns>True if <paramref name="left"/> and <paramref name="right"/> are the same; otherwise, false.</returns>
    public static bool operator ==(CompressionAlgorithm left, CompressionAlgorithm right) => left.Equals(right);

    /// <summary>
    /// Determines if two <see cref="CompressionAlgorithm"/> values are different.
    /// </summary>
    /// <param name="left">The first <see cref="CompressionAlgorithm"/> to compare.</param>
    /// <param name="right">The second <see cref="CompressionAlgorithm"/> to compare.</param>
    /// <returns>True if <paramref name="left"/> and <paramref name="right"/> are different; otherwise, false.</returns>
    public static bool operator !=(CompressionAlgorithm left, CompressionAlgorithm right) => !left.Equals(right);

    /// <summary>
    /// Converts a string to a <see cref="KeyType"/>.
    /// </summary>
    /// <param name="value">The string value to convert.</param>
    public static implicit operator CompressionAlgorithm(string value) => new CompressionAlgorithm(value);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is KeyType other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(CompressionAlgorithm other) => string.Equals(_value, other._value, StringComparison.Ordinal);

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;

    /// <inheritdoc/>
    public override string ToString() => _value;
}
