using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace OpenKMS.EntityFrameworkCore;

public class EncryptionConverter : ValueConverter<string, string>
{
    public EncryptionConverter(Expression<Func<string, string>> convertToProviderExpression,
        Expression<Func<string, string>> convertFromProviderExpression,
        ConverterMappingHints mappingHints = null) :
        base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
    {
    }
}
