using Microsoft.Extensions.DependencyInjection;
using OpenKMS.Abstractions;

namespace OpenKMS.Builders;

public class EncryptionBuilder
{
    public EncryptionBuilder(IServiceCollection services)
        => Services = services;

    /// <summary>
    /// The services being configured.
    /// </summary>
    protected virtual IServiceCollection Services { get; }

    private EncryptionBuilder AddSchemeHelper<TContentEncryptionOptions, TContentEncryptionHandler, TKeyEncryptionOptions, TKeyEncryptionHandler>(string encryptionScheme, Action<TContentEncryptionOptions>? configureContentEncryptionOptions, Action<TKeyEncryptionOptions>? configureKeyEncryptionOptions)
        where TContentEncryptionOptions : EncryptionHandlerOptions, new()
        where TContentEncryptionHandler : class, IEncryptionHandler
        where TKeyEncryptionOptions : EncryptionHandlerOptions, new()
        where TKeyEncryptionHandler : class, IEncryptionHandler
    {
        var builder = new EncryptionSchemeBuilder(encryptionScheme, Services)
        {
            ContentEncryptionHandlerType = typeof(TContentEncryptionHandler),
            KeyEncryptionHandlerType = typeof(TKeyEncryptionHandler)
        };

        Services.Configure<EncryptionOptions>(o =>
        {
            o.AddScheme(builder);
        });
        if (configureContentEncryptionOptions != null)
        {
            Services.Configure(encryptionScheme, configureContentEncryptionOptions);
        }
        Services.AddOptions<TContentEncryptionOptions>(encryptionScheme).Validate(o => {
            o.Validate(encryptionScheme);
            return true;
        });

        if (configureKeyEncryptionOptions != null)
        {
            Services.AddOptions<TKeyEncryptionOptions>(encryptionScheme)
                .Configure(configureKeyEncryptionOptions);
        }
        Services.AddOptions<TKeyEncryptionOptions>(encryptionScheme).Validate(o => {
            o.Validate(encryptionScheme);
            return true;
        });
        Services.AddTransient<TContentEncryptionHandler>();
        Services.AddTransient<TKeyEncryptionHandler>();
        return this;
    }

    /// <summary>
    /// Adds a <see cref="EncryptionScheme"/> which can be used by <see cref="IEncryptionService"/>.
    /// </summary>
    /// <typeparam name="TContentEncryptionOptions">The <see cref="EncryptionHandlerOptions"/> type to configure the content encryptionhandler."/>.</typeparam>
    /// <typeparam name="TContentEncryptionHandler">The <see cref="EncryptionHandler{TOptions}"/> used to handle this scheme.</typeparam>
    /// <typeparam name="TKeyEncryptionOptions">The <see cref="EncryptionHandlerOptions"/> type to configure the key encryption handler."/>.</typeparam>
    /// <typeparam name="TKeyEncryptionHandler">The <see cref="EncryptionHandler{TOptions}"/> used to handle this scheme.</typeparam>
    /// <param name="encryptionScheme">The name of this scheme.</param>
    /// <param name="configureContentEncryptionOptions">Used to configure the content encryption scheme options.</param>
    /// <param name="configureKeyEncryptionOptions">Used to configure the key encryption scheme options.</param>
    /// <returns>The builder.</returns>
    public virtual EncryptionBuilder AddScheme<TContentEncryptionOptions, TContentEncryptionHandler, TKeyEncryptionOptions, TKeyEncryptionHandler>(string encryptionScheme,
        Action<TContentEncryptionOptions>? configureContentEncryptionOptions, Action<TKeyEncryptionOptions>? configureKeyEncryptionOptions)
        where TContentEncryptionOptions : EncryptionHandlerOptions, new()
        where TContentEncryptionHandler : EncryptionHandler<TContentEncryptionOptions>
        where TKeyEncryptionOptions : EncryptionHandlerOptions, new()
        where TKeyEncryptionHandler : EncryptionHandler<TKeyEncryptionOptions>
        => AddSchemeHelper<TContentEncryptionOptions, TContentEncryptionHandler, TKeyEncryptionOptions, TKeyEncryptionHandler>(encryptionScheme, configureContentEncryptionOptions, configureKeyEncryptionOptions);

    public virtual EncryptionBuilder AddScheme(string schemeName, Action<EncryptionSchemeBuilder> configureBuilder)
    {
        var builder = new EncryptionSchemeBuilder(schemeName, Services);
        configureBuilder(builder);

        Services.Configure<EncryptionOptions>(o =>
        {
            o.AddScheme(builder);
        });

        return this;
    }
}
