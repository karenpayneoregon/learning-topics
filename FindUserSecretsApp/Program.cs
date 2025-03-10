using System.Text.Json;
using System.Text.RegularExpressions;
using FindUserSecretsApp.Classes;
using Serilog;
using FindUserSecretsApp.Models;

namespace FindUserSecretsApp;

internal partial class Program
{
    static void Main()
    {
        if (!Utilities.SecretsFolderExists)
        {
            AnsiConsole.MarkupLine("[red]UserSecrets folder not found.[/]");
            return;
        }

        string rootDirectory = @"C:\OED\DotnetLand\VS2022";
        string outputFile = @"UserSecretsProjects.json";

        List<SecretItem> secretItems = [];

        try
        {
            AnsiConsole.MarkupLine("[yellow]Scanning for UserSecretsId values...[/]");
            ScanDirectory(rootDirectory, secretItems);

            // Serialize the results to a JSON file
            string json = JsonSerializer.Serialize(secretItems, Indented);
            File.WriteAllText(outputFile, json);

            Console.WriteLine($"Scan complete. Results saved in: {outputFile}");
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"In {nameof(Main)}");
            Console.WriteLine($"Error: {ex.Message}");
        }

        AnsiConsole.MarkupLine("[yellow]Done[/]");
        Console.ReadLine();

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
    private static void ScanDirectory(string directory, List<SecretItem> secretItems)
    {
        try
        {
            foreach (var file in Directory.GetFiles(directory, "*.csproj", SearchOption.AllDirectories))
            {
                string userSecretsId = ExtractUserSecretsId(file);
                if (string.IsNullOrEmpty(userSecretsId)) continue;
                secretItems.Add(new SecretItem
                {
                    FileName = file,
                    UserSecretsId = userSecretsId
                });

                AnsiConsole.MarkupLine($"[cyan]{file}[/] - UserSecretsId: [yellow]{userSecretsId}[/]");
            }
        }
        catch (UnauthorizedAccessException unauthorized)
        {
            Log.Error(unauthorized,$"In {nameof(ScanDirectory)}");
            Console.WriteLine($"Access denied: {directory}");
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"In {nameof(ScanDirectory)}");
            Console.WriteLine($"Error processing {directory}: {ex.Message}");
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
            Match match = GenerateUserSecretsIdRegex().Match(content);
            return match.Success ? match.Groups[1].Value : null;
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"In {nameof(ExtractUserSecretsId)}");
            Console.WriteLine($"Error reading {filePath}: {ex.Message}");
            return null;
        }
    }
}