using SecretsLibrary.Classes;
using SecretsLibrary.Converters;
using SecretsLibrary.Models;
using Serilog;
using System.Text.Json;


namespace FindUserSecretsApp;

internal partial class Program
{
    private static async Task Main()
    {

        if (!Utilities.SecretsFolderExists)
        {
            AnsiConsole.MarkupLine("[red]UserSecrets folder not found.[/]");
            Console.ReadLine();
            return;
        }

        AnsiConsole.MarkupLine($"[yellow]Secrets folder[/] [cyan]{Utilities.SecretsFolder}[/]");

        string visualStudioFolder = GetApplicationSettings().VisualStudioFolder;
        AnsiConsole.MarkupLine($"[yellow]Root directory[/] [cyan]{visualStudioFolder}[/]");


        const string outputFile = @"UserSecretsProjects.json";
        const string projectFile = "UserProjects.json";

        if (!Directory.Exists(visualStudioFolder))
        {
            
            AnsiConsole.MarkupLine("[red]Root directory not found![/]");
            Console.ReadLine();
            return;
        }

        List<SecretItem> secretItems = [];

        try
        {
            /*
             * See Status from Spectre.Console
             */
            AnsiConsole.Status()
                .Start("[cyan]Scanning...[/]", ctx =>
                {
                    ctx.Spinner(Spinner.Known.Star);
                    ctx.SpinnerStyle(Style.Parse("deeppink2"));
                    ScanDirectory(visualStudioFolder, secretItems);
                });

            if (secretItems.Count >0)
            {

                var projects = secretItems.Select(x => new SecretData(x.UserSecretsId, x.ProjectFileName)).ToList();
                await File.WriteAllTextAsync(projectFile, JsonSerializer.Serialize(projects, JsonSerializerOptions));

                var options = JsonSerializerOptions;
                options.Converters.Add(new SecretItemConverter());

                var json = JsonSerializer.Serialize(secretItems, options);
                await File.WriteAllTextAsync(outputFile, json);

                AnsiConsole.MarkupLine($"[yellow]Scan complete. Results saved in:[/] [cyan]{outputFile}[/] [yellow]with a count of[/] [cyan]{secretItems.Count}[/]");
                Log.Information($"Scan complete. Results saved in: {outputFile} with a count of {secretItems.Count}");

            } else
            {
                AnsiConsole.MarkupLine("[red]No UserSecrets found in the specified directory.[/]");
                Log.Information("No UserSecrets found.");

            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"In {nameof(Main)}");
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        Console.ReadLine();

    }

    private static JsonSerializerOptions JsonSerializerOptions => new() { WriteIndented = true };
}