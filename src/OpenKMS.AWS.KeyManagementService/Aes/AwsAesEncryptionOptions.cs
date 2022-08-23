using Amazon.KeyManagementService;
using OpenKMS.Structs;

namespace OpenKMS.AWS.KeyManagementService;

public class AwsAesEncryptionOptions: BaseAwsEncryptionOptions
{
    public override IList<EncryptionAlgorithm> ValidEncryptionAlgorithms { get; } = new List<EncryptionAlgorithm>
    {
        EncryptionAlgorithm.A256GCM
    };

    public override Dictionary<KeyType, int?[]> ValidKeyTypeSizes { get; } = new Dictionary<KeyType, int?[]>
    {
        { KeyType.OCT, new int?[] { null, 256 } }
    };
    public override EncryptionAlgorithm EncryptionAlgorithm { get; set; } = EncryptionAlgorithm.A256GCM;
    public override KeyType KeyType { get; set; } = KeyType.OCT;
    public override int? KeySize { get; set; } = 256;

    public override string KeyName { get; set; } = default!;
    protected internal override KeySpec GetKeySpec() => KeySpec.SYMMETRIC_DEFAULT;
}
