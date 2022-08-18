using System.Text.Json;
using System.Text.Json.Serialization;
using OpenKMS.Structs;

namespace OpenKMS.Converters;

public class EncryptionAlgorithmJsonConverter : JsonConverter<EncryptionAlgorithm>
{
    public override EncryptionAlgorithm Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        return new EncryptionAlgorithm(value!);
;
    }

    public override void Write(Utf8JsonWriter writer, EncryptionAlgorithm value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
