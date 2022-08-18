using OpenKMS.Structs;

namespace OpenKMS.Models;

public class EncryptResult
{
    public EncryptResult(byte[] ciphertext,
        EncryptionAlgorithm algorithm,
        JsonWebKey? key = null,
        byte[]? iv = null,
        byte[]? authenticationTag = null,
        byte[]? additionalAuthenticatedData = null)
    {
        Ciphertext = ciphertext;
        Algorithm = algorithm;
        Key = key;
        Iv = iv;
        AuthenticationTag = authenticationTag;
        AdditionalAuthenticatedData = additionalAuthenticatedData;
    }

    /// <summary>
    /// Gets the ciphertext that is the result of the encryption.
    /// </summary>
    public byte[] Ciphertext { get; internal set; }

    /// <summary>
    /// Gets the <see cref="EncryptionAlgorithm"/> used for encryption. This must be stored alongside the <see cref="Ciphertext"/> as the same algorithm must be used to decrypt it.
    /// </summary>
    public EncryptionAlgorithm Algorithm { get; internal set; }

    /// <summary>
    /// Gets the JsonWebKey used for encryption.
    /// </summary>
    public JsonWebKey? Key { get; internal set; }

    /// <summary>
    /// Gets the initialization vector for encryption.
    /// </summary>
    public byte[]? Iv { get; internal set; }

    /// <summary>
    /// Gets the authentication tag resulting from encryption with a symmetric key including <see cref="EncryptionAlgorithm.A128Gcm"/>, <see cref="EncryptionAlgorithm.A192Gcm"/>, or <see cref="EncryptionAlgorithm.A256Gcm"/>.
    /// </summary>
    public byte[]? AuthenticationTag { get; internal set; }

    /// <summary>
    /// Gets additional data that is authenticated during decryption but not encrypted.
    /// </summary>
    public byte[]? AdditionalAuthenticatedData { get; internal set; }
}
