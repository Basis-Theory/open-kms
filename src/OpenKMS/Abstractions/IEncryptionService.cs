using OpenKMS.Models;

namespace OpenKMS.Abstractions;

public interface IEncryptionService
{
    Task<JsonWebEncryption> EncryptAsync(byte[] plaintext, string? scheme, CancellationToken cancellationToken = default);
    Task<JsonWebEncryption> EncryptAsync(string plaintext, string? scheme, CancellationToken cancellationToken = default);

    Task<byte[]> DecryptAsync(JsonWebEncryption encryption, CancellationToken cancellationToken = default);
    Task<string> DecryptStringAsync(JsonWebEncryption encryption, CancellationToken cancellationToken = default);
}
