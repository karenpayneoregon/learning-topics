using System.Text.Json;
using System.Text.Json.Serialization;
using JsonConvertersSampleLibrary.Extensions;

namespace JsonConvertersSampleLibrary.Converters;
/// <summary>
/// Provides a custom JSON converter for Social Security Numbers (SSNs) that masks the SSN value during serialization.
/// </summary>
public class SocialSecurityMaskConverter : JsonConverter<string>
{
    /// <summary>
    /// Reads and converts the JSON to a string value.
    /// </summary>
    /// <param name="reader">The <see cref="Utf8JsonReader"/> to read from.</param>
    /// <param name="typeToConvert">The type of the object to convert.</param>
    /// <param name="options">Options to control the behavior during reading.</param>
    /// <returns>The string value read from the JSON.</returns>
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) 
        => reader.GetString()!;

    /// <summary>
    /// Writes a masked Social Security Number (SSN) to the JSON.
    /// </summary>
    /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write to.</param>
    /// <param name="value">The SSN value to be masked and written.</param>
    /// <param name="options">Options to control the behavior during writing.</param>
    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.MaskSsn());
    }
}