using System.Text;
using OpenKMS.Abstractions;
using OpenKMS.Extensions;
using OpenKMS.Models;
using OpenKMS.Providers;
using OpenKMS.Structs;
using JsonWebKey = OpenKMS.Models.JsonWebKey;

namespace OpenKMS;

public class EncryptionService : IEncryptionService
{
    public EncryptionService(IEncryptionSchemeProvider encryptionSchemeProvider,
        IEncryptionHandlerProvider encryptionHandlerProvider)
    {
        Schemes = encryptionSchemeProvider;
        Handlers = encryptionHandlerProvider;
    }

    /// <summary>
    /// Used to lookup EncryptionSchemes.
    /// </summary>
    public IEncryptionSchemeProvider Schemes { get; }

    /// <summary>
    /// Used to resolve IEncryptionHandler instances.
    /// </summary>
    public IEncryptionHandlerProvider Handlers { get; }

    public async Task<JsonWebEncryption> EncryptAsync(byte[] plaintext, string? scheme,
        CancellationToken cancellationToken = default)
    {
        var encryptionScheme = scheme != null
            ? await Schemes.GetSchemeAsync(scheme)
            : await Schemes.GetDefaultEncryptSchemeAsync();

        if (encryptionScheme == null)
            throw new ArgumentNullException(nameof(encryptionScheme));

        var contentEncryptionHandler =
            await Handlers.GetContentEncryptionHandlerAsync(encryptionScheme.Name, cancellationToken);
        if (contentEncryptionHandler == null)
            throw new ArgumentNullException(nameof(contentEncryptionHandler));

        var encryptContentResult = await contentEncryptionHandler.EncryptAsync(plaintext, cancellationToken: cancellationToken);

        var keyEncryptionHandler =
            await Handlers.GetKeyEncryptionHandlerAsync(encryptionScheme.Name, cancellationToken);

        EncryptResult? encryptKeyResult = null;
        if (keyEncryptionHandler != null)
            encryptKeyResult =
                await keyEncryptionHandler.EncryptAsync(encryptContentResult.Key!.GetBytes(), cancellationToken: cancellationToken);

        return new JsonWebEncryption
        {
            ProtectedHeader = new JoseHeader
            {
                KeyId = encryptKeyResult?.Key?.KeyId ?? encryptContentResult.Key?.KeyId,
                ContentEncryptionAlgorithm = encryptContentResult.Algorithm,
                KeyEncryptionAlgorithm = encryptKeyResult?.Algorithm,
            },
            EncryptedKey = encryptKeyResult?.Ciphertext != null
                ? Convert.ToBase64String(encryptKeyResult.Ciphertext)
                : null,
            Ciphertext = Convert.ToBase64String(encryptContentResult.Ciphertext),
            InitializationVector =
                encryptContentResult.Iv != null ? Convert.ToBase64String(encryptContentResult.Iv) : null,
            AuthenticationTag = encryptContentResult.AuthenticationTag != null
                ? Convert.ToBase64String(encryptContentResult.AuthenticationTag)
                : null,
            AdditionalAuthenticatedData = encryptContentResult.AdditionalAuthenticatedData != null
                ? Convert.ToBase64String(encryptContentResult.AdditionalAuthenticatedData)
                : null,
        };
    }

    public Task<JsonWebEncryption> EncryptAsync(string plaintext, string? scheme,
        CancellationToken cancellationToken = default)
    {
        return EncryptAsync(Encoding.UTF8.GetBytes(plaintext), scheme, cancellationToken);
    }

    public async Task<byte[]> DecryptAsync(JsonWebEncryption encryption, CancellationToken cancellationToken = default)
    {
        var contentEncryptionHandler = await Handlers.GetContentEncryptionHandlerAsync(encryption, cancellationToken);
        if (contentEncryptionHandler == null)
            throw new ArgumentNullException(nameof(contentEncryptionHandler));

        var keyEncryptionHandler = await Handlers.GetKeyEncryptionHandlerAsync(encryption, cancellationToken);
        if (keyEncryptionHandler == null)
        {
            var key = new JsonWebKey
            {
                Algorithm = encryption.ProtectedHeader!.ContentEncryptionAlgorithm,
                KeyId = encryption.ProtectedHeader.KeyId,
                KeyType = KeyType.OCT
            };

            return await contentEncryptionHandler.DecryptAsync(key, Convert.FromBase64String(encryption.Ciphertext!),
                cancellationToken: cancellationToken);
        }

        var keyEncryptionKey = new JsonWebKey
        {
            Algorithm = encryption.ProtectedHeader!.KeyEncryptionAlgorithm,
            KeyId = encryption.ProtectedHeader.KeyId,
            KeyType = KeyType.OCT
        };

        var cekBytes = await keyEncryptionHandler.DecryptAsync(keyEncryptionKey,
            Convert.FromBase64String(encryption.EncryptedKey!), cancellationToken: cancellationToken);

        var cek = new JsonWebKey
        {
            Algorithm = encryption.ProtectedHeader.ContentEncryptionAlgorithm,
            KeyType = KeyType.OCT,
            K = cekBytes
        };

        var authenticationTagBytes = string.IsNullOrEmpty(encryption.AuthenticationTag)
            ? null
            : Convert.FromBase64String(encryption.AuthenticationTag);

        var additionalAuthenticatedDataBytes = string.IsNullOrEmpty(encryption.AdditionalAuthenticatedData)
            ? null
            : Convert.FromBase64String(encryption.AdditionalAuthenticatedData);

        return await contentEncryptionHandler.DecryptAsync(cek, Convert.FromBase64String(encryption.Ciphertext!),
            Convert.FromBase64String(encryption.InitializationVector!), authenticationTagBytes,
            additionalAuthenticatedDataBytes, cancellationToken);
    }

    public async Task<string> DecryptStringAsync(JsonWebEncryption encryption,
        CancellationToken cancellationToken = default)
    {
        var plaintextBytes = await DecryptAsync(encryption, cancellationToken);

        return Encoding.UTF8.GetString(plaintextBytes);
    }
}
