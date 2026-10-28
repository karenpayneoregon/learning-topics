using System.Text.Json;
using System.Text.Json.Serialization;
using JsonConvertersSampleLibrary.Extensions;

namespace JsonConvertersSampleLibrary.Converters;

/// <summary>
/// Provides functionality to mask credit card numbers when serializing to JSON.
/// </summary>
public class CreditCardMaskConverter : JsonConverter<string>
{
    /// <summary>
    /// Reads and returns the string value from the JSON reader.
    /// </summary>
    /// <param name="reader">The <see cref="Utf8JsonReader"/> to read from.</param>
    /// <param name="typeToConvert">The type to convert the JSON to.</param>
    /// <param name="options">Options to control the behavior during reading.</param>
    /// <returns>The string value read from the JSON.</returns>
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString()!;
    }

    /// <summary>
    /// Writes the masked credit card number to the JSON writer.
    /// </summary>
    /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write to.</param>
    /// <param name="value">The credit card number to be masked and written.</param>
    /// <param name="options">Options to control the behavior during writing.</param>
    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.MaskCreditCardNumber());
    }
}