using OpenKMS.Structs;

namespace OpenKMS.Aes;

public class AesEncryptionOptions : EncryptionHandlerOptions
{
    public override IList<EncryptionAlgorithm> ValidEncryptionAlgorithms { get; } = new[]
    {
        EncryptionAlgorithm.A128CBC_HS256,
        EncryptionAlgorithm.A256CBC_HS512,
    };

    public override Dictionary<KeyType, int?[]> ValidKeyTypeSizes { get; } = new()
    {
        { KeyType.OCT, new[] { (int?)null, 128, 256 } }
    };

    public override EncryptionAlgorithm EncryptionAlgorithm { get; set; } = EncryptionAlgorithm.A256CBC_HS512;

    public override KeyType KeyType { get; set; } = KeyType.OCT;

    public override int? KeySize { get; set; } = 256;
}
