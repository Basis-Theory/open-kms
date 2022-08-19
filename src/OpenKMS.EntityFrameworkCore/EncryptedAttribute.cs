namespace OpenKMS.EntityFrameworkCore;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class EncryptedAttribute : Attribute
{
    public string Scheme { get; init; }

    public EncryptedAttribute() { }

    public EncryptedAttribute(string scheme)
    {
        Scheme = scheme;
    }
}
