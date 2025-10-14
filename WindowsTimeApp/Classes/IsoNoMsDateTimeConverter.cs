using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WindowsTimeApp.Classes;

/// <summary>
/// Provides a custom JSON converter for <see cref="DateTime"/> objects, 
/// ensuring that the serialized and deserialized format excludes milliseconds 
/// and adheres to the ISO 8601 standard.
/// </summary>
public class IsoNoMsDateTimeConverter : JsonConverter<DateTime>
{
    private const string Format = "yyyy-MM-dd'T'HH:mm:ssK"; // tweak as needed

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => DateTime.Parse(reader.GetString()!, null, DateTimeStyles.RoundtripKind);

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(Format));
}