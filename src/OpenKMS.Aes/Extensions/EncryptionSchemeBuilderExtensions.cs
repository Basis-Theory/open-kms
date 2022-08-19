using OpenKMS.Builders;

namespace OpenKMS.Aes.Extensions;

public static class EncryptionSchemeBuilderExtensions
{
    public static EncryptionSchemeBuilder AddAesContentEncryption(this EncryptionSchemeBuilder schemeBuilder,
        Action<AesEncryptionOptions> configureOptions)
    {
        schemeBuilder.AddContentEncryption<AesEncryptionOptions, AesEncryptionHandler>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAesContentEncryption<TDep>(this EncryptionSchemeBuilder schemeBuilder,
        Action<AesEncryptionOptions, TDep> configureOptions)
        where TDep : class
    {
        schemeBuilder.AddContentEncryption<AesEncryptionOptions, AesEncryptionHandler, TDep>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAesContentEncryption<TDep1, TDep2>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AesEncryptionOptions, TDep1, TDep2> configureOptions)
        where TDep1 : class
        where TDep2 : class
    {
        schemeBuilder.AddContentEncryption<AesEncryptionOptions, AesEncryptionHandler, TDep1, TDep2>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAesContentEncryption<TDep1, TDep2, TDep3>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AesEncryptionOptions, TDep1, TDep2, TDep3> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
    {
        schemeBuilder.AddContentEncryption<AesEncryptionOptions, AesEncryptionHandler, TDep1, TDep2, TDep3>(
            configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAesContentEncryption<TDep1, TDep2, TDep3, TDep4>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AesEncryptionOptions, TDep1, TDep2, TDep3, TDep4> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
        where TDep4 : class
    {
        schemeBuilder.AddContentEncryption<AesEncryptionOptions, AesEncryptionHandler, TDep1, TDep2, TDep3, TDep4>(
            configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAesContentEncryption<TDep1, TDep2, TDep3, TDep4, TDep5>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AesEncryptionOptions, TDep1, TDep2, TDep3, TDep4, TDep5> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
        where TDep4 : class
        where TDep5 : class
    {
        schemeBuilder
            .AddContentEncryption<AesEncryptionOptions, AesEncryptionHandler, TDep1, TDep2, TDep3, TDep4, TDep5>(
                configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAesKeyEncryption(this EncryptionSchemeBuilder schemeBuilder,
        Action<AesEncryptionOptions> configureOptions)
    {
        schemeBuilder.AddKeyEncryption<AesEncryptionOptions, AesEncryptionHandler>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAesKeyEncryption<TDep>(this EncryptionSchemeBuilder schemeBuilder,
        Action<AesEncryptionOptions, TDep> configureOptions)
        where TDep : class
    {
        schemeBuilder.AddKeyEncryption<AesEncryptionOptions, AesEncryptionHandler, TDep>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAesKeyEncryption<TDep1, TDep2>(this EncryptionSchemeBuilder schemeBuilder,
        Action<AesEncryptionOptions, TDep1, TDep2> configureOptions)
        where TDep1 : class
        where TDep2 : class
    {
        schemeBuilder.AddKeyEncryption<AesEncryptionOptions, AesEncryptionHandler, TDep1, TDep2>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAesKeyEncryption<TDep1, TDep2, TDep3>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AesEncryptionOptions, TDep1, TDep2, TDep3> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
    {
        schemeBuilder.AddKeyEncryption<AesEncryptionOptions, AesEncryptionHandler, TDep1, TDep2, TDep3>(
            configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAesKeyEncryption<TDep1, TDep2, TDep3, TDep4>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AesEncryptionOptions, TDep1, TDep2, TDep3, TDep4> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
        where TDep4 : class
    {
        schemeBuilder.AddKeyEncryption<AesEncryptionOptions, AesEncryptionHandler, TDep1, TDep2, TDep3, TDep4>(
            configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAesKeyEncryption<TDep1, TDep2, TDep3, TDep4, TDep5>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AesEncryptionOptions, TDep1, TDep2, TDep3, TDep4, TDep5> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
        where TDep4 : class
        where TDep5 : class
    {
        schemeBuilder.AddKeyEncryption<AesEncryptionOptions, AesEncryptionHandler, TDep1, TDep2, TDep3, TDep4, TDep5>(
            configureOptions);
        return schemeBuilder;
    }
}
