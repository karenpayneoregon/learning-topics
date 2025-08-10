namespace HelpDeskApplication.Classes;


/// <summary>
/// Represents the required directories configuration for the application.
/// </summary>
/// <remarks>
/// This class is used to define and validate the directories that are essential for the application to function correctly.
/// It includes properties that specify the paths to these directories, which are validated at runtime to ensure their existence.
/// </remarks>
public class RequiredDirectories
{
    [DirectoryExists]
    public required string SecretsDirectory { get; set; }
}