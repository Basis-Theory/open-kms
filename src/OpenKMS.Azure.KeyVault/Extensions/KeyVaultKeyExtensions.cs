using System.Collections.Immutable;
using Azure.Security.KeyVault.Keys;
using OpenKMS.Constants;
using OpenKMS.Structs;
using JsonWebKey = OpenKMS.Models.JsonWebKey;
using KeyOperation = OpenKMS.Structs.KeyOperation;
using KeyType = OpenKMS.Structs.KeyType;

namespace OpenKMS.Azure.KeyVault.Extensions;

public static class KeyVaultKeyExtensions
{
    public static JsonWebKey ToJsonWebKey(this KeyVaultKey key)
    {
        var keyType = new KeyType(key.KeyType.ToString());

        var keyOperations = key.KeyOperations.Select(keyOp =>
        {
            return keyOp.ToString() switch
            {
                KeyOperations.Encrypt => KeyOperation.Encrypt,
                KeyOperations.Decrypt => KeyOperation.Decrypt,
                KeyOperations.Sign => KeyOperation.Sign,
                KeyOperations.Verify => KeyOperation.Verify,
                KeyOperations.WrapKey => KeyOperation.WrapKey,
                KeyOperations.UnwrapKey => KeyOperation.UnwrapKey,
                _ => throw new ArgumentOutOfRangeException(),
            };
        });

        KeyState keyState;
        if (key.Properties.ExpiresOn.HasValue && key.Properties.ExpiresOn.Value < DateTimeOffset.Now)
            keyState = KeyState.EXPIRED;
        else if (key.Properties.NotBefore.HasValue && key.Properties.ExpiresOn > DateTimeOffset.Now)
            keyState = KeyState.DISABLED;
        else if (key.Properties.Enabled ?? false)
            keyState = KeyState.ENABLED;
        else
            keyState = KeyState.DISABLED;

        return new JsonWebKey
        {
            KeyType = keyType,
            KeyId = key.Id.ToString(),
            KeyOperations = keyOperations.ToImmutableList(),
            State = keyState,
        };
    }
}
