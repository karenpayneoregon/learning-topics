using FindUserSecretsApp.Classes;
using Serilog;
using FindUserSecretsApp.Models;

namespace FindUserSecretsApp;

internal partial class Program
{
    private static async Task Main()
    {


        if (!Utilities.SecretsFolderExists)
        {
            AnsiConsole.MarkupLine("[red]UserSecrets folder not found.[/]");
            return;
        }
        
        const string rootDirectory = @"C:\OED\DotnetLand\VS2022";
        const string outputFile = @"UserSecretsProjects.json";

        List<SecretItem> secretItems = [];

        try
        {

            AnsiConsole.MarkupLine("[yellow]Scanning...[/]");
            FileOperations.ScanDirectory(rootDirectory, secretItems);
            List<SecretItem> results = await FileOperations.WriteSecretsFileAsync(outputFile, secretItems);

            Console.Clear();

            if (results.Count >0)
            {
                Console.WriteLine(ObjectDumper.Dump(results));
                Console.WriteLine();
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No user secrets found.[/]");
            }


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
    /// Reads a secret file associated with the specified identifier and displays its content.
    /// </summary>
    /// <param name="secretIdentifier">
    /// The identifier used to locate the secret file.
    /// </param>
    /// <remarks>
    /// This method utilizes <see cref="FileOperations.ReadSecretFile(string)"/> to retrieve the content of the secret file.
    /// If the file exists, its content is displayed line by line in the console.
    /// </remarks>
    private static void ReadAndDisplaySecretFile(string secretIdentifier)
    {
        var (json, exists) = FileOperations.ReadSecretFile(secretIdentifier);

        if (exists)
        {
            Console.WriteLine(string.Join(Environment.NewLine, json));
        }
    }
}