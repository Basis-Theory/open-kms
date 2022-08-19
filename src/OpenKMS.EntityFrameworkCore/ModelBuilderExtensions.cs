using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OpenKMS.Abstractions;
using OpenKMS.Extensions;
using OpenKMS.Models;

namespace OpenKMS.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    public static ModelBuilder UseEncryption(this ModelBuilder modelBuilder, IEncryptionService encryptionService)
    {
        if (encryptionService == null)
            return modelBuilder;

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                var attribute = property.PropertyInfo?.GetCustomAttribute<EncryptedAttribute>(false);
                if (attribute == null) continue;

                if (property.ClrType == typeof(string))
                {
                    property.SetValueConverter(
                        new EncryptionConverter(
                            m => encryptionService.Encrypt(m, attribute.Scheme).ToCompactSerializationFormat(),
                            s => encryptionService.DecryptString(JsonWebEncryption.FromCompactSerializationFormat(s))
                        )
                    );
                }
            }
        }

        return modelBuilder;
    }
}
