using JsonConverterSamplesApp.Classes.Core;
using JsonConverterSamplesApp.Models;
using System.Text.Json;

namespace JsonConverterSamplesApp.Classes;

internal class Samples
{
    /// <summary>
    /// Demonstrates the serialization of a data object with properties formatted using custom 
    /// double serialization string formats.
    /// </summary>
    /// <remarks>
    /// This method creates an instance of <see cref="JsonConverterSamplesApp.Models.DataClass"/> 
    /// with properties decorated with <see cref="JsonSampleConverterLibrary.Classes.DoubleSerializationStringFormatAttribute"/> 
    /// to specify the desired formatting for double values. The object is serialized to JSON using 
    /// <see cref="System.Text.Json.JsonSerializer"/> with predefined options, and the resulting JSON 
    /// is displayed using <see cref="JsonConverterSamplesApp.Classes.Core.SpectreConsoleHelpers.PresentJson(string)"/>.
    /// </remarks>
    public static void DoubleSerializationStringFormatExample()
    {
        
        SpectreConsoleHelpers.PrintPink();
        
        var data = new DataClass() { PropTwoPlaces = 10.5678, PropFivePlaces = 3.14159267 };
        var serialized = JsonSerializer.Serialize(data, Options);
        
        SpectreConsoleHelpers.PresentJson(serialized);
        Console.WriteLine("\n");
        Console.WriteLine(ObjectDumper.Dump(data));
        
    }

    private static JsonSerializerOptions Options => new() { WriteIndented = true };
}