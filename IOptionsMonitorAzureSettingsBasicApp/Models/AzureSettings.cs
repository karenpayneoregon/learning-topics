namespace IOptionsMonitorAzureSettingsBasicApp.Models;
#nullable disable
public class AzureSettings
{
    public const string Settings = "AzureSettings";
    public bool UseAdal { get; set; }
    public string Tenant { get; set; }
    public string TenantName { get; set; }
    public string TenantId { get; set; }
    public string Audience { get; set; }
    public string ClientId { get; set; }
    public string GraphClientId { get; set; }
    public string GraphClientSecret { get; set; }
    public string SignUpSignInPolicyId { get; set; }
    public string AzureGraphVersion { get; set; }
    public string MicrosoftGraphVersion { get; set; }
    public string AadInstance { get; set; }
}