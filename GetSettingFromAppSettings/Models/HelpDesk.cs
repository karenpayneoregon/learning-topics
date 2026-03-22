namespace GetSettingFromAppSettings.Models;

/// <summary>
/// Represents the HelpDesk configuration settings.
/// </summary>
/// <remarks>
/// This class is used to map the "HelpDesk" section of the application's configuration.
/// It contains properties for storing contact information such as phone number and email address.
/// </remarks>
public class HelpDesk
{
    public string? Phone { get; set; }
    public string? Email { get; set; }
}