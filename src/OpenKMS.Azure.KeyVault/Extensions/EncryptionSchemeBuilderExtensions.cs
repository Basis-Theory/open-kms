using OpenKMS.Builders;

namespace OpenKMS.Azure.KeyVault.Extensions;

public static class EncryptionSchemeBuilderExtensions
{
    public static EncryptionSchemeBuilder AddKeyVaultContentEncryption(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AzureKeyVaultEncryptionOptions> configureOptions)
    {
        schemeBuilder.AddContentEncryption<AzureKeyVaultEncryptionOptions,
            AzureKeyVaultEncryptionHandler>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddKeyVaultContentEncryption<TDep>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AzureKeyVaultEncryptionOptions, TDep> configureOptions)
        where TDep : class
    {
        schemeBuilder.AddContentEncryption<AzureKeyVaultEncryptionOptions,
            AzureKeyVaultEncryptionHandler, TDep>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddKeyVaultContentEncryption<TDep1, TDep2>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AzureKeyVaultEncryptionOptions, TDep1, TDep2> configureOptions)
        where TDep1 : class
        where TDep2 : class
    {
        schemeBuilder.AddContentEncryption<AzureKeyVaultEncryptionOptions,
            AzureKeyVaultEncryptionHandler, TDep1, TDep2>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddKeyVaultContentEncryption<TDep1, TDep2, TDep3>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AzureKeyVaultEncryptionOptions, TDep1, TDep2, TDep3> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
    {
        schemeBuilder.AddContentEncryption<AzureKeyVaultEncryptionOptions,
            AzureKeyVaultEncryptionHandler, TDep1, TDep2, TDep3>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddKeyVaultContentEncryption<TDep1, TDep2, TDep3, TDep4>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AzureKeyVaultEncryptionOptions, TDep1, TDep2, TDep3, TDep4> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
        where TDep4 : class
    {
        schemeBuilder.AddContentEncryption<AzureKeyVaultEncryptionOptions,
            AzureKeyVaultEncryptionHandler, TDep1, TDep2, TDep3, TDep4>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddKeyVaultContentEncryption<TDep1, TDep2, TDep3, TDep4, TDep5>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AzureKeyVaultEncryptionOptions, TDep1, TDep2, TDep3, TDep4, TDep5> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
        where TDep4 : class
        where TDep5 : class
    {
        schemeBuilder.AddContentEncryption<AzureKeyVaultEncryptionOptions,
            AzureKeyVaultEncryptionHandler, TDep1, TDep2, TDep3, TDep4, TDep5>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddKeyVaultKeyEncryption(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AzureKeyVaultEncryptionOptions> configureOptions)
    {
        schemeBuilder.AddKeyEncryption<AzureKeyVaultEncryptionOptions,
            AzureKeyVaultEncryptionHandler>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddKeyVaultKeyEncryption<TDep>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AzureKeyVaultEncryptionOptions, TDep> configureOptions)
        where TDep : class
    {
        schemeBuilder.AddKeyEncryption<AzureKeyVaultEncryptionOptions,
            AzureKeyVaultEncryptionHandler, TDep>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddKeyVaultKeyEncryption<TDep1, TDep2>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AzureKeyVaultEncryptionOptions, TDep1, TDep2> configureOptions)
        where TDep1 : class
        where TDep2 : class
    {
        schemeBuilder.AddKeyEncryption<AzureKeyVaultEncryptionOptions,
            AzureKeyVaultEncryptionHandler, TDep1, TDep2>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddKeyVaultKeyEncryption<TDep1, TDep2, TDep3>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AzureKeyVaultEncryptionOptions, TDep1, TDep2, TDep3> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
    {
        schemeBuilder.AddKeyEncryption<AzureKeyVaultEncryptionOptions,
            AzureKeyVaultEncryptionHandler, TDep1, TDep2, TDep3>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddKeyVaultKeyEncryption<TDep1, TDep2, TDep3, TDep4>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AzureKeyVaultEncryptionOptions, TDep1, TDep2, TDep3, TDep4> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
        where TDep4 : class
    {
        schemeBuilder.AddKeyEncryption<AzureKeyVaultEncryptionOptions,
            AzureKeyVaultEncryptionHandler, TDep1, TDep2, TDep3, TDep4>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddKeyVaultKeyEncryption<TDep1, TDep2, TDep3, TDep4, TDep5>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AzureKeyVaultEncryptionOptions, TDep1, TDep2, TDep3, TDep4, TDep5> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
        where TDep4 : class
        where TDep5 : class
    {
        schemeBuilder.AddKeyEncryption<AzureKeyVaultEncryptionOptions,
            AzureKeyVaultEncryptionHandler, TDep1, TDep2, TDep3, TDep4, TDep5>(configureOptions);
        return schemeBuilder;
    }
}
