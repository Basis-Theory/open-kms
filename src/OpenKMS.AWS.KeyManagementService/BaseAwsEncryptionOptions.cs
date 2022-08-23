using Amazon.KeyManagementService;

namespace OpenKMS.AWS.KeyManagementService;

public abstract class BaseAwsEncryptionOptions : EncryptionHandlerOptions
{

    public abstract string KeyName { get; set; }

    protected internal abstract KeySpec GetKeySpec();
}
