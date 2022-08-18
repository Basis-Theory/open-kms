namespace OpenKMS.Abstractions;

/// <summary>
/// Defines a provider which can be used to generate encryption key names.
/// </summary>
public interface IKeyNameProvider
{
    /// <summary>
    /// Generates a key name
    /// </summary>
    /// <returns>A string key name.</returns>
    string GetKeyName();
}
