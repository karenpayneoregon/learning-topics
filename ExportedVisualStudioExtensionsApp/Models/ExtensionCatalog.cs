using System.Text.Json.Serialization;
#nullable disable
namespace ExportedVisualStudioExtensionsApp.Models;

public class ExtensionCatalog
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("extensions")]
    public List<VsExtension> Extensions { get; set; } = [];
}