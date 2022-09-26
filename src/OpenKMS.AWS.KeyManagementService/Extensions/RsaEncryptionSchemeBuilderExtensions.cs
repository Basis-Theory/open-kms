using OpenKMS.AWS.KeyManagementService.Rsa;
using OpenKMS.Builders;

namespace OpenKMS.AWS.KeyManagementService.Extensions;

public static class RsaEncryptionSchemeBuilderExtensions
{
    public static EncryptionSchemeBuilder AddAwsRsaKeyEncryption(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AwsRsaEncryptionOptions> configureOptions)
    {
        schemeBuilder.AddKeyEncryption<AwsRsaEncryptionOptions,
            AwsRsaEncryptionHandler>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAwsRsaKeyEncryption<TDep>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AwsRsaEncryptionOptions, TDep> configureOptions)
        where TDep : class
    {
        schemeBuilder.AddKeyEncryption<AwsRsaEncryptionOptions,
            AwsRsaEncryptionHandler, TDep>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAwsRsaKeyEncryption<TDep1, TDep2>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AwsRsaEncryptionOptions, TDep1, TDep2> configureOptions)
        where TDep1 : class
        where TDep2 : class
    {
        schemeBuilder.AddKeyEncryption<AwsRsaEncryptionOptions,
            AwsRsaEncryptionHandler, TDep1, TDep2>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAwsRsaKeyEncryption<TDep1, TDep2, TDep3>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AwsRsaEncryptionOptions, TDep1, TDep2, TDep3> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
    {
        schemeBuilder.AddKeyEncryption<AwsRsaEncryptionOptions,
            AwsRsaEncryptionHandler, TDep1, TDep2, TDep3>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAwsRsaKeyEncryption<TDep1, TDep2, TDep3, TDep4>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AwsRsaEncryptionOptions, TDep1, TDep2, TDep3, TDep4> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
        where TDep4 : class
    {
        schemeBuilder.AddKeyEncryption<AwsRsaEncryptionOptions,
            AwsRsaEncryptionHandler, TDep1, TDep2, TDep3, TDep4>(configureOptions);
        return schemeBuilder;
    }

    public static EncryptionSchemeBuilder AddAwsRsaKeyEncryption<TDep1, TDep2, TDep3, TDep4, TDep5>(
        this EncryptionSchemeBuilder schemeBuilder,
        Action<AwsRsaEncryptionOptions, TDep1, TDep2, TDep3, TDep4, TDep5> configureOptions)
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
        where TDep4 : class
        where TDep5 : class
    {
        schemeBuilder.AddKeyEncryption<AwsRsaEncryptionOptions,
            AwsRsaEncryptionHandler, TDep1, TDep2, TDep3, TDep4, TDep5>(configureOptions);
        return schemeBuilder;
    }
}
