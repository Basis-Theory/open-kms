using System.Collections.Immutable;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using Microsoft.Extensions.Options;
using OpenKMS.Abstractions;
using OpenKMS.AWS.KeyManagementService.Extensions;
using OpenKMS.Constants;
using OpenKMS.Exceptions;
using OpenKMS.Models;
using OpenKMS.Structs;

namespace OpenKMS.AWS.KeyManagementService;

public class AwsRsaEncryptionHandler :
    EncryptionHandler<AwsRsaEncryptionOptions>, IEncryptionHandler
{
    private readonly AmazonKeyManagementServiceClient _kmsClient;

    public AwsRsaEncryptionHandler(AmazonKeyManagementServiceClient kmsClient,
        IOptionsMonitor<AwsRsaEncryptionOptions> options) : base(options)
    {
        _kmsClient = kmsClient;
    }

    public override async Task<EncryptResult> EncryptAsync(byte[] plaintext, byte[]? additionalAuthenticatedData = null,
        CancellationToken cancellationToken = default)
    {
        var keyName = Options.KeyName;
        if (string.IsNullOrEmpty(keyName))
            throw new ArgumentException("Key name is required");


        var key = await GetOrCreateKey(keyName, cancellationToken);



        using var plaintextStream = new MemoryStream(plaintext);
        var result = await _kmsClient.EncryptAsync(new EncryptRequest
            {
                Plaintext = plaintextStream,
                EncryptionAlgorithm = Options.EncryptionAlgorithm.ToEncryptionAlgorithmSpec(),
                KeyId = key.KeyId,
            }, cancellationToken);
        var ciphertextReader = new StreamReader(result.CiphertextBlob);
        var ciphertext = await ciphertextReader.ReadToEndAsync();

        return new EncryptResult(Convert.FromBase64String(ciphertext), Options.EncryptionAlgorithm, key);
    }

    public override async Task<byte[]> DecryptAsync(JsonWebKey key, byte[] ciphertext, byte[]? iv = null,
        byte[]? authenticationTag = null,
        byte[]? additionalAuthenticatedData = null, CancellationToken cancellationToken = default)
    {
        using var ciphertextStream = new MemoryStream(ciphertext);

        var result = await _kmsClient.DecryptAsync(new DecryptRequest
        {
            CiphertextBlob = ciphertextStream,
            EncryptionAlgorithm = key.Algorithm.ToEncryptionAlgorithmSpec(),
            KeyId = key.KeyId
        }, cancellationToken);
        throw new NotImplementedException();
    }

    public override bool CanDecrypt(JsonWebKey key)
    {
        throw new NotImplementedException();
    }

    private static readonly ImmutableList<KeySpec> SupportedKeySpecs = new List<KeySpec>()
    {
        KeySpec.RSA_2048, KeySpec.RSA_3072, KeySpec.RSA_4096
    }.ToImmutableList();

    private async Task<JsonWebKey> GetOrCreateKey(string keyName, CancellationToken cancellationToken = default)
    {
        JsonWebKey key;
        try
        {
            var describeKeyResponse = await _kmsClient.DescribeKeyAsync(keyName, cancellationToken);

            if (!SupportedKeySpecs.Contains(describeKeyResponse.KeyMetadata.KeySpec))
            {
                // TODO logging
                throw new KeyTypeNotSupportedException();
            }

            key = new JsonWebKey
            {
                KeyId = describeKeyResponse.KeyMetadata.Arn,
                KeyOperations = MapKeyUsageToKeyOps(describeKeyResponse.KeyMetadata.KeyUsage),
                KeyType = KeyType.RSA,
            };
        }
        catch (NotFoundException ex)
        {
            // TODO error logging
            var keySpec = Options.KeySize switch
            {
                2048 => KeySpec.RSA_2048,
                3072 => KeySpec.RSA_3072,
                4096 => KeySpec.RSA_4096,
                null => KeySpec.RSA_2048,
                _ => throw new ArgumentOutOfRangeException(),
            };

            var createKeyResponse = await _kmsClient.CreateKeyAsync(new CreateKeyRequest
            {
                KeyUsage = KeyUsageType.ENCRYPT_DECRYPT,
                KeySpec = keySpec
            }, cancellationToken);

            key = new JsonWebKey
            {
                KeyId = createKeyResponse.KeyMetadata.Arn,
                KeyOperations = MapKeyUsageToKeyOps(createKeyResponse.KeyMetadata.KeyUsage),
                KeyType = KeyType.RSA,
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
