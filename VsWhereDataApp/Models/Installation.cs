using System.Text.Json.Serialization;
#nullable disable
namespace VsWhereDataApp.Models;

public class Installation
{
    [JsonPropertyName("instanceId")]
    public string InstanceId { get; set; }

    [JsonPropertyName("installDate")]
    public DateTime InstallDate { get; set; }

    [JsonPropertyName("installationName")]
    public string InstallationName { get; set; }

    [JsonPropertyName("installationPath")]
    public string InstallationPath { get; set; }

    [JsonPropertyName("installationVersion")]
    public Version InstallationVersion { get; set; }

    [JsonPropertyName("productId")]
    public string ProductId { get; set; }

    [JsonPropertyName("productPath")]
    public string ProductPath { get; set; }

    [JsonPropertyName("state")]
    public long State { get; set; }

    [JsonPropertyName("isComplete")]
    public bool IsComplete { get; set; }

    [JsonPropertyName("isLaunchable")]
    public bool IsLaunchable { get; set; }

    [JsonPropertyName("isPrerelease")]
    public bool IsPrerelease { get; set; }

    [JsonPropertyName("isRebootRequired")]
    public bool IsRebootRequired { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("channelId")]
    public string ChannelId { get; set; }

    [JsonPropertyName("channelUri")]
    public string ChannelUri { get; set; }

    [JsonPropertyName("enginePath")]
    public string EnginePath { get; set; }

    [JsonPropertyName("installedChannelId")]
    public string InstalledChannelId { get; set; }

    [JsonPropertyName("installedChannelUri")]
    public string InstalledChannelUri { get; set; }

    [JsonPropertyName("releaseNotes")]
    public string ReleaseNotes { get; set; }

    [JsonPropertyName("thirdPartyNotices")]
    public string ThirdPartyNotices { get; set; }

    [JsonPropertyName("updateDate")]
    public DateTime UpdateDate { get; set; }

    [JsonPropertyName("catalog")]
    public Catalog Catalog { get; set; }

    [JsonPropertyName("properties")]
    public Properties Properties { get; set; }
}