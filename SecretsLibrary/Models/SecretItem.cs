namespace SecretsLibrary.Models;

/// <summary>
/// Represents a secret item containing information about a file and its associated UserSecretsId.
/// </summary>
/// <remarks>
/// This class is used to store and manage data related to user secrets, including the file name
/// and the corresponding UserSecretsId extracted from project files.
/// </remarks>
public class SecretItem
{
    /// <summary>
    /// Gets the full path of the project file associated with the secret item.
    /// </summary>
    /// <value>
    /// A string representing the full path of the project file.
    /// </value>
    /// <remarks>
    /// This property is used to store the file path of a project file, typically with a ".csproj" extension,
    /// from which the <see cref="UserSecretsId"/> is extracted.
    /// </remarks>
    public string ProjectFileName { get; init; }
    /// <summary>
    /// Gets the UserSecretsId associated with the secret item.
    /// </summary>
    /// <value>
    /// A string representing the UserSecretsId extracted from the project file.
    /// </value>
    /// <remarks>
    /// This property holds the unique identifier used for managing user secrets in the associated project.
    /// It is typically defined in the project file and is essential for accessing user-specific configuration data.
    /// </remarks>
    public string UserSecretsId { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the secret item is valid.
    /// </summary>
    /// <remarks>
    /// A secret item is considered valid if the associated project folder exists
    /// within the UserSecrets directory.
    /// </remarks>
    public bool IsValid { get; set; }
}