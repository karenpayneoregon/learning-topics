﻿using System.Text.Json;

namespace SecretsLibrary.Models;
#nullable disable
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
    /// Gets or sets the contents of the secret item as a dictionary of key-value pairs.
    /// </summary>
    /// <remarks>
    /// This property holds the deserialized data from the secrets.json file associated with the UserSecretsId.
    /// Each key-value pair represents a secret stored in the file.
    /// </remarks>
    //public Dictionary<string, string> Contents { get; set; }
    public JsonDocument Contents { get; set; }

}