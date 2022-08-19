using OpenKMS.Models;

namespace OpenKMS.Abstractions;

public interface IEncryptionService
{
    JsonWebEncryption Encrypt(byte[] plaintext, string? scheme);
    JsonWebEncryption Encrypt(string plaintext, string? scheme);

    Task<JsonWebEncryption> EncryptAsync(byte[] plaintext, string? scheme, CancellationToken cancellationToken = default);
    Task<JsonWebEncryption> EncryptAsync(string plaintext, string? scheme, CancellationToken cancellationToken = default);

    byte[] Decrypt(JsonWebEncryption encryption);
    string DecryptString(JsonWebEncryption encryption);

    Task<byte[]> DecryptAsync(JsonWebEncryption encryption, CancellationToken cancellationToken = default);
    Task<string> DecryptStringAsync(JsonWebEncryption encryption, CancellationToken cancellationToken = default);
}
