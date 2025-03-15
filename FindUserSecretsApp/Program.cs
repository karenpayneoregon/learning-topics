using Serilog;
using SecretsLibrary.Classes;
using SecretsLibrary.Models;

using static SecretsLibrary.Classes.FileOperations;


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
            ScanDirectory(rootDirectory, secretItems);
            List<SecretItem> results = await WriteSecretsFileAsync(outputFile, secretItems);

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
}