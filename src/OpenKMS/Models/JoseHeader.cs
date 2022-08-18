using System.Text.Json.Serialization;
using OpenKMS.Converters;
using OpenKMS.Structs;

namespace OpenKMS.Models;

public class JoseHeader
{
    /// <summary>
    /// the Header Parameter identifies the cryptographic algorithm used
    /// to encrypt or determine the value of the CEK.  The encrypted content
    /// is not usable if the "alg" value does not represent a supported
    /// algorithm, or if the recipient does not have a key that can be used
    /// with that algorithm.
    /// </summary>
    [JsonPropertyName("alg")]
    [JsonConverter(typeof(NullableEncryptionAlgorithmJsonConverter))]
    public EncryptionAlgorithm? KeyEncryptionAlgorithm { get; set; }

    /// <summary>
    /// The "enc" (encryption algorithm) Header Parameter identifies the
    /// content encryption algorithm used to perform authenticated encryption
    /// on the plaintext to produce the ciphertext and the Authentication
    /// Tag.  This algorithm MUST be an AEAD algorithm with a specified key
    /// length.  The encrypted content is not usable if the "enc" value does
    /// not represent a supported algorithm.
    /// </summary>
    [JsonPropertyName("enc")]
    [JsonConverter(typeof(EncryptionAlgorithmJsonConverter))]
    public EncryptionAlgorithm ContentEncryptionAlgorithm { get; set; }

    [JsonPropertyName("zip")]
    [JsonConverter(typeof(NullableCompressionAlgorithmJsonConverter))]
    public CompressionAlgorithm? CompressionAlgorithm { get; set; }

    [JsonPropertyName("jku")]
    public Uri? JwkSetUrl { get; set; }

    [JsonPropertyName("jwk")]
    public JsonWebKey? JsonWebKey { get; set; }

    [JsonPropertyName("kid")]
    public string? KeyId { get; set; }

    [JsonPropertyName("x5u")]
    public Uri? X509Url { get; set; }

    [JsonPropertyName("x5c")]
    public string? X509CertificateChain { get; set; }

    [JsonPropertyName("x5t")]
    public string? X509CertificateSha1Thumbprint { get; set; }

    [JsonPropertyName("x5t#S256")]
    public string? X509CertificateSha256Thumbprint { get; set; }

    [JsonPropertyName("typ")]
    public string? Type { get; set; }

    [JsonPropertyName("cty")]
    public string? ContentType { get; set; }

    [JsonPropertyName("crit")]
    public IList<string>? CriticalHeaderParameters { get; set; }
}
