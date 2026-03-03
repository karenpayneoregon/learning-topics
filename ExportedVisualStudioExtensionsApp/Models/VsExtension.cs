namespace ExportedVisualStudioExtensionsApp.Models;
#nullable disable
using System.Text.Json.Serialization;

public class VsExtension
{
    [JsonPropertyName("vsixId")]
    public string VsixId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("moreInfoUrl")]
    public string MoreInfoUrl { get; set; }

    [JsonPropertyName("downloadUrl")]
    public string DownloadUrl { get; set; }

    public override string ToString() => Name;
}
