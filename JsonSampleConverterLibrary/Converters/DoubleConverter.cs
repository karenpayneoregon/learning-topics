using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonSampleConverterLibrary.Converters;

/// <summary>
/// Represents a custom JSON converter for <see cref="double"/> values, allowing formatting during serialization.
/// </summary>
/// <remarks>
/// This converter enables the serialization of <see cref="double"/> values using a specified format string.
/// </remarks>
public class DoubleConverter(string format) : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Not needed for the example.
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(format));
    }
}