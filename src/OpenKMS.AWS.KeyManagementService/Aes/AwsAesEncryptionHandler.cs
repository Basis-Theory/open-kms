using System.Collections.Immutable;
using Amazon.KeyManagementService;
using Microsoft.Extensions.Options;

namespace OpenKMS.AWS.KeyManagementService.Aes;

public class AwsAesEncryptionHandler : BaseAwsEncryptionHandler<AwsAesEncryptionOptions>
{
    public AwsAesEncryptionHandler(AmazonKeyManagementServiceClient kmsClient,
        IOptionsMonitor<AwsAesEncryptionOptions> options) : base(kmsClient, options)
    {
    }

    protected override ImmutableList<KeySpec> SupportedKeySpecs { get; } = new List<KeySpec>
    {
        KeySpec.SYMMETRIC_DEFAULT
    }.ToImmutableList();
}
