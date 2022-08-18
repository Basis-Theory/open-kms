using Microsoft.Extensions.Options;
using OpenKMS.Abstractions;
using OpenKMS.Options;

namespace OpenKMS.Providers;

public class EncryptionSchemeProvider : IEncryptionSchemeProvider
{
    private readonly IDictionary<string, EncryptionScheme> _schemes;
    private readonly EncryptionOptions _options;

    /// <summary>
    /// Creates an instance of <see cref="EncryptionSchemeProvider"/>
    /// using the specified <paramref name="options"/>,
    /// </summary>
    /// <param name="options">The <see cref="EncryptionOptions"/> options.</param>
    public EncryptionSchemeProvider(IOptions<EncryptionOptions> options)
        : this(options, new Dictionary<string, EncryptionScheme>(StringComparer.Ordinal))
    {
    }

    /// <summary>
    /// Creates an instance of <see cref="EncryptionSchemeProvider"/>
    /// using the specified <paramref name="options"/> and <paramref name="schemes"/>.
    /// </summary>
    /// <param name="options">The <see cref="EncryptionOptions"/> options.</param>
    /// <param name="schemes">The dictionary used to store encryption schemes.</param>
    protected EncryptionSchemeProvider(IOptions<EncryptionOptions> options,
        IDictionary<string, EncryptionScheme> schemes)
    {
        _options = options.Value;

        _schemes = schemes ?? throw new ArgumentNullException(nameof(schemes));

        foreach (var builder in _options.Schemes)
        {
            var scheme = builder.Build();
            AddScheme(scheme);
        }
    }


    private readonly object _lock = new object();

    private Task<EncryptionScheme?> GetDefaultSchemeAsync()
        => _options.DefaultScheme != null
            ? GetSchemeAsync(_options.DefaultScheme)
            : Task.FromResult<EncryptionScheme?>(null);

    public Task<IEnumerable<EncryptionScheme>> GetSchemesAsync()
    {
        return Task.FromResult(_schemes.Select(s => s.Value));
    }

    public Task<EncryptionScheme?> GetSchemeAsync(string schemeName)
    {
        if (!_schemes.TryGetValue(schemeName, out var scheme))
            return Task.FromResult<EncryptionScheme?>(null);

        return Task.FromResult<EncryptionScheme?>(scheme);
    }

    public Task<EncryptionScheme?> GetDefaultEncryptSchemeAsync()
    {
        return _options.DefaultEncryptScheme != null
            ? GetSchemeAsync(_options.DefaultEncryptScheme)
            : GetDefaultSchemeAsync();
    }

    /// <summary>
    /// Registers a scheme for use by <see cref="IEncryptionService"/>.
    /// </summary>
    /// <param name="scheme">The scheme.</param>
    public virtual void AddScheme(EncryptionScheme scheme)
    {
        if (_schemes.ContainsKey(scheme.Name))
        {
            throw new InvalidOperationException("Scheme already exists: " + scheme.Name);
        }

        lock (_lock)
        {
            if (!TryAddScheme(scheme))
            {
                throw new InvalidOperationException("Scheme already exists: " + scheme.Name);
            }
        }
    }

    /// <summary>
    /// Registers a scheme for use by <see cref="IEncryptionService"/>.
    /// </summary>
    /// <param name="scheme">The scheme.</param>
    /// <returns>true if the scheme was added successfully.</returns>
    public virtual bool TryAddScheme(EncryptionScheme scheme)
    {
        if (_schemes.ContainsKey(scheme.Name))
            return false;

        lock (_lock)
        {
            if (_schemes.ContainsKey(scheme.Name))
            {
                return false;
            }

            _schemes[scheme.Name] = scheme;
            return true;
        }
    }

    /// <summary>
    /// Removes a scheme, preventing it from being used by <see cref="IEncryptionService"/>.
    /// </summary>
    /// <param name="name">The name of the authenticationScheme being removed.</param>
    public void RemoveScheme(string name)
    {
        if (!_schemes.ContainsKey(name))
        {
            return;
        }

        lock (_lock)
        {
            if (_schemes.ContainsKey(name))
            {
                _schemes.Remove(name);
            }
        }
    }
}
