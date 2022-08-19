using Microsoft.Extensions.Options;
using OpenKMS.Abstractions;
using OpenKMS.Models;

namespace OpenKMS;

/// <summary>
/// An opinionated abstraction for implementing <see cref="IEncryptionHandler"/>.
/// </summary>
/// <typeparam name="TOptions">The type for the options used to configure the encryption handler.</typeparam>
public abstract class EncryptionHandler<TOptions> : IEncryptionHandler where TOptions : EncryptionHandlerOptions, new()
{
    /// <summary>
    /// Gets or sets the <see cref="EncryptionScheme"/> associated with this encryption handler.
    /// </summary>
    public EncryptionScheme Scheme { get; private set; } = default!;

    /// <summary>
    /// Gets or sets the options associated with this encryption handler.
    /// </summary>
    public TOptions Options { get; private set; } = default!;

    public abstract Task<EncryptResult> EncryptAsync(byte[] plaintext, byte[]? additionalAuthenticatedData = null,
        CancellationToken cancellationToken = default);

    public abstract Task<byte[]> DecryptAsync(JsonWebKey key, byte[] ciphertext, byte[]? iv = null,
        byte[]? authenticationTag = null, byte[]? additionalAuthenticatedData = null,
        CancellationToken cancellationToken = default);

    public abstract bool CanDecrypt(JsonWebKey key);

    /// <summary>
    /// Gets the <see cref="IOptionsMonitor{TOptions}"/> to detect changes to options.
    /// </summary>
    protected IOptionsMonitor<TOptions> OptionsMonitor { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="EncryptionHandler{TOptions}"/>.
    /// </summary>
    /// <param name="options">The monitor for the options instance.</param>
    protected EncryptionHandler(IOptionsMonitor<TOptions> options) => OptionsMonitor = options;

    /// <summary>
    /// Initialize the handler, resolve the options and validate them.
    /// </summary>
    /// <returns></returns>
    public async Task InitializeAsync(EncryptionScheme scheme)
    {
        Scheme = scheme ?? throw new ArgumentNullException(nameof(scheme));
        Options = OptionsMonitor.Get(Scheme.Name);

        await InitializeHandlerAsync();
    }

    /// <summary>
    /// Called after options/events have been initialized for the handler to finish initializing itself.
    /// </summary>
    /// <returns>A task</returns>
    protected virtual Task InitializeHandlerAsync() => Task.CompletedTask;
}
