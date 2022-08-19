using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using OpenKMS.Abstractions;
using OpenKMS.Models;
using OpenKMS.Structs;

namespace OpenKMS.Aes;

public class AesEncryptionHandler : EncryptionHandler<AesEncryptionOptions>, IEncryptionHandler
{
    public AesEncryptionHandler(IOptionsMonitor<AesEncryptionOptions> options) : base(options)
    {
    }

    public override Task<EncryptResult> EncryptAsync(byte[] plaintext, byte[]? additionalAuthenticatedData = null,
        CancellationToken cancellationToken = default)
    {
        additionalAuthenticatedData ??= new byte[16];
        RandomNumberGenerator.Fill(additionalAuthenticatedData);

        int keySizeBits;
        if (Options.KeySize.HasValue)
            keySizeBits = Options.KeySize.Value;
        else if (Options.EncryptionAlgorithm == EncryptionAlgorithm.A128CBC_HS256)
            keySizeBits = 128;
        else
            keySizeBits = 256;
        var keyLength = keySizeBits / 8;

        var key = new byte[keyLength * 2];
        RandomNumberGenerator.Fill(key);
        var iv = new byte[16];
        RandomNumberGenerator.Fill(iv);

        var encKey = key[..keyLength];
        var macKey = key[keyLength..];
        using var aes = System.Security.Cryptography.Aes.Create();
        aes.Key = encKey;

        var ciphertext = aes.EncryptCbc(plaintext, iv);

        var al = BitConverter.GetBytes(BitConverter.ToUInt64(additionalAuthenticatedData));
        if (BitConverter.IsLittleEndian)
            Array.Reverse(al);

        var hmac = Options.EncryptionAlgorithm == EncryptionAlgorithm.A128CBC_HS256
            ? new HMACSHA256(macKey)
            : new HMACSHA512(macKey) as HMAC;

        var bytesToHash = additionalAuthenticatedData.Concat(iv).Concat(ciphertext).Concat(al).ToArray();
        var hash = hmac.ComputeHash(bytesToHash);
        var tag = hash[..keyLength];

        return Task.FromResult(new EncryptResult(ciphertext, Options.EncryptionAlgorithm,
            new JsonWebKey
            {
                KeyType = KeyType.OCT,
                K = key,
            }, iv, authenticationTag: tag, additionalAuthenticatedData: additionalAuthenticatedData));
    }

    public override Task<byte[]> DecryptAsync(JsonWebKey key, byte[] ciphertext, byte[]? iv = null,
        byte[]? authenticationTag = null, byte[]? additionalAuthenticatedData = null,
        CancellationToken cancellationToken = default)
    {
        if (key.K == null)
            throw new ArgumentNullException(nameof(key.K));

        if (iv == null)
            throw new ArgumentNullException(nameof(iv));

        // TODO Verify integrity and authenticity
        var encKey = key.K![..(key.K!.Length / 2)];

        using var aes = System.Security.Cryptography.Aes.Create();
        aes.Key = encKey;
        aes.IV = iv;

        return Task.FromResult(aes.DecryptCbc(ciphertext, iv));
    }

    public override bool CanDecrypt(JsonWebKey key) => Options.EncryptionAlgorithm == key.Algorithm;
}
