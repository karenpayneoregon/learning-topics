using System.Xml.Linq;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace SecretsLibrary.Classes;

/// <summary>
/// Provides functionality to read and manage user secrets information from a .csproj file.
/// </summary>
/// <remarks>
/// This class is designed to parse a .csproj file to extract and manage the <c>UserSecretsId</c> element.
/// It also provides methods to verify the existence of the user secrets folder and project-specific secrets folder.
/// </remarks>
public class ProjectSecretsReader
{
    private readonly string _csprojFilePath;
    private readonly XDocument _projectXml;

    public ProjectSecretsReader(string csprojFilePath)
    {
        Continue = true;

        if (string.IsNullOrWhiteSpace(csprojFilePath))
        {
            Continue = false;
        }

        if (!File.Exists(csprojFilePath))
        {
            Continue = false;
        }

        if (Continue)
        {
            _csprojFilePath = csprojFilePath;
            _projectXml = XDocument.Load(_csprojFilePath);
        }

    }

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="ProjectSecretsReader"/> should continue processing.
    /// </summary>
    /// <value>
    /// <c>true</c> if the processing should continue; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    /// This property is initialized based on the validity of the provided .csproj file path.
    /// If the path is null, empty, or the file does not exist, this property is set to <c>false</c>.
    /// </remarks>
    public bool Continue { get; set; }

    /// <summary>
    /// Determines whether the project-specific secrets folder exists within the user secrets directory.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the project-specific secrets folder exists; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// The method checks for the existence of a folder named after the <c>UserSecretsId</c> value
    /// within the user secrets directory.
    /// </remarks>
    public bool ProjectFolderExists() 
        => Directory.Exists(Path.Combine(Utilities.SecretsFolder, GetValue()!));


    /// <summary>
    /// Checks whether the <c>UserSecretsId</c> element exists in the .csproj file.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the <c>UserSecretsId</c> element is present; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method determines the presence of the <c>UserSecretsId</c> element in the .csproj file,
    /// which is used to identify the project-specific user secrets folder.
    /// </remarks>
    public bool Exists() 
        => GetUserSecretsElement() != null;

    /// <summary>
    /// Retrieves the value of the <c>UserSecretsId</c> element from the .csproj file.
    /// </summary>
    /// <returns>
    /// The value of the <c>UserSecretsId</c> element if it exists; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method extracts the value of the <c>UserSecretsId</c> element, which is used
    /// to identify the project-specific user secrets folder.
    /// </remarks>
    public string? GetValue()
    {
        var element = GetUserSecretsElement();
        return element?.Value;
    }


    /// <summary>
    /// Retrieves the <c>UserSecretsId</c> element from the .csproj file.
    /// </summary>
    /// <returns>
    /// An <see cref="XElement"/> representing the <c>UserSecretsId</c> element if it exists; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method searches the .csproj file for the <c>UserSecretsId</c> element within the <c>PropertyGroup</c>
    /// elements. The <c>UserSecretsId</c> element is used to identify the project-specific user secrets folder.
    /// </remarks>
    private XElement? GetUserSecretsElement() =>
        _projectXml
            .Descendants("PropertyGroup")
            .Elements("UserSecretsId")
            .FirstOrDefault();
}