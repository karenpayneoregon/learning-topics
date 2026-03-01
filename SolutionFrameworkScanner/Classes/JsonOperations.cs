using SolutionFrameworkScanner.Models;
using System.Text.Json;

namespace SolutionFrameworkScanner.Classes;
internal class JsonOperations
{
    private static string jsonFileName => "FrameworkReport.json";
    /// <summary>
    /// Serializes a list of grouped items by framework into a JSON file.
    /// </summary>
    /// <param name="groupedByFramework">
    /// A list of <see cref="Models.GroupedItems"/> representing projects grouped by their target frameworks.
    /// </param>
    /// <remarks>
    /// The serialized JSON is written to a file named "FrameworkReport.json" in the current directory.
    /// The JSON output is formatted with indentation for readability.
    /// </remarks>
    /// <exception cref="System.ArgumentNullException">
    /// Thrown if <paramref name="groupedByFramework"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="System.IO.IOException">
    /// Thrown if an I/O error occurs while writing to the file.
    /// </exception>
    public static void ToJson(List<GroupedItems> groupedByFramework)
    {
        var json = JsonSerializer.Serialize(groupedByFramework, IndentedOption);
        File.WriteAllText(jsonFileName, json);
    }

    /// <summary>
    /// Reads and deserializes the framework report from a JSON file.
    /// </summary>
    /// <returns>
    /// A list of <see cref="Models.GroupedItems"/> representing 
    /// the projects grouped by their respective frameworks.
    /// </returns>
    /// <remarks>
    /// This method reads the content of the JSON file specified by the <c>jsonFileName</c> field
    /// and deserializes it into a list of <see cref="Models.GroupedItems"/>.
    /// If the file is empty or the deserialization fails, an empty list is returned.
    /// </remarks>
    /// <exception cref="IOException">
    /// Thrown if an error occurs while reading the JSON file.
    /// </exception>
    /// <exception cref="JsonException">
    /// Thrown if the JSON content is invalid or cannot be deserialized.
    /// </exception>
    public static List<GroupedItems> FromJson()
    {
        // Assumes that the file exists (never do this in production code without proper error handling)
        var json = File.ReadAllText(jsonFileName);
        return JsonSerializer.Deserialize<List<GroupedItems>>(json) ?? [];
    }

    /// <summary>
    /// Gets the <see cref="JsonSerializerOptions"/> configured to format JSON output with indentation.
    /// </summary>
    /// <value>78
    /// A <see cref="JsonSerializerOptions"/> instance with <see cref="JsonSerializerOptions.WriteIndented"/> set to <c>true</c>.
    /// </value>
    /// <remarks>
    /// This property is used to ensure that JSON output is human-readable by including indentation.
    /// </remarks>
    public static JsonSerializerOptions IndentedOption => new() { WriteIndented = true };
    
}
