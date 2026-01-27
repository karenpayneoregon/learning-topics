using System.Text.Json;
using System.Text.Json.Serialization;

namespace UpperCaseFirstCharConverterApp.Classes;

/// <summary>
/// A custom JSON converter that ensures the first character of a string 
/// is converted to uppercase during serialization and deserialization.
/// </summary>
/// <remarks>
/// This converter is particularly useful for scenarios where string values 
/// need to follow a specific capitalization format, such as names or titles.
/// </remarks>
public class UpperCaseFirstCharConverter : JsonConverter<string>
{
    public override bool CanConvert(Type typeToConvert) 
        => typeToConvert == typeof(string);

    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) 
        => (reader.GetString() ?? string.Empty).CapitalizeFirstLetter();

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value ?? "Null");
    }
}