using JsonDictionaryStringObjectConverterSample.Classes;
using JsonDictionaryStringObjectConverterSample.Classes.Core;
using Spectre.Console;
using System.Text.Json;


namespace JsonDictionaryStringObjectConverterSample;
internal partial class Program
{
    static void Main(string[] args)
    {
        SpectreConsoleHelpers.PinkPill(Justify.Left, "Converter");
        Console.WriteLine();

        var options = new JsonSerializerOptions
        {
            Converters = { new DictionaryStringObjectConverter() },
            WriteIndented = true
        };

        var data = Dictionary();

        string json = JsonSerializer.Serialize(data, options);

        SpectreConsoleHelpers.WriteJson(json);
        Console.WriteLine();
        
        IDictionary<string, object>? deserialized = 
            JsonSerializer.Deserialize<IDictionary<string, object>>(json, options);

        Dictionary<string, object?> clean = deserialized!.ToDictionary();

        foreach (var kvp in clean)
        {
            AnsiConsole.MarkupLine($"[cyan]{kvp.Key,-6}[/]: {FormatValue(kvp.Value).ConsoleEscape()}");
            
            if (kvp.Key != "scores") continue;
            
            // The "scores" key is expected to contain a list of integers
            if (kvp.Value is List<object> scoresList && scoresList.All(item => item is JsonElement { ValueKind: JsonValueKind.Number }))
            {
                List<int> scores = scoresList.Select(item => ((JsonElement)item).GetInt32()).ToList();
                AnsiConsole.MarkupLine($"    [hotpink2](List<int>):[/] [cyan]{string.Join(", ", scores)}[/]");
            }
        }

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
        
    }

    /// <summary>
    /// Creates and returns a dictionary containing data.
    /// </summary>
    /// <returns>
    /// An <see cref="IDictionary{TKey, TValue}"/> where the keys are strings and the values are objects.
    /// The dictionary includes sample data such as a name, age, active status, and a list of scores.
    /// </returns>System.Text.Json.JsonException: ''S' is an invalid start of a value. Path: $ | LineNumber: 0 | BytePositionInLine: 0.'

    private static IDictionary<string, object> Dictionary() =>
        new Dictionary<string, object>
        {
            ["name"] = "Karen",
            ["age"] = 42,
            ["active"] = true,
            ["scores"] = (List<int>)[1, 2, 3]
        };

    /// <summary>
    /// Formats the provided value into a string representation.
    /// </summary>
    /// <param name="value">The value to format. Can be a list, dictionary, or any other object.</param>
    /// <returns>
    /// A string representation of the value:
    /// - Lists are formatted as "[ item1, item2, ... ]".
    /// - Dictionaries are formatted as "{ key1: value1, key2: value2, ... }".
    /// - Other objects are converted to their string representation using <see cref="object.ToString"/>.
    /// - If the value is <c>null</c>, the string "null" is returned.
    /// </returns>
    private static string FormatValue(object? value)
    {
        if (value is List<object?> list)
        {
            return $"[ {string.Join(", ", list)} ]";
        }

        if (value is Dictionary<string, object?> dict)
        {
            return $"{{ {string.Join(", ", dict.Select(k => $"{k.Key}: {k.Value}"))} }}";
        }

        return value?.ToString() ?? "null";
    }
}
