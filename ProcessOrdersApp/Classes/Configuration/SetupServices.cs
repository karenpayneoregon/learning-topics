using Microsoft.Extensions.Options;
using ProcessOrdersApp.Models.Configuration;

namespace ProcessOrdersApp.Classes.Configuration;
/// <summary>
/// Provides methods to configure and retrieve application settings, including connection strings, 
/// entity configuration, and file configuration, from the application's configuration files.
/// </summary>
/// <remarks>
/// This class is responsible for initializing and managing the application's settings by utilizing 
/// dependency injection to retrieve configuration data. It interacts with the application's 
/// configuration models such as <see cref="ConnectionStrings"/>, <see cref="EntityConfiguration"/>, 
/// and <see cref="FileConfiguration"/>.
/// </remarks>
internal class SetupServices
{
    
    private readonly EntityConfiguration _settings;
    private readonly ConnectionStrings _options;
    private readonly FileConfiguration _fileSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetupServices"/> class.
    /// </summary>
    /// <param name="options">
    /// An instance of <see cref="IOptions{TOptions}"/> containing the application's connection strings.
    /// </param>
    /// <param name="settings">
    /// An instance of <see cref="IOptions{TOptions}"/> containing the entity configuration settings.
    /// </param>
    /// <param name="fileSettings">
    /// An instance of <see cref="IOptions{TOptions}"/> containing the file configuration settings.
    /// </param>
    public SetupServices(
        IOptions<ConnectionStrings> options, 
        IOptions<EntityConfiguration> settings, 
        IOptions<FileConfiguration> fileSettings)
    {
        _fileSettings = fileSettings.Value;
        _options = options.Value;
        _settings = settings.Value;
    }
    
    /// <summary>
    /// Read connection strings from appsettings
    /// </summary>
    public void GetConnectionStrings()
    {
        DataConnections.Instance.MainConnection = _options.MainConnection;
    }
    public void GetEntitySettings()
    {
        EntitySettings.Instance.CreateNew = _settings.CreateNew;
    }

    public void GetFileSettings()
    {
        FileSettings.Instance.FileName = _fileSettings.FilePath;
    }
}
