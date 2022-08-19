using OpenKMS.Exceptions;
using OpenKMS.Structs;

namespace OpenKMS;

public abstract class EncryptionHandlerOptions
{
    /// <summary>
    /// Check that the options are valid. Should throw an exception if things are not ok.
    /// </summary>
    public virtual void Validate()
    {
        if (!ValidEncryptionAlgorithms.Contains(EncryptionAlgorithm))
            throw new EncryptionAlgorithmNotSupportedException();

        if (!ValidKeyTypeSizes.ContainsKey(KeyType) ||  !ValidKeyTypeSizes[KeyType].Contains(KeySize))
            throw new KeyTypeNotSupportedException();
    }

    /// <summary>
    /// Checks that the options are valid for a specific scheme
    /// </summary>
    /// <param name="scheme">The scheme being validated.</param>
    public virtual void Validate(string scheme)
        => Validate();

    public abstract IList<EncryptionAlgorithm> ValidEncryptionAlgorithms { get; }
    public abstract Dictionary<KeyType, int?[]> ValidKeyTypeSizes { get; }

    public abstract EncryptionAlgorithm EncryptionAlgorithm { get; set; }

    public abstract KeyType KeyType { get; set; }

    public abstract int? KeySize { get; set; }

    public virtual IList<KeyOperation> KeyOperations { get; set; } = new List<KeyOperation>
    {
        KeyOperation.Decrypt,
        KeyOperation.Encrypt
    };
}
