using OpenKMS.Options;
using OpenKMS.Structs;

namespace OpenKMS.Azure.KeyVault;

public class AzureKeyVaultEncryptionOptions : EncryptionHandlerOptions
{
    public override IList<EncryptionAlgorithm> ValidEncryptionAlgorithms { get; } = new[]
    {
        EncryptionAlgorithm.RSA1_5,
        EncryptionAlgorithm.RSA_OAEP
    };

    public override Dictionary<KeyType, int?[]> ValidKeyTypeSizes { get; } = new()
    {
        { KeyType.RSA, new[] { (int?)null, 2048, 3072, 4096 } },
        { KeyType.EC, new[] { (int?)null } },
        { KeyType.OCT, new[] { (int?)128, 192, 256 } },
    };

    public override int? KeySize { get; set; } = 2048;

    public override EncryptionAlgorithm EncryptionAlgorithm { get; set; } = EncryptionAlgorithm.RSA1_5;

    public override KeyType KeyType { get; set; } = KeyType.RSA;

    public string? EcCurveName { get; set; }

    public string KeyName { get; set; } = default!;

    public TimeSpan? KeyRotationInterval { get; set; }
}
