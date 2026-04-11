namespace ProcessOrdersApp.Models.Configuration;
/// <summary>
/// Represents the configuration settings for file operations within the application.
/// </summary>
/// <remarks>
/// This class is used to define and manage file-related configuration properties, such as the file path.
/// It is typically populated from the application's configuration files (e.g., appsettings.json) 
/// and injected into dependent services using dependency injection.
/// </remarks>
public class FileConfiguration
{
    public string FilePath { get; set; } = string.Empty;
}
