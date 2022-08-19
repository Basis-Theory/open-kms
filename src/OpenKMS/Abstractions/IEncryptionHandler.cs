using OpenKMS.Models;

namespace OpenKMS.Abstractions;

public interface IEncryptionHandler
{
    /// <summary>
    /// Initialize the encryption handler. The handler should initialize anything it needs from the scheme as part of this method.
    /// </summary>
    /// <param name="scheme">The <see cref="EncryptionScheme"/> scheme.</param>
    Task InitializeAsync(EncryptionScheme scheme);

    Task<EncryptResult> EncryptAsync(byte[] plaintext, byte[]? additionalAuthenticatedData = null,
        CancellationToken cancellationToken = default);

    Task<byte[]> DecryptAsync(JsonWebKey key, byte[] ciphertext, byte[]? iv = null,
        byte[]? authenticationTag = null, byte[]? additionalAuthenticatedData = null,
        CancellationToken cancellationToken = default);

    bool CanDecrypt(JsonWebKey key);
}
