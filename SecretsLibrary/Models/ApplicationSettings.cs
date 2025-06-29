namespace SecretsLibrary.Models;
public class ApplicationSettings
{
    /// <summary>
    /// Gets or sets the path to the Visual Studio folder.
    /// </summary>
    /// <remarks>
    /// This property is required and typically represents the root directory
    /// used for scanning and managing UserSecrets or related configurations.
    /// </remarks>
    public required string VisualStudioFolder { get; set; }
}
