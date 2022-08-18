using OpenKMS.Constants;
using OpenKMS.Models;
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace OpenKMS.Structs;

/// <summary>
/// <see cref="JsonWebKey"/> encryption algorithm.
/// </summary>
public readonly struct EncryptionAlgorithm : IEquatable<EncryptionAlgorithm>
{


    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="EncryptionAlgorithm"/> structure.
    /// </summary>
    /// <param name="value">The string value of the instance.</param>
    public EncryptionAlgorithm(string value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// HMAC using SHA-256
    /// </summary>
    public static EncryptionAlgorithm HS256 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.HS256);

    /// <summary>
    /// HMAC using SHA-384
    /// </summary>
    public static EncryptionAlgorithm HS384 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.HS384);

    /// <summary>
    /// HMAC using SHA-512
    /// </summary>
    public static EncryptionAlgorithm HS512 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.HS512);

    /// <summary>
    /// RSASSA-PKCS1-v1_5 using SHA-256
    /// </summary>
    public static EncryptionAlgorithm RS256 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.RS256);

    /// <summary>
    /// RSASSA-PKCS1-v1_5 using SHA-384
    /// </summary>
    public static EncryptionAlgorithm RS384 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.RS384);

    /// <summary>
    /// RSASSA-PKCS1-v1_5 using SHA-512
    /// </summary>
    public static EncryptionAlgorithm RS512 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.RS512);

    /// <summary>
    /// ECDSA using P-256 and SHA-256
    /// </summary>
    public static EncryptionAlgorithm ES256 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.ES256);

    /// <summary>
    /// ECDSA using P-384 and SHA-384
    /// </summary>
    public static EncryptionAlgorithm ES384 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.ES384);

    /// <summary>
    /// ECDSA using P-521 and SHA-512
    /// </summary>
    public static EncryptionAlgorithm ES512 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.ES512);

    /// <summary>
    /// RSASSA-PSS using SHA-256 and MGF1 with
    /// </summary>
    public static EncryptionAlgorithm PS256 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.PS256);

    /// <summary>
    /// RSASSA-PSS using SHA-384 and MGF1 with
    /// </summary>
    public static EncryptionAlgorithm PS384 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.PS384);

    /// <summary>
    /// RSASSA-PSS using SHA-512 and MGF1 with
    /// </summary>
    public static EncryptionAlgorithm PS512 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.PS512);

    /// <summary>
    /// No digital signature or MAC performed
    /// </summary>
    public static EncryptionAlgorithm None { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.None);

    /// <summary>
    /// RSAES-PKCS1-v1_5
    /// </summary>
    public static EncryptionAlgorithm RSA1_5 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.RSA1_5);

    /// <summary>
    /// RSAES OAEP using default parameters
    /// </summary>
    public static EncryptionAlgorithm RSA_OAEP { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.RSA_OAEP);

    /// <summary>
    /// RSAES OAEP using SHA-256 and MGF1 with
    /// </summary>
    public static EncryptionAlgorithm RSA_OAEP_256 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.RSA_OAEP_256);

    /// <summary>
    /// AES Key Wrap using 128-bit key
    /// </summary>
    public static EncryptionAlgorithm A128KW { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.A128KW);

    /// <summary>
    /// AES Key Wrap using 192-bit key
    /// </summary>
    public static EncryptionAlgorithm A192KW { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.A192KW);

    /// <summary>
    /// AES Key Wrap using 256-bit key
    /// </summary>
    public static EncryptionAlgorithm A256KW { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.A256KW);

    /// <summary>
    /// Direct use of a shared symmetric key
    /// </summary>
    public static EncryptionAlgorithm Dir { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.Dir);

    /// <summary>
    /// ECDH-ES using Concat KDF
    /// </summary>
    public static EncryptionAlgorithm ECDH_ES { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.ECDH_ES);

    /// <summary>
    /// ECDH-ES using Concat KDF and "A128KW"
    /// </summary>
    public static EncryptionAlgorithm ECDH_ES_A128KW { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.ECDH_ES_A128KW);

    /// <summary>
    /// ECDH-ES using Concat KDF and "A256KW"
    /// </summary>
    public static EncryptionAlgorithm ECDH_ES_A256KW { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.ECDH_ES_A256KW);

    /// <summary>
    /// Key wrapping with AES GCM using 128-bit key
    /// </summary>
    public static EncryptionAlgorithm A128GCMKW { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.A128GCMKW);

    /// <summary>
    /// Key wrapping with AES GCM using 192-bit key
    /// </summary>
    public static EncryptionAlgorithm A192GCMKW { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.A192GCMKW);

    /// <summary>
    /// Key wrapping with AES GCM using 256-bit key
    /// </summary>
    public static EncryptionAlgorithm A256GCMKW { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.A256GCMKW);

    /// <summary>
    /// PBES2 with HMAC SHA-256 and "A128KW"
    /// </summary>
    public static EncryptionAlgorithm PBES2_HS256_A128KW { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.PBES2_HS256_A128KW);

    /// <summary>
    /// PBES2 with HMAC SHA-384 and "A192KW"
    /// </summary>
    public static EncryptionAlgorithm PBES2_HS384_A192KW { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.PBES2_HS384_A192KW);

    /// <summary>
    /// PBES2 with HMAC SHA-512 and "A256KW"
    /// </summary>
    public static EncryptionAlgorithm PBES2_HS512_A256KW { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.PBES2_HS512_A256KW);

    /// <summary>
    /// AES_128_CBC_HMAC_SHA_256 authenticated
    /// </summary>
    public static EncryptionAlgorithm A128CBC_HS256 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.A128CBC_HS256);

    /// <summary>
    /// AES_192_CBC_HMAC_SHA_384 authenticated
    /// </summary>
    public static EncryptionAlgorithm A192CBC_HS384 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.A192CBC_HS384);

    /// <summary>
    /// AES_256_CBC_HMAC_SHA_512 authenticated
    /// </summary>
    public static EncryptionAlgorithm A256CBC_HS512 { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.A256CBC_HS512);

    /// <summary>
    /// AES GCM using 128-bit key
    /// </summary>
    public static EncryptionAlgorithm A128GCM { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.A128GCM);

    /// <summary>
    /// AES GCM using 192-bit key
    /// </summary>
    public static EncryptionAlgorithm A192GCM { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.A192GCM);

    /// <summary>
    /// AES GCM using 256-bit key
    /// </summary>
    public static EncryptionAlgorithm A256GCM { get; } = new EncryptionAlgorithm(EncryptionAlgorithms.A256GCM);

    /// <summary>
    /// Determines if two <see cref="EncryptionAlgorithm"/> values are the same.
    /// </summary>
    /// <param name="left">The first <see cref="EncryptionAlgorithm"/> to compare.</param>
    /// <param name="right">The second <see cref="EncryptionAlgorithm"/> to compare.</param>
    /// <returns>True if <paramref name="left"/> and <paramref name="right"/> are the same; otherwise, false.</returns>
    public static bool operator ==(EncryptionAlgorithm left, EncryptionAlgorithm right) => left.Equals(right);

    /// <summary>
    /// Determines if two <see cref="EncryptionAlgorithm"/> values are different.
    /// </summary>
    /// <param name="left">The first <see cref="EncryptionAlgorithm"/> to compare.</param>
    /// <param name="right">The second <see cref="EncryptionAlgorithm"/> to compare.</param>
    /// <returns>True if <paramref name="left"/> and <paramref name="right"/> are different; otherwise, false.</returns>
    public static bool operator !=(EncryptionAlgorithm left, EncryptionAlgorithm right) => !left.Equals(right);

    /// <summary>
    /// Converts a string to a <see cref="KeyType"/>.
    /// </summary>
    /// <param name="value">The string value to convert.</param>
    public static implicit operator EncryptionAlgorithm(string value) => new EncryptionAlgorithm(value);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is KeyType other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(EncryptionAlgorithm other) => string.Equals(_value, other._value, StringComparison.Ordinal);

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;

    /// <inheritdoc/>
    public override string ToString() => _value;
}
