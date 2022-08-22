using OpenKMS.Structs;

namespace OpenKMS.AWS.KeyManagementService;

public class AwsRsaEncryptionOptions: EncryptionHandlerOptions
{
    public override IList<EncryptionAlgorithm> ValidEncryptionAlgorithms { get; } = new List<EncryptionAlgorithm>
    {
        EncryptionAlgorithm.RSA_OAEP,
        EncryptionAlgorithm.RSA_OAEP_256,
    };

    public override Dictionary<KeyType, int?[]> ValidKeyTypeSizes { get; } = new Dictionary<KeyType, int?[]>
    {
        { KeyType.RSA, new int?[] { 2048, 3072, 4096 } }
    };
    public override EncryptionAlgorithm EncryptionAlgorithm { get; set; } = EncryptionAlgorithm.RSA_OAEP;
    public override KeyType KeyType { get; set; } = KeyType.RSA;
    public override int? KeySize { get; set; } = 2048;

    public string KeyName { get; set; } = default!;
}
