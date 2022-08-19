using OpenKMS.Builders;

namespace OpenKMS.Aes.Extensions;

public static class EncryptionSchemeBuilderExtensions
{
    public static EncryptionSchemeBuilder AddAesKeyEncryption(this EncryptionSchemeBuilder schemeBuilder,
        Action<AesEncryptionOptions> configureOptions)
    {
        schemeBuilder.AddKeyEncryption<AesEncryptionOptions,
            AesEncryptionHandler>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAesContentEncryption(this EncryptionSchemeBuilder schemeBuilder,
        Action<AesEncryptionOptions> configureOptions)
    {
        schemeBuilder.AddContentEncryption<AesEncryptionOptions, AesEncryptionHandler>(configureOptions);
        return schemeBuilder;
    }
}
