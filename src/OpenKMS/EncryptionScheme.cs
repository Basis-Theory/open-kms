using OpenKMS.Abstractions;

namespace OpenKMS;

public class EncryptionScheme
{
    public EncryptionScheme(string name, Type contentEncryptionHandlerType, Type? keyEncryptionHandlerType)
    {
        if (contentEncryptionHandlerType == null)
            throw new ArgumentNullException(nameof(contentEncryptionHandlerType));

        if (!typeof(IEncryptionHandler).IsAssignableFrom(contentEncryptionHandlerType))
            throw new ArgumentException("contentEncryptionHandlerType must implement IEncryptionHandler.");

        if (keyEncryptionHandlerType != null && !typeof(IEncryptionHandler).IsAssignableFrom(keyEncryptionHandlerType))
            throw new ArgumentException("keyEncryptionHandlerType must implement IEncryptionHandler.");

        Name = name ?? throw new ArgumentNullException(nameof(name));
        ContentEncryptionHandlerType = contentEncryptionHandlerType;
        KeyEncryptionHandlerType = keyEncryptionHandlerType;
    }

    public string Name { get; }

    public Type ContentEncryptionHandlerType { get; }

    public Type? KeyEncryptionHandlerType { get; }

}
