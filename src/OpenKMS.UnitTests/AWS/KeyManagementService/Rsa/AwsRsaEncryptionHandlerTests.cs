using System.Security.Cryptography;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using OpenKMS.AWS.KeyManagementService.Rsa;
using OpenKMS.Exceptions;
using OpenKMS.Structs;
using OpenKMS.UnitTests.AWS.Helpers;

namespace OpenKMS.UnitTests.AWS.KeyManagementService.Rsa;

public class AwsRsaEncryptionHandlerTests
{
    private readonly Randomizer _randomizer = new();
    private readonly EncryptionHandler<AwsRsaEncryptionOptions> _handler;
    private readonly Mock<IOptionsMonitor<AwsRsaEncryptionOptions>> _handlerOptionsMonitor;
    private readonly AwsRsaEncryptionOptions _handlerOptions;
    private readonly string _keyName;
    private readonly string _schemeName;
    private readonly AmazonKeyManagementServiceClient _amazonKeyManagementServiceClient;

     public AwsRsaEncryptionHandlerTests()
     {
         _keyName = _randomizer.String2(10, 20);
         _schemeName = _randomizer.String2(10, 20);

         _handlerOptions = new AwsRsaEncryptionOptions
         {
             KeyName = _keyName
         };
         _handlerOptionsMonitor = new Mock<IOptionsMonitor<AwsRsaEncryptionOptions>>();
         _handlerOptionsMonitor.Setup(x => x.Get(_schemeName))
             .Returns(_handlerOptions);

         _amazonKeyManagementServiceClient = new AmazonKeyManagementServiceClient(new LocalAwsCredentials(),
             new AmazonKeyManagementServiceConfig
             {
                 UseHttp = true,
                 EndpointDiscoveryEnabled = false,
                 ServiceURL = "http://localhost:7071/",
             });

         _handler = new AwsRsaEncryptionHandler(_amazonKeyManagementServiceClient, _handlerOptionsMonitor.Object);
     }

     [Fact]
     public async Task ShouldReturnEncryptResultWithInformationRequiredToDecrypt()
     {
         await _handler.InitializeAsync(new EncryptionScheme(_schemeName, typeof(AwsRsaEncryptionHandler), typeof(AwsRsaEncryptionHandler)));

         var plaintextBytes = new byte[32];
         RandomNumberGenerator.Fill(plaintextBytes);

         var encryptResult = await _handler.EncryptAsync(plaintextBytes, null, CancellationToken.None);

         encryptResult.Key.Should().NotBeNull();
         encryptResult.Key!.KeyId.Should().StartWith($"arn:aws:kms:eu-west-2:111122223333:key/");
         encryptResult.Ciphertext.Should().NotBeNull();
         encryptResult.Ciphertext.Should().NotBeEquivalentTo(plaintextBytes);
         encryptResult.Algorithm.Should().Be(EncryptionAlgorithm.RSA_OAEP);
         encryptResult.Iv.Should().BeNull();
         encryptResult.AuthenticationTag.Should().BeNull();
         encryptResult.AdditionalAuthenticatedData.Should().BeNull();

         var decryptedPlaintextBytes = await _handler.DecryptAsync(encryptResult.Key!, encryptResult.Ciphertext,
             encryptResult.Iv, encryptResult.AuthenticationTag, encryptResult.AdditionalAuthenticatedData, CancellationToken.None);

         decryptedPlaintextBytes.Should().BeEquivalentTo(plaintextBytes);
     }

     [Fact]
     public async Task ShouldLookupKeyByAliasWhenKeyNameIsNotPrefixedWithArn()
     {
         var existingKey = await _amazonKeyManagementServiceClient.CreateKeyAsync(new CreateKeyRequest
         {
             KeySpec = KeySpec.RSA_2048,
             KeyUsage = KeyUsageType.ENCRYPT_DECRYPT
         });
         await _amazonKeyManagementServiceClient.CreateAliasAsync($"alias/{_keyName}", existingKey.KeyMetadata.Arn);

         await _handler.InitializeAsync(new EncryptionScheme(_schemeName, typeof(AwsRsaEncryptionHandler), typeof(AwsRsaEncryptionHandler)));

         var plaintextBytes = new byte[32];
         RandomNumberGenerator.Fill(plaintextBytes);

         var encryptResult = await _handler.EncryptAsync(plaintextBytes, null, CancellationToken.None);

         encryptResult.Key.Should().NotBeNull();
         encryptResult.Key!.KeyId.Should().Be(existingKey.KeyMetadata.Arn);
     }

     [Fact]
     public async Task ShouldLookupKeyByAliasWhenKeyNameIsPrefixedWithAlias()
     {
         var existingKey = await _amazonKeyManagementServiceClient.CreateKeyAsync(new CreateKeyRequest
         {
             KeySpec = KeySpec.RSA_2048,
             KeyUsage = KeyUsageType.ENCRYPT_DECRYPT
         });
         _handlerOptions.KeyName = $"alias/{_keyName}";

         await _amazonKeyManagementServiceClient.CreateAliasAsync(_handlerOptions.KeyName, existingKey.KeyMetadata.Arn);

         await _handler.InitializeAsync(new EncryptionScheme(_schemeName, typeof(AwsRsaEncryptionHandler), typeof(AwsRsaEncryptionHandler)));

         var plaintextBytes = new byte[32];
         RandomNumberGenerator.Fill(plaintextBytes);

         var encryptResult = await _handler.EncryptAsync(plaintextBytes, null, CancellationToken.None);

         encryptResult.Key.Should().NotBeNull();
         encryptResult.Key!.KeyId.Should().Be(existingKey.KeyMetadata.Arn);
     }

     [Fact]
     public async Task ShouldThrowExceptionWhenKeyProvidedIsNotSupportedKeySpec()
     {
         var existingKey = await _amazonKeyManagementServiceClient.CreateKeyAsync(new CreateKeyRequest
         {
             KeySpec = KeySpec.SYMMETRIC_DEFAULT,
             KeyUsage = KeyUsageType.ENCRYPT_DECRYPT
         });
         _handlerOptions.KeyName = $"alias/{_keyName}";
         await _amazonKeyManagementServiceClient.CreateAliasAsync(_handlerOptions.KeyName, existingKey.KeyMetadata.Arn);

         await _handler.InitializeAsync(new EncryptionScheme(_schemeName, typeof(AwsRsaEncryptionHandler), typeof(AwsRsaEncryptionHandler)));

         var plaintextBytes = new byte[32];
         RandomNumberGenerator.Fill(plaintextBytes);

         Func<Task> encryptAction = async () => await _handler.EncryptAsync(plaintextBytes, null, CancellationToken.None);

         await encryptAction.Should().ThrowAsync<KeyTypeNotSupportedException>();
     }
}
