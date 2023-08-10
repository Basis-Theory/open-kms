using System.Security.Cryptography;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using OpenKMS.Aes;
using OpenKMS.Exceptions;

namespace OpenKMS.UnitTests.Aes;

public class AesEncryptionHandlerTests
{
    private readonly Randomizer _randomizer = new();
    private readonly EncryptionHandler<AesEncryptionOptions> _handler;
    private readonly IOptionsMonitor<AesEncryptionOptions> _handlerOptionsMonitor;
    private readonly AesEncryptionOptions _handlerOptions;
    private readonly string _schemeName;

    public AesEncryptionHandlerTests()
    {
        _schemeName = _randomizer.String2(10, 20);
        _handlerOptions = new AesEncryptionOptions();

        _handlerOptionsMonitor = Substitute.For<IOptionsMonitor<AesEncryptionOptions>>();
        _handlerOptionsMonitor.Get(_schemeName).Returns(_handlerOptions);

        _handler = new AesEncryptionHandler(_handlerOptionsMonitor);
    }

    [Fact]
    public async Task ShouldReturnEncryptResultWithInformationRequiredToDecrypt()
    {
        await _handler.InitializeAsync(new EncryptionScheme(_schemeName, typeof(AesEncryptionHandler), null));

        var plaintextBytes = new byte[32];
        RandomNumberGenerator.Fill(plaintextBytes);

        var encryptResult = await _handler.EncryptAsync(plaintextBytes, null, CancellationToken.None);

        encryptResult.Key.Should().NotBeNull();
        encryptResult.Ciphertext.Should().NotBeNull();
        encryptResult.Iv.Should().NotBeNull();

        var decryptedPlaintextBytes = await _handler.DecryptAsync(encryptResult.Key!, encryptResult.Ciphertext,
            encryptResult.Iv, encryptResult.AuthenticationTag, encryptResult.AdditionalAuthenticatedData, CancellationToken.None);

        decryptedPlaintextBytes.Should().BeEquivalentTo(plaintextBytes);
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenComputedAuthenticationTagIsMutated()
    {
        await _handler.InitializeAsync(new EncryptionScheme(_schemeName, typeof(AesEncryptionHandler), null));

        var plaintextBytes = new byte[32];
        RandomNumberGenerator.Fill(plaintextBytes);

        var encryptResult = await _handler.EncryptAsync(plaintextBytes, null, CancellationToken.None);

        Func<Task> decryptAction = async () => await _handler.DecryptAsync(encryptResult.Key!, encryptResult.Ciphertext,
            encryptResult.Iv, encryptResult.AuthenticationTag!.Reverse().ToArray(), encryptResult.AdditionalAuthenticatedData, CancellationToken.None);

        await decryptAction.Should().ThrowAsync<IntegrityCheckFailedException>();
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenAdditionalAuthenticatedDataIsMutated()
    {
        await _handler.InitializeAsync(new EncryptionScheme(_schemeName, typeof(AesEncryptionHandler), null));

        var plaintextBytes = new byte[32];
        RandomNumberGenerator.Fill(plaintextBytes);

        var encryptResult = await _handler.EncryptAsync(plaintextBytes, null, CancellationToken.None);

        Func<Task> decryptAction = async () => await _handler.DecryptAsync(encryptResult.Key!, encryptResult.Ciphertext,
            encryptResult.Iv, encryptResult.AuthenticationTag, encryptResult.AdditionalAuthenticatedData![1..], CancellationToken.None);

        await decryptAction.Should().ThrowAsync<IntegrityCheckFailedException>();
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenCiphertextIsMutated()
    {
        await _handler.InitializeAsync(new EncryptionScheme(_schemeName, typeof(AesEncryptionHandler), null));

        var plaintextBytes = new byte[32];
        RandomNumberGenerator.Fill(plaintextBytes);

        var encryptResult = await _handler.EncryptAsync(plaintextBytes, null, CancellationToken.None);

        Func<Task> decryptAction = async () => await _handler.DecryptAsync(encryptResult.Key!, encryptResult.Ciphertext[..1],
            encryptResult.Iv, encryptResult.AuthenticationTag, encryptResult.AdditionalAuthenticatedData, CancellationToken.None);

        await decryptAction.Should().ThrowAsync<IntegrityCheckFailedException>();
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenInitializationVectorIsMutated()
    {
        await _handler.InitializeAsync(new EncryptionScheme(_schemeName, typeof(AesEncryptionHandler), null));

        var plaintextBytes = new byte[32];
        RandomNumberGenerator.Fill(plaintextBytes);

        var encryptResult = await _handler.EncryptAsync(plaintextBytes, null, CancellationToken.None);

        Func<Task> decryptAction = async () => await _handler.DecryptAsync(encryptResult.Key!, encryptResult.Ciphertext,
            encryptResult.Iv![..1], encryptResult.AuthenticationTag, encryptResult.AdditionalAuthenticatedData, CancellationToken.None);

        await decryptAction.Should().ThrowAsync<IntegrityCheckFailedException>();
    }
}
