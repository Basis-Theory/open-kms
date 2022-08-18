using OpenKMS.Abstractions;

namespace OpenKMS.Providers;

/// <summary>
/// Generate a random Guid that can be used to name encryption keys. />.
/// </summary>
public class GuidKeyNameProvider : IKeyNameProvider
{
    /// <inheritdoc />
    public string GetKeyName() => Guid.NewGuid().ToString();
}
