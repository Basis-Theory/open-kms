using System.Text.Json;
using System.Text.Json.Serialization;
using OpenKMS.Structs;

namespace OpenKMS.Converters;

public class NullableCompressionAlgorithmJsonConverter : JsonConverter<CompressionAlgorithm?>
{
    public override CompressionAlgorithm? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        return string.IsNullOrEmpty(value) ? (CompressionAlgorithm?)null : new CompressionAlgorithm(value);
;
    }

    public override void Write(Utf8JsonWriter writer, CompressionAlgorithm? value, JsonSerializerOptions options)
    {
        if (value == null)
            return;

        writer.WriteStringValue(value.ToString());
    }
}
