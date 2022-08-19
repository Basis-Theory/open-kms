using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenKMS.Abstractions;
using OpenKMS.Builders;

namespace OpenKMS.Azure.KeyVault.Extensions;

public static class EncryptionSchemeBuilderExtensions
{
    public static EncryptionSchemeBuilder AddKeyVaultKeyEncryption<TKeyNameProvider>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AzureKeyVaultEncryptionOptions> configureOptions)
        where TKeyNameProvider : class, IKeyNameProvider
    {
        schemeBuilder.Services.TryAddSingleton<TKeyNameProvider>();
        schemeBuilder.AddKeyEncryption<AzureKeyVaultEncryptionOptions,
            AzureKeyVaultEncryptionHandler<TKeyNameProvider>>(configureOptions);

        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddKeyVaultContentEncryption<TKeyNameProvider>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AzureKeyVaultEncryptionOptions> configureOptions)
        where TKeyNameProvider : class, IKeyNameProvider
    {
        schemeBuilder.Services.TryAddSingleton<TKeyNameProvider>();
        schemeBuilder.AddContentEncryption<AzureKeyVaultEncryptionOptions,
            AzureKeyVaultEncryptionHandler<TKeyNameProvider>>(configureOptions);
        return schemeBuilder;
    }
}
