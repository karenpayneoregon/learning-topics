using System.Text.Json.Serialization;
using JsonSampleConverterLibrary.Converters;

namespace JsonSampleConverterLibrary.Classes;

/// <summary>
/// Specifies the string format to be used during the serialization of <see cref="double"/> properties.
/// </summary>
/// <remarks>
/// This attribute is applied to properties of type <see cref="double"/> to define a custom string format
/// for their JSON serialization. It leverages a custom JSON converter to enforce the specified format.
/// </remarks>
[AttributeUsage(AttributeTargets.Property)]
public class DoubleSerializationStringFormatAttribute(string format) : JsonConverterAttribute
{
    public override JsonConverter CreateConverter(Type typeToConvert) => typeToConvert != typeof(double) ? throw new ArgumentException($"This converter only works with double, and it was provided {typeToConvert.Name}.") : new DoubleConverter(format);
}