using Amazon.KeyManagementService;
using OpenKMS.Structs;

namespace OpenKMS.AWS.KeyManagementService.Rsa;

public class AwsRsaEncryptionOptions : BaseAwsEncryptionOptions
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

    public override string KeyName { get; set; } = default!;

    protected internal override KeySpec GetKeySpec() => KeySize switch
    {
        2048 => KeySpec.RSA_2048,
        3072 => KeySpec.RSA_3072,
        4096 => KeySpec.RSA_4096,
        null => KeySpec.RSA_2048,
        _ => throw new ArgumentOutOfRangeException(),
    };
}
