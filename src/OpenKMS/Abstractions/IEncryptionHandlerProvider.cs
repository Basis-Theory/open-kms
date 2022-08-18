using OpenKMS.Models;

namespace OpenKMS.Abstractions;

public interface IEncryptionHandlerProvider
{
    Task<IEncryptionHandler> GetContentEncryptionHandlerAsync(string scheme,
        CancellationToken cancellationToken = default);

    Task<IEncryptionHandler> GetContentEncryptionHandlerAsync(JsonWebEncryption jwe,
        CancellationToken cancellationToken = default);

    Task<IEncryptionHandler?>
        GetKeyEncryptionHandlerAsync(string scheme, CancellationToken cancellationToken = default);

    Task<IEncryptionHandler?> GetKeyEncryptionHandlerAsync(JsonWebEncryption jwe,
        CancellationToken cancellationToken = default);
}
