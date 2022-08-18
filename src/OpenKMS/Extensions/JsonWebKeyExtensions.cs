using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using OpenKMS.Constants;
using OpenKMS.Models;
using JsonWebKey = OpenKMS.Models.JsonWebKey;

namespace OpenKMS.Extensions;

public static class JsonWebKeyExtensions
{
    public static byte[] GetBytes(this JsonWebKey key)
    {
        return key.KeyType.ToString() switch
        {
            KeyTypes.Oct => key.K!,
            _ => throw new ArgumentOutOfRangeException(nameof(key.KeyType))
        };
    }

    public static string ToCompactSerializationFormat(this JsonWebEncryption jwe)
    {
        var protectedHeader = "";
        if (jwe.ProtectedHeader != null)
        {
            var serializedHeader = JsonSerializer.Serialize(jwe.ProtectedHeader, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            protectedHeader = Base64UrlEncoder.Encode(serializedHeader);
        }

        var encryptedKey = jwe.EncryptedKey != null ? Base64UrlEncoder.Encode(jwe.EncryptedKey) : "";

        var iv = jwe.InitializationVector != null ? Base64UrlEncoder.Encode(jwe.InitializationVector) : "";

        var ciphertext = jwe.Ciphertext != null ? Base64UrlEncoder.Encode(jwe.Ciphertext) : "";

        var authenticationTag = jwe.AuthenticationTag != null ? Base64UrlEncoder.Encode(jwe.AuthenticationTag) : "";

        return protectedHeader + "." + encryptedKey + "." + iv + "." + ciphertext + "." + authenticationTag;
    }
}
