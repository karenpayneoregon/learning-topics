#nullable disable
using System.Text.Json.Serialization;

namespace VsWhereDataApp.Models;

public class Properties
{
    [JsonPropertyName("campaignId")]
    public string CampaignId { get; set; }

    [JsonPropertyName("channelManifestId")]
    public string ChannelManifestId { get; set; }

    [JsonPropertyName("nickname")]
    public string Nickname { get; set; }

    [JsonPropertyName("setupEngineFilePath")]
    public string SetupEngineFilePath { get; set; }
}