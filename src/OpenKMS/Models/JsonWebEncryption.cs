using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;

namespace OpenKMS.Models;

public class JsonWebEncryption
{
    /// <summary>
    /// with the value BASE64URL(UTF8(JWE Protected Header))
    /// </summary>
    [JsonPropertyName("protected")]
    public JoseHeader? ProtectedHeader { get; set; }

    /// <summary>
    /// with the value JWE Shared Unprotected Header
    /// </summary>
    [JsonPropertyName("unprotected")]
    public JoseHeader? UnprotectedHeader { get; set; }

    /// <summary>
    /// with the value JWE Per-Recipient Unprotected Header
    /// </summary>
    [JsonPropertyName("header")]
    public JoseHeader? Header { get; set; }

    [JsonPropertyName("encrypted_key")]
    public string? EncryptedKey { get; set; }

    [JsonPropertyName("iv")]
    public string? InitializationVector { get; set; }

    [JsonPropertyName("ciphertext")]
    public string? Ciphertext { get; set; }

    [JsonPropertyName("tag")]
    public string? AuthenticationTag { get; set; }

    [JsonPropertyName("aad")]
    public string? AdditionalAuthenticatedData { get; set; }



    public static JsonWebEncryption FromCompactSerializationFormat(string token)
    {
        var tokenParts = token.Split(".");

        if (tokenParts.Length != 5)
            throw new FormatException(nameof(token));

        var protectedHeaderString = tokenParts[0];
        JoseHeader? protectedHeader = null;
        if (!string.IsNullOrEmpty(protectedHeaderString))
        {
            protectedHeader = JsonSerializer.Deserialize<JoseHeader>(Base64UrlEncoder.Decode(protectedHeaderString));
        }

        var urlEncodedEncryptedKey = tokenParts[1];
        string? encryptedKey = null;
        if (!string.IsNullOrEmpty(urlEncodedEncryptedKey))
            encryptedKey = Base64UrlEncoder.Decode(urlEncodedEncryptedKey);

        var urlEncodedIv = tokenParts[2];
        string? iv = null;
        if (!string.IsNullOrEmpty(urlEncodedIv))
            iv = Base64UrlEncoder.Decode(urlEncodedIv);

        var urlEncodedCiphertext = tokenParts[3];
        string? ciphertext = null;
        if (!string.IsNullOrEmpty(urlEncodedCiphertext))
            ciphertext = Base64UrlEncoder.Decode(urlEncodedCiphertext);

        var urlEncodedAuthenticationTag = tokenParts[4];
        string? authenticationTag = null;
        if (!string.IsNullOrEmpty(urlEncodedAuthenticationTag))
            authenticationTag = Base64UrlEncoder.Decode(urlEncodedAuthenticationTag);

        return new JsonWebEncryption
        {
            ProtectedHeader = protectedHeader,
            EncryptedKey = encryptedKey,
            InitializationVector = iv,
            Ciphertext = ciphertext,
            AuthenticationTag = authenticationTag,
        };
    }
}
