using GlobbingApp1.Classes;
using GlobbingApp1.Models;
using Spectre.Console;
using System.Text.Json;

namespace GlobbingApp1;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        var options = JsonSerializer.Deserialize<GlobbingSetup>(await File.ReadAllTextAsync("GlobbingOptions.json"));
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        if (Directory.Exists(folder))
        {
            MatcherParameters matcherParameters = new()
            {
                Patterns = options.Include,
                ExcludePatterns = options.Exclude,
                ParentFolder = folder
            };

            var files = await Globbing.GetAsync(matcherParameters).ConfigureAwait(false);

            foreach (var file in files())
            {
                Console.WriteLine($"{Path.GetDirectoryName(file)?.Replace(folder, "").TrimStart('\\')}{Path.GetFileName(file)}");
            }
        }
        else
        {
            AnsiConsole.MarkupLine($"Folder [hotpink]{folder}[/] not found");
        }
        
        SpectreConsoleHelpers.ExitPrompt();
        
    }
}
