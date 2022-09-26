using Amazon.KeyManagementService;
using OpenKMS.Constants;
using OpenKMS.Structs;

namespace OpenKMS.AWS.KeyManagementService.Extensions;

public static class EncryptionAlgorithmExtensions
{
    public static EncryptionAlgorithmSpec ToEncryptionAlgorithmSpec(this EncryptionAlgorithm algorithm) =>
        algorithm.ToString() switch
        {
            EncryptionAlgorithms.RSA_OAEP => EncryptionAlgorithmSpec.RSAES_OAEP_SHA_1,
            EncryptionAlgorithms.RSA_OAEP_256 => EncryptionAlgorithmSpec.RSAES_OAEP_SHA_256,
            _ => throw new ArgumentOutOfRangeException(),
        };
}
