using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenKMS.Abstractions;
using OpenKMS.Builders;
using OpenKMS.Providers;

namespace OpenKMS.Extensions.DependencyInjection;

public static class EncryptionServiceCollectionExtensions
{
    public static EncryptionBuilder AddEncryption(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.TryAddScoped<IEncryptionService, EncryptionService>();
        services.TryAddScoped<IEncryptionHandlerProvider, EncryptionHandlerProvider>();
        services.TryAddSingleton<IEncryptionSchemeProvider, EncryptionSchemeProvider>();

        return new EncryptionBuilder(services);
    }

    public static EncryptionBuilder AddEncryption(this IServiceCollection services, Action<EncryptionOptions> configureOptions)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configureOptions == null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }

        var builder = services.AddEncryption();
        services.Configure(configureOptions);
        return builder;
    }
}
