using GlobbingApp1.Classes;
using GlobbingApp1.Models;
using Spectre.Console;
using System.Text.Json;

namespace GlobbingApp1;
internal partial class Program
{
    private static async Task Main(string[] args)
    {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var list = await Globbing.GetDuplicatesTask(folder);
        Console.WriteLine(list.Count);
        foreach (var (index, item) in list.Index())
        {
            try
            {
                if (File.Exists(item.FullName))
                {
                    File.Delete(item.FullName);
                }else
                {
                    AnsiConsole.MarkupLine($"[yellow bold]{item.FullName}[/]");
                }
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine($"[red bold]Failed to delete " +
                                       $"{index} {item.FullName}[/]");
            }
        }

        SpectreConsoleHelpers.ExitPrompt();
    }

    private static async Task ProcessGlobbingFiles()
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

            Func<List<string>> files = await Globbing.GetAsync(matcherParameters);

            foreach (var file in files())
            {
                Console.WriteLine($"{Path.GetDirectoryName(file)?.Replace(folder, "").TrimStart('\\')}{Path.GetFileName(file)}");
            }
        }
        else
        {
            AnsiConsole.MarkupLine($"Folder [hotpink]{folder}[/] not found");
        }
    }
}
