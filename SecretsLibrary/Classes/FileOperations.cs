using System.Text.Json;
using System.Text.RegularExpressions;
using SecretsLibrary.Models;
using Serilog;
using Spectre.Console;

namespace SecretsLibrary.Classes
{
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
                            var data = StringSanitizer.StripQuotesAndBreaks(string.Join(Environment.NewLine, json));
                            secretItems.Add(new SecretItem
                            {
                                ProjectFileName = file,
                                UserSecretsId = userSecretsId,
                                Contents = data.RemoveExtraWhitespace(),
                            });
                        }
  
                    }
                    else
                    {
                        secretItems.Add(new SecretItem
                        {
                            ProjectFileName = file,
                            UserSecretsId = userSecretsId,
                            Contents = "[No secrets found]",
                        });
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
        /// Writes a list of valid secret items to a JSON file asynchronously.
        /// </summary>
        /// <param name="filePath">The file path where the JSON file will be written.</param>
        /// <param name="secretItems">The list of <see cref="SecretItem"/> objects to be written to the file.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the list of valid <see cref="SecretItem"/> objects 
        /// that were successfully written to the file.
        /// </returns>
        /// <remarks>
        /// This method filters the provided list of secret items to include only those that are valid, based on the existence of their 
        /// associated project folders. The filtered list is serialized into JSON format and written to the specified file.
        /// </remarks>
        /// <exception cref="Exception">
        /// Thrown if an error occurs during the file writing process. The error is logged using Serilog.
        /// </exception>
        public static async Task<List<SecretItem>> WriteSecretsFileAsync(string filePath, List<SecretItem> secretItems)
        {

            foreach (var item in secretItems)
            {
                item.IsValid = Directory.Exists(Utilities.ProjectFolder(item.UserSecretsId));
            }

            secretItems = secretItems.Where(x => x.IsValid).ToList();


            try
            {
                var json = JsonSerializer.Serialize(secretItems, Indented);
                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"In {nameof(WriteSecretsFileAsync)}");
            }

            return secretItems;
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
                if (File.Exists(file))
                {
                    var isEmpty = Utilities.IsEmptyJsonObject(File.ReadAllText(file));
                    return (File.ReadAllLines(file), !isEmpty);
                }
            }

            return ([], false);
        }

        public static JsonSerializerOptions Indented => new() { WriteIndented = true };


        [GeneratedRegex(@"<UserSecretsId>(.*?)</UserSecretsId>", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex GenerateUserSecretsIdRegex();
    }
}
