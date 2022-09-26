using OpenKMS.AWS.KeyManagementService.Aes;
using OpenKMS.Builders;

namespace OpenKMS.AWS.KeyManagementService.Extensions;

public static class AesEncryptionSchemeBuilderExtensions
{
    public static EncryptionSchemeBuilder AddAwsAesKeyEncryption(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AwsAesEncryptionOptions> configureOptions)
    {
        schemeBuilder.AddKeyEncryption<AwsAesEncryptionOptions,
            AwsAesEncryptionHandler>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAwsAesKeyEncryption<TDep>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AwsAesEncryptionOptions, TDep> configureOptions)
        where TDep : class
    {
        schemeBuilder.AddKeyEncryption<AwsAesEncryptionOptions,
            AwsAesEncryptionHandler, TDep>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAwsAesKeyEncryption<TDep1, TDep2>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AwsAesEncryptionOptions, TDep1, TDep2> configureOptions)
        where TDep1 : class
        where TDep2 : class
    {
        schemeBuilder.AddKeyEncryption<AwsAesEncryptionOptions,
            AwsAesEncryptionHandler, TDep1, TDep2>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAwsAesKeyEncryption<TDep1, TDep2, TDep3>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AwsAesEncryptionOptions, TDep1, TDep2, TDep3> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
    {
        schemeBuilder.AddKeyEncryption<AwsAesEncryptionOptions,
            AwsAesEncryptionHandler, TDep1, TDep2, TDep3>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAwsAesKeyEncryption<TDep1, TDep2, TDep3, TDep4>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AwsAesEncryptionOptions, TDep1, TDep2, TDep3, TDep4> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
        where TDep4 : class
    {
        schemeBuilder.AddKeyEncryption<AwsAesEncryptionOptions,
            AwsAesEncryptionHandler, TDep1, TDep2, TDep3, TDep4>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAwsAesKeyEncryption<TDep1, TDep2, TDep3, TDep4, TDep5>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AwsAesEncryptionOptions, TDep1, TDep2, TDep3, TDep4, TDep5> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
        where TDep4 : class
        where TDep5 : class
    {
        schemeBuilder.AddKeyEncryption<AwsAesEncryptionOptions,
            AwsAesEncryptionHandler, TDep1, TDep2, TDep3, TDep4, TDep5>(configureOptions);
        return schemeBuilder;
    }
}
