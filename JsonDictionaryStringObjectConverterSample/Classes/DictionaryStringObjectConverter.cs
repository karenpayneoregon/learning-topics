using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonDictionaryStringObjectConverterSample.Classes;

/// <summary>
/// Provides a custom JSON converter for serializing and deserializing 
/// dictionaries with string keys and object values.
/// </summary>
/// <remarks>
/// This converter is specifically designed to handle <see cref="IDictionary{TKey, TValue}"/> 
/// where the key is of type <see cref="string"/> and the value is of type <see cref="object"/>.
/// It ensures proper serialization and deserialization of complex objects and nested structures.
/// </remarks>
public class DictionaryStringObjectConverter 
    : JsonConverter<IDictionary<string, object>>
{
    public override IDictionary<string, object> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dictionary = new Dictionary<string, object>();

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return dictionary;

            // Property name
            string propertyName = reader.GetString()!;

            reader.Read();

            dictionary[propertyName] = ReadValue(ref reader, options);
        }

        throw new JsonException();
    }

    /// <summary>
    /// Reads a value from the JSON reader and converts it to an appropriate .NET object.
    /// </summary>
    /// <param name="reader">The <see cref="Utf8JsonReader"/> to read the JSON data from.</param>
    /// <param name="options">The <see cref="JsonSerializerOptions"/> to use for deserialization.</param>
    /// <returns>
    /// The deserialized .NET object, which can be a <see cref="string"/>, <see cref="long"/>, 
    /// <see cref="double"/>, <see cref="bool"/>, <see cref="Dictionary{TKey, TValue}"/>, 
    /// <see cref="List{T}"/>, or <c>null</c>.
    /// </returns>
    /// <exception cref="JsonException">
    /// Thrown when the JSON token type is not supported or when deserialization fails.
    /// </exception>
    private object ReadValue(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        return (reader.TokenType switch
        {
            JsonTokenType.String => reader.GetString(),
            JsonTokenType.Number => reader.TryGetInt64(out var l) ? l : reader.GetDouble(),
            JsonTokenType.True => true,
            JsonTokenType.False => false,
            JsonTokenType.StartObject => JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options),
            JsonTokenType.StartArray => JsonSerializer.Deserialize<List<object>>(ref reader, options),
            JsonTokenType.Null => null,
            _ => throw new JsonException()
        })!;
    }

    /// <summary>
    /// Writes a dictionary with string keys and object values to a JSON writer.
    /// </summary>
    /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write the JSON data to.</param>
    /// <param name="value">The dictionary to serialize, where the keys are <see cref="string"/> and the values are <see cref="object"/>.</param>
    /// <param name="options">The <see cref="JsonSerializerOptions"/> to use for serialization.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="writer"/> or <paramref name="value"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// This method serializes the dictionary into a JSON object, ensuring that each key-value pair
    /// is properly written to the JSON output. The values are serialized using the provided <paramref name="options"/>.
    /// </remarks>
    public override void Write(Utf8JsonWriter writer, IDictionary<string, object> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var kvp in value)
        {
            writer.WritePropertyName(kvp.Key);
            JsonSerializer.Serialize(writer, kvp.Value, options);
        }

        writer.WriteEndObject();
    }
}