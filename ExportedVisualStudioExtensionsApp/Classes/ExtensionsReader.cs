using ExportedVisualStudioExtensionsApp.Models;
using System.Text.Json;

namespace ExportedVisualStudioExtensionsApp.Classes;

internal class ExtensionsReader
{
    /// <summary>
    /// Reads and parses a JSON file containing Visual Studio extensions and returns a sorted list of extensions.
    /// </summary>
    /// <param name="path">The file path to the JSON file containing the extension catalog.</param>
    /// <returns>
    /// A sorted <see cref="List{T}"/> of <see cref="VsExtension"/> objects if the file is successfully read and parsed; 
    /// otherwise, <c>null</c> if the catalog cannot be deserialized.
    /// </returns>
    /// <exception cref="System.IO.IOException">Thrown if an I/O error occurs while reading the file.</exception>
    /// <exception cref="System.Text.Json.JsonException">Thrown if the JSON content is invalid or cannot be deserialized.</exception>
    public static List<VsExtension>? Get(string path)
    {
        
        var json = File.ReadAllText(path);
        var catalog = JsonSerializer.Deserialize<ExtensionCatalog>(json, Options);
        return catalog?.Extensions.OrderBy(x => x.Name).ToList();

    }

    public static JsonSerializerOptions Options 
        => new()
        {
            PropertyNameCaseInsensitive = true
        };
}
