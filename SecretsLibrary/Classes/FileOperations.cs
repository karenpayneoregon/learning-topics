using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using SecretsLibrary.Models;
using Serilog;
using Spectre.Console;

namespace SecretsLibrary.Classes;

/// <summary>
/// Provides utility methods for file operations related to scanning directories for project files
/// and managing UserSecretsId values.
/// </summary>
/// <remarks>
/// This class includes methods for recursively scanning directories to locate project files,
/// extracting UserSecretsId values, and writing the results to a JSON file. It also provides
/// functionality for filtering and validating secret items.
/// </remarks>
public partial class FileOperations
{

    /// <summary>
    /// Retrieves the application settings from the "appsettings.json" configuration file.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="ApplicationSettings"/> containing the application settings
    /// defined in the "appsettings.json" file.
    /// </returns>
    /// <remarks>
    /// This method uses the <see cref="ConfigurationBuilder"/>
    /// to load the "appsettings.json" file and map its contents to an <see cref="ApplicationSettings"/> object.
    /// Ensure that the "appsettings.json" file is present in the application's root directory.
    /// </remarks>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the "appsettings.json" file is not found.
    /// </exception>
    /// <exception cref="JsonException">
    /// Thrown if the contents of the "appsettings.json" file cannot be deserialized into an <see cref="ApplicationSettings"/> object.
    /// </exception>
    public static ApplicationSettings GetApplicationSettings()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        return config.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>();
    }
    /// <summary>
    /// Processes the specified directory to locate project files and extract UserSecretsId values.
    /// </summary>
    /// <param name="directory">The root directory to scan for project files.</param>
    /// <param name="secretItems">A list to store the extracted <see cref="SecretItem"/> objects containing file names and UserSecretsId values.</param>
    /// <remarks>
    /// This method recursively scans the directory for files with a ".csproj" extension, extracts the UserSecretsId values,
    /// and adds them to the provided list. If access to a directory is denied or an error occurs during processing,
    /// appropriate error messages are logged and displayed.
    /// </remarks>
    /// <exception cref="UnauthorizedAccessException">Thrown when access to a directory is denied.</exception>
    /// <exception cref="Exception">Thrown when an unexpected error occurs during directory processing.</exception>
    public static void ScanDirectory(string directory, List<SecretItem> secretItems)
    {
        try
        {
            foreach (var file in Directory.GetFiles(directory, "*.csproj", SearchOption.AllDirectories))
            {
                var userSecretsId = ExtractUserSecretsId(file);
                if (string.IsNullOrEmpty(userSecretsId)) continue;

                if (Directory.Exists(Path.Combine(Utilities.SecretsFolder, userSecretsId)))
                {

                    var (json, exists) = ReadSecretFile(userSecretsId);

                    if (exists)
                    {
                        var rawJson = string.Join(Environment.NewLine, json).Trim('\uFEFF').Trim();

                        try
                        {
                            var jsonDocument = JsonDocument.Parse(rawJson);

                            secretItems.Add(new SecretItem
                            {
                                ProjectFileName = file,
                                UserSecretsId = userSecretsId,
                                Contents = jsonDocument
                            });

                        }
                        catch (JsonException ex)
                        {
                                
                            Log.Warning(ex, $"Could not parse secrets.json for {userSecretsId}");
                            var fallbackDoc = JsonDocument.Parse("{\"error\": \"Invalid JSON content\"}");

                            secretItems.Add(new SecretItem
                            {
                                ProjectFileName = file,
                                UserSecretsId = userSecretsId,
                                Contents = fallbackDoc
                            });
                        }
                    }
                    else
                    {
                        var emptyDoc = JsonDocument.Parse("{\"info\": \"No secrets found\"}");
                        secretItems.Add(new SecretItem
                        {
                            ProjectFileName = file,
                            UserSecretsId = userSecretsId,
                            Contents = emptyDoc
                        });
                    }


                }

            }
        }
        catch (UnauthorizedAccessException unauthorized)
        {
            Log.Error(unauthorized, $"In {nameof(ScanDirectory)}");
            AnsiConsole.MarkupLine($"[deeppink3]Access denied:[/] {directory}");
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"In {nameof(ScanDirectory)}");
            AnsiConsole.MarkupLine($"[deeppink3]Error processing[/] {directory}: {ex.Message}");
        }
    }

    /// <summary>
    /// Extracts the UserSecretsId value from the specified project file.
    /// </summary>
    /// <param name="filePath">The full path of the project file to process.</param>
    /// <returns>
    /// The extracted UserSecretsId value if found; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method reads the content of the specified project file, searches for a UserSecretsId
    /// using a regular expression, and returns the matched value if successful.
    /// If an error occurs while reading the file, the error is logged, and <c>null</c> is returned.
    /// </remarks>
    /// <exception cref="Exception">Thrown when an unexpected error occurs while reading the file.</exception>
    private static string ExtractUserSecretsId(string filePath)
    {
        try
        {

            var content = File.ReadAllText(filePath);
            var match = GenerateUserSecretsIdRegex().Match(content);

            return match.Success ? match.Groups[1].Value : null;

        }
        catch (Exception ex)
        {
            Log.Error(ex, $"In {nameof(ExtractUserSecretsId)}");
            return null;
        }
    }

    /// <summary>
    /// Reads the secrets file associated with the specified UserSecretsId.
    /// </summary>
    /// <param name="userSecretsId">The UserSecretsId used to locate the secrets file.</param>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    /// <item>
    /// <description>An array of strings representing the contents of the secrets file, or an empty array if the file does not exist.</description>
    /// </item>
    /// <item>
    /// <description>A boolean indicating whether the secrets file exists and is not empty.</description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This method checks if the directory corresponding to the UserSecretsId exists, and if so, verifies the presence of a "secrets.json" file.
    /// If the file exists, it reads its contents and determines whether it is empty.
    /// </remarks>
    public static (string[] json, bool exists) ReadSecretFile(string userSecretsId)
    {
        var directory = Utilities.ProjectFolder(userSecretsId);

        if (Directory.Exists(directory))
        {
                
            var file = Path.Combine(directory, "secrets.json");
            if (!File.Exists(file)) return ([], false);

            var lines = File.ReadAllLines(file);
            var isEmpty = Utilities.IsEmptyJsonObject(File.ReadAllText(file));

            return (lines, !isEmpty);
        }

        return ([], false);
    }

    public static JsonSerializerOptions Indented => new() { WriteIndented = true };


    [GeneratedRegex(@"<UserSecretsId>(.*?)</UserSecretsId>", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex GenerateUserSecretsIdRegex();
}