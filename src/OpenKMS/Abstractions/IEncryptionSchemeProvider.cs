namespace OpenKMS.Abstractions;

public interface IEncryptionSchemeProvider
{
    Task<IEnumerable<EncryptionScheme>> GetSchemesAsync();
    Task<EncryptionScheme?> GetSchemeAsync(string schemeName);
    Task<EncryptionScheme?> GetDefaultEncryptSchemeAsync();

    void AddScheme(EncryptionScheme scheme);
    bool TryAddScheme(EncryptionScheme scheme);
    void RemoveScheme(string schemeName);
}
