using Microsoft.Extensions.DependencyInjection;
using OpenKMS.Abstractions;
using OpenKMS.Models;

namespace OpenKMS.Providers;

public class EncryptionHandlerProvider : IEncryptionHandlerProvider
{
    private readonly IEncryptionSchemeProvider _schemeProvider;
    private readonly IServiceProvider _serviceProvider;

    public EncryptionHandlerProvider(IEncryptionSchemeProvider schemeProvider, IServiceProvider serviceProvider)
    {
        _schemeProvider = schemeProvider;
        _serviceProvider = serviceProvider;
    }

    public async Task<IEncryptionHandler> GetContentEncryptionHandlerAsync(string scheme,
        CancellationToken cancellationToken = default)
    {
        var encryptionScheme = await _schemeProvider.GetSchemeAsync(scheme);

        var handler =
            _serviceProvider.GetRequiredService(encryptionScheme!.ContentEncryptionHandlerType) as IEncryptionHandler;

        await handler!.InitializeAsync(encryptionScheme);

        return handler;
    }

    public async Task<IEncryptionHandler> GetContentEncryptionHandlerAsync(JsonWebEncryption jwe,
        CancellationToken cancellationToken = default)
    {
        var key = new JsonWebKey
        {
            Algorithm = jwe.ProtectedHeader!.ContentEncryptionAlgorithm,
            KeyId = jwe.ProtectedHeader.KeyId,
        };

        var schemes = await _schemeProvider.GetSchemesAsync();
        foreach (var scheme in schemes)
        {
            var handler =
                _serviceProvider.GetRequiredService(scheme.ContentEncryptionHandlerType) as IEncryptionHandler;
            await handler!.InitializeAsync(scheme);

            if (handler.CanDecrypt(key)) return handler;
        }


        throw new InvalidOperationException($"No content encryption handler is registered that can decrypt {jwe.ProtectedHeader!.ContentEncryptionAlgorithm} using key ID {jwe.ProtectedHeader.KeyId ?? "<null>"}.");
    }

    public async Task<IEncryptionHandler?> GetKeyEncryptionHandlerAsync(string scheme,
        CancellationToken cancellationToken = default)
    {
        var encryptionScheme = await _schemeProvider.GetSchemeAsync(scheme);

        if (encryptionScheme!.KeyEncryptionHandlerType == null) return null;

        var handler =
            _serviceProvider.GetRequiredService(encryptionScheme.KeyEncryptionHandlerType) as IEncryptionHandler;

        if (handler != null)
            await handler.InitializeAsync(encryptionScheme);

        return handler;
    }

    public async Task<IEncryptionHandler?> GetKeyEncryptionHandlerAsync(JsonWebEncryption jwe,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(jwe.EncryptedKey))
            return null;

        var key = new JsonWebKey
        {
            Algorithm = jwe.ProtectedHeader!.KeyEncryptionAlgorithm,
            KeyId = jwe.ProtectedHeader.KeyId,
        };

        var schemes = await _schemeProvider.GetSchemesAsync();
        foreach (var scheme in schemes.Where(s => s.KeyEncryptionHandlerType != null))
        {
            var handler = _serviceProvider.GetRequiredService(scheme.KeyEncryptionHandlerType!) as IEncryptionHandler;
            await handler!.InitializeAsync(scheme);

            if (handler.CanDecrypt(key)) return handler;
        }

        throw new InvalidOperationException($"No key encryption handler is registered that can decrypt {jwe.ProtectedHeader!.KeyEncryptionAlgorithm} with key ID {jwe.ProtectedHeader.KeyId ?? "<null>"}.");
    }
}
