using System.Collections.Immutable;
using Amazon.KeyManagementService;
using Microsoft.Extensions.Options;

namespace OpenKMS.AWS.KeyManagementService.Rsa;

public class AwsRsaEncryptionHandler : BaseAwsEncryptionHandler<AwsRsaEncryptionOptions>
{
    public AwsRsaEncryptionHandler(AmazonKeyManagementServiceClient kmsClient,
        IOptionsMonitor<AwsRsaEncryptionOptions> options) : base(kmsClient, options)
    {
    }

    protected override ImmutableList<KeySpec> SupportedKeySpecs { get; } = new List<KeySpec>()
    {
        KeySpec.RSA_2048, KeySpec.RSA_3072, KeySpec.RSA_4096
    }.ToImmutableList();
}
