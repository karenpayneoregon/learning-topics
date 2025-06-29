using SecretsLibrary.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SecretsLibrary.Converters;

/// <summary>
/// Provides custom JSON serialization and deserialization for the <see cref="SecretItem"/> class.
/// </summary>
/// <remarks>
/// This converter is responsible for writing the properties of a <see cref="SecretItem"/> object
/// into JSON format. The reading functionality is not implemented as it is not required for the current use case.
/// </remarks>
public class SecretItemConverter : JsonConverter<SecretItem>
{
    public override SecretItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => throw new NotImplementedException(); 

    /// <summary>
    /// Writes the JSON representation of the specified <see cref="SecretItem"/> object.
    /// </summary>
    /// <param name="writer">The <see cref="Utf8JsonWriter"/> used to write the JSON output.</param>
    /// <param name="value">The <see cref="SecretItem"/> instance to serialize into JSON.</param>
    /// <param name="options">The <see cref="JsonSerializerOptions"/> to use for serialization.</param>
    /// <remarks>
    /// This method serializes the properties of the <see cref="SecretItem"/> object, including
    /// <see cref="SecretItem.ProjectFileName"/>, <see cref="SecretItem.UserSecretsId"/>, 
    /// <see cref="SecretItem.IsValid"/>, and <see cref="SecretItem.Contents"/>.
    /// </remarks>
    public override void Write(Utf8JsonWriter writer, SecretItem value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString(nameof(SecretItem.ProjectFileName), value.ProjectFileName);
        writer.WriteString(nameof(SecretItem.UserSecretsId), value.UserSecretsId);
        writer.WritePropertyName(nameof(SecretItem.Contents));
        value.Contents.WriteTo(writer);

        writer.WriteEndObject();
    }
}

