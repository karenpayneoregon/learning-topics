using System.Text.Json.Serialization;
#nullable disable
namespace VsWhereDataApp.Models;

public class Catalog
{
    [JsonPropertyName("buildBranch")]
    public string BuildBranch { get; set; }

    [JsonPropertyName("buildVersion")]
    public string BuildVersion { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("localBuild")]
    public string LocalBuild { get; set; }

    [JsonPropertyName("manifestName")]
    public string ManifestName { get; set; }

    [JsonPropertyName("manifestType")]
    public string ManifestType { get; set; }

    [JsonPropertyName("productDisplayVersion")]
    public string ProductDisplayVersion { get; set; }

    [JsonPropertyName("productLine")]
    public string ProductLine { get; set; }

    [JsonPropertyName("productLineVersion")]
    public string ProductLineVersion { get; set; }

    [JsonPropertyName("productMilestone")]
    public string ProductMilestone { get; set; }

    [JsonPropertyName("productMilestoneIsPreRelease")]
    public string ProductMilestoneIsPreRelease { get; set; }

    [JsonPropertyName("productName")]
    public string ProductName { get; set; }

    [JsonPropertyName("productPatchVersion")]
    public string ProductPatchVersion { get; set; }

    [JsonPropertyName("productPreReleaseMilestoneSuffix")]
    public string ProductPreReleaseMilestoneSuffix { get; set; }

    [JsonPropertyName("productSemanticVersion")]
    public string ProductSemanticVersion { get; set; }

    [JsonPropertyName("requiredEngineVersion")]
    public string RequiredEngineVersion { get; set; }

    // This field only exists in one object but should still be declared
    [JsonPropertyName("productReleaseNameSuffix")]
    public string ProductReleaseNameSuffix { get; set; }
}