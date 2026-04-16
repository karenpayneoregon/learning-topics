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

        var options = new JsonSerializerOptions
        {
            Converters = { new DictionaryStringObjectConverter() },
            WriteIndented = true
        };

        IDictionary<string, object> data = new Dictionary<string, object>
        {
            ["name"] = "Karen",
            ["age"] = 42,
            ["active"] = true,
            ["scores"] = new List<int> { 1, 2, 3 }
        };

        string json = JsonSerializer.Serialize(data, options);

        Console.WriteLine(json);

        IDictionary<string, object>? deserialized = JsonSerializer.Deserialize<IDictionary<string, object>>(json, options);
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }
}
