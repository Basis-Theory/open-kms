using System.Collections.Immutable;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using Microsoft.Extensions.Options;
using OpenKMS.Abstractions;
using OpenKMS.AWS.KeyManagementService.Extensions;
using OpenKMS.Exceptions;
using OpenKMS.Models;
using OpenKMS.Structs;

namespace OpenKMS.AWS.KeyManagementService;

public abstract class BaseAwsEncryptionHandler<TOptions> :
    EncryptionHandler<TOptions>, IEncryptionHandler where TOptions : BaseAwsEncryptionOptions, new()
{
    protected readonly AmazonKeyManagementServiceClient KmsClient;

    protected BaseAwsEncryptionHandler(AmazonKeyManagementServiceClient kmsClient,
        IOptionsMonitor<TOptions> options) : base(options)
    {
        KmsClient = kmsClient;
    }

    public override async Task<EncryptResult> EncryptAsync(byte[] plaintext, byte[]? additionalAuthenticatedData = null,
        CancellationToken cancellationToken = default)
    {
        var keyName = Options.KeyName;
        if (string.IsNullOrEmpty(keyName))
            throw new ArgumentException("Key name is required");

        var key = await GetOrCreateKey(keyName, cancellationToken);

        using var plaintextStream = new MemoryStream(plaintext);
        var result = await KmsClient.EncryptAsync(new EncryptRequest
            {
                Plaintext = plaintextStream,
                EncryptionAlgorithm = Options.EncryptionAlgorithm.ToEncryptionAlgorithmSpec(),
                KeyId = key.KeyId,
            }, cancellationToken);

        return new EncryptResult(result.CiphertextBlob.ToArray(), Options.EncryptionAlgorithm, key);
    }

    public override async Task<byte[]> DecryptAsync(JsonWebKey key, byte[] ciphertext, byte[]? iv = null,
        byte[]? authenticationTag = null,
        byte[]? additionalAuthenticatedData = null, CancellationToken cancellationToken = default)
    {
        using var ciphertextStream = new MemoryStream(ciphertext);

        var result = await KmsClient.DecryptAsync(new DecryptRequest
        {
            CiphertextBlob = ciphertextStream,
            EncryptionAlgorithm = (key.Algorithm ?? Options.EncryptionAlgorithm).ToEncryptionAlgorithmSpec(),
            KeyId = key.KeyId
        }, cancellationToken);

        return result.Plaintext.ToArray();
    }

    public override bool CanDecrypt(JsonWebKey key) =>
        key.KeyId != null &&
        key.KeyId.StartsWith("arn:") &&
        Options.EncryptionAlgorithm == key.Algorithm;

    protected abstract ImmutableList<KeySpec> SupportedKeySpecs { get; }

    private async Task<JsonWebKey> GetOrCreateKey(string keyName, CancellationToken cancellationToken = default)
    {
        JsonWebKey key;
        try
        {
            var lookupKeyId = keyName.StartsWith("arn:") || keyName.StartsWith("alias/") ? keyName : $"alias/{keyName}";
            var describeKeyResponse = await KmsClient.DescribeKeyAsync(lookupKeyId, cancellationToken);

            if (!SupportedKeySpecs.Contains(describeKeyResponse.KeyMetadata.KeySpec))
            {
                // TODO logging
                throw new KeyTypeNotSupportedException();
            }

            key = new JsonWebKey
            {
                KeyId = describeKeyResponse.KeyMetadata.Arn,
                KeyOperations = MapKeyUsageToKeyOps(describeKeyResponse.KeyMetadata.KeyUsage),
                KeyType = Options.KeyType,
            };
        }
        catch (NotFoundException ex)
        {
            // TODO error logging
            var keySpec = Options.GetKeySpec();

            var createKeyResponse = await KmsClient.CreateKeyAsync(new CreateKeyRequest
            {
                KeyUsage = KeyUsageType.ENCRYPT_DECRYPT,
                KeySpec = keySpec
            }, cancellationToken);

            var aliasName = Options.KeyName.StartsWith("alias/") ? Options.KeyName : $"alias/{Options.KeyName}";
            await KmsClient.CreateAliasAsync(new CreateAliasRequest
            {
                AliasName = aliasName,
                TargetKeyId = createKeyResponse.KeyMetadata.KeyId,
            }, cancellationToken);

            key = new JsonWebKey
            {
                KeyId = createKeyResponse.KeyMetadata.Arn,
                KeyOperations = MapKeyUsageToKeyOps(createKeyResponse.KeyMetadata.KeyUsage),
                KeyType = Options.KeyType,
            };
        }

        return key;
    }

    private static IReadOnlyList<KeyOperation> MapKeyUsageToKeyOps(KeyUsageType keyUsage) =>
        keyUsage.Value switch
        {
            nameof(KeyUsageType.ENCRYPT_DECRYPT) => new[] { KeyOperation.Encrypt, KeyOperation.Decrypt, },
            nameof(KeyUsageType.GENERATE_VERIFY_MAC) => new[] { KeyOperation.DeriveKey, KeyOperation.DeriveBits, },
            nameof(KeyUsageType.SIGN_VERIFY) => new[] { KeyOperation.Sign, KeyOperation.Verify, },
            _ => throw new ArgumentOutOfRangeException()
        };
}
