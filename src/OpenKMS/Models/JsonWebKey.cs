using System.Text.Json.Serialization;
using OpenKMS.Structs;

namespace OpenKMS.Models;

public class JsonWebKey
{
    [JsonPropertyName("kty")]
    public KeyType KeyType { get; set; }

    [JsonPropertyName("use")]
    public string? PublicKeyUse { get; set; }

    [JsonPropertyName("key_ops")]
    public IReadOnlyCollection<KeyOperation>? KeyOperations { get; set; }

    [JsonPropertyName("alg")]
    public EncryptionAlgorithm? Algorithm { get; set; }

    [JsonPropertyName("kid")]
    public string? KeyId { get; set; }

    [JsonPropertyName("x5u")]
    public Uri? X509Url { get; set; }

    [JsonPropertyName("x5c")]
    public IList<string>? X509CertificateChain { get; set; }

    [JsonPropertyName("x5t")]
    public string? X509CertificateSha1Thumbprint { get; set; }

    [JsonPropertyName("x5t#256")]
    public string? X509CertificateSha256Thumbprint { get; set; }

    [JsonIgnore]
    public KeyState? State { get; set; }

    [JsonPropertyName("crv")]
    public string? Curve { get; set; }

    [JsonPropertyName("n")]
    public byte[]? N { get; set; }

    [JsonPropertyName("e")]
    public byte[]? E { get; set; }

    [JsonPropertyName("dp")]
    public byte[]? Dp { get; set; }

    [JsonPropertyName("dq")]
    public byte[]? Dq { get; set; }

    [JsonPropertyName("qi")]
    public byte[]? Qi { get; set; }

    [JsonPropertyName("p")]
    public byte[]? P { get; set; }

    [JsonPropertyName("q")]
    public byte[]? Q { get; set; }

    [JsonPropertyName("x")]
    public byte[]? X { get; set; }

    [JsonPropertyName("y")]
    public byte[]? Y { get; set; }

    [JsonPropertyName("d")]
    public byte[]? D { get; set; }

    [JsonPropertyName("k")]
    public byte[]? K { get; set; }
}
