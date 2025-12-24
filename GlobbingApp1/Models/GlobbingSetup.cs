using System.Text.Json.Serialization;

namespace GlobbingApp1.Models;


/// <summary>
/// Represents the configuration for file globbing operations, including patterns to include and exclude.
/// </summary>
/// <remarks>
/// This class is used to define the include and exclude patterns for file matching.
/// The patterns are specified as arrays of strings and are serialized/deserialized using JSON.
/// </remarks>
public class GlobbingSetup
{
    /// <summary>
    /// Gets or sets the array of file patterns to include during file matching operations.
    /// </summary>
    /// <remarks>
    /// The patterns specified in this property determine which files will be included
    /// in the file matching process. These patterns support globbing syntax and are
    /// serialized/deserialized using JSON.
    /// </remarks>
    [JsonPropertyName("include")]
    public string[] Include { get; set; } = [];

    /// <summary>
    /// Gets or sets the array of patterns to exclude during file matching operations.
    /// </summary>
    /// <remarks>
    /// These patterns are used to specify files or directories that should be excluded
    /// from the results of file globbing. The patterns are serialized/deserialized using JSON.
    /// </remarks>
    [JsonPropertyName("exclude")]
    public string[] Exclude { get; set; } = [];
}