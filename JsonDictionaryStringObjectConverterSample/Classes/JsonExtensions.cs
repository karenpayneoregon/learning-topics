using System.Text.Json;

namespace JsonDictionaryStringObjectConverterSample.Classes;

/// <summary>
/// Provides extension methods for working with JSON data, including conversions
/// between <see cref="JsonElement"/> and CLR objects, as well as processing
/// dictionaries containing JSON elements.
/// </summary>
/// <remarks>
/// This class includes utility methods to simplify handling and transforming JSON
/// data into .NET types, making it easier to work with deserialized JSON structures.
/// </remarks>
public static class JsonExtensions
{
    /// <summary>
    /// Converts a <see cref="JsonElement"/> into its corresponding CLR object representation.
    /// </summary>
    /// <param name="element">The <see cref="JsonElement"/> to convert.</param>
    /// <returns>
    /// A CLR object representing the JSON value. This can be a <see cref="Dictionary{TKey, TValue}"/> for JSON objects,
    /// a <see cref="List{T}"/> for JSON arrays, a <see cref="string"/> for JSON strings, a numeric type for JSON numbers,
    /// a <see cref="bool"/> for JSON boolean values, or <c>null</c> for JSON null values.
    /// </returns>
    /// <remarks>
    /// This method recursively processes the <see cref="JsonElement"/> to convert it into a CLR object,
    /// handling nested objects and arrays as needed.
    /// </remarks>
    public static object? ToClrObject(this JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                var dict = new Dictionary<string, object?>();
                foreach (var prop in element.EnumerateObject())
                {
                    dict[prop.Name] = prop.Value.ToClrObject();
                }
                return dict;

            case JsonValueKind.Array:
                var list = new List<object?>();
                foreach (var item in element.EnumerateArray())
                {
                    list.Add(item.ToClrObject());
                }
                return list;

            case JsonValueKind.String:
                return element.GetString();

            case JsonValueKind.Number:
                
                if (element.TryGetInt32(out int i))
                    return i;
                
                if (element.TryGetInt64(out long l))
                    return l;
                
                if (element.TryGetDouble(out double d))
                    return d;
                
                return element.GetDecimal();

            case JsonValueKind.True:
            case JsonValueKind.False:
                return element.GetBoolean();

            case JsonValueKind.Null:
                return null;

            default:
                return element.ToString();
        }
    }

    /// <summary>
    /// Converts an <see cref="IDictionary{TKey, TValue}"/> with <see cref="string"/> keys and <see cref="object"/> values
    /// into a <see cref="Dictionary{TKey, TValue}"/> with <see cref="string"/> keys and <see cref="object"/> values,
    /// resolving any <see cref="JsonElement"/> instances into their corresponding CLR objects.
    /// </summary>
    /// <param name="source">The source dictionary to convert.</param>
    /// <returns>A new <see cref="Dictionary{TKey, TValue}"/> with resolved CLR objects.</returns>
    /// <remarks>
    /// This method is particularly useful for processing deserialized JSON data where
    /// <see cref="JsonElement"/> instances need to be converted into their respective CLR representations.
    /// </remarks>
    public static Dictionary<string, object?> ToDictionary(this IDictionary<string, object> source)
    {
        var result = new Dictionary<string, object?>();

        foreach (var kvp in source)
        {
            if (kvp.Value is JsonElement element)
            {
                result[kvp.Key] = element.ToClrObject();
            }
            else
            {
                result[kvp.Key] = kvp.Value;
            }
        }

        return result;
    }
}