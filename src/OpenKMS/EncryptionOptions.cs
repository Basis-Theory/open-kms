using OpenKMS.Builders;

namespace OpenKMS.Options;

/// <summary>
/// Options to configure Encryption
/// </summary>
public class EncryptionOptions
{
        private readonly IList<EncryptionSchemeBuilder> _schemes = new List<EncryptionSchemeBuilder>();

        /// <summary>
        /// Returns the schemes in the order they were added (important for request handling priority)
        /// </summary>
        public IEnumerable<EncryptionSchemeBuilder> Schemes => _schemes;

        /// <summary>
        /// Maps schemes by name.
        /// </summary>
        public IDictionary<string, EncryptionSchemeBuilder> SchemeMap { get; } = new Dictionary<string, EncryptionSchemeBuilder>(StringComparer.Ordinal);

        /// <summary>
        /// Builds and adds an <see cref="EncryptionScheme"/>.
        /// </summary>
        /// <param name="builder">The builder being added.</param>
        public void AddScheme(EncryptionSchemeBuilder builder)
        {
            if (builder.Name == null)
                throw new ArgumentNullException(nameof(builder.Name));
            if (SchemeMap.ContainsKey(builder.Name))
                throw new InvalidOperationException($"Scheme already exists: {builder.Name}");

            _schemes.Add(builder);
            SchemeMap[builder.Name] = builder;
        }

        /// <summary>
        /// Used as the fallback default scheme for all the other defaults.
        /// </summary>
        public string? DefaultScheme { get; set; }

        /// <summary>
        /// Used as the default scheme.
        /// </summary>
        public string? DefaultEncryptScheme { get; set; }
}
