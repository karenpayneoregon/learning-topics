using GlobbingApp1.Classes;
using GlobbingApp1.Models;
using Spectre.Console;

namespace GlobbingApp1;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        // patterns to include 
        string[] include = 
            [
                "**/Doc*.docx", 
                "**/Employee*.sql"
            ];

        // patterns to exclude
        string[] exclude = 
        [
            "**/Doc2*.docx",
            "**/DesktopStuff/**",                   // exclude DesktopStuff folder
            "**/SQL Server Management Studio/**",   // exclude SQL Server Management Studio folder
            "**/My Music/**",                       // exclude My Music folder
            "**/My Pictures/**",                    // exclude My Pictures folder
            "**/My Videos/**"                       // exclude My Videos folder
        ];
        
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        if (Directory.Exists(folder))
        {
            MatcherParameters matcherParameters = new()
            {
                Patterns = include,
                ExcludePatterns = exclude,
                ParentFolder = folder
            };

            Func<List<string>> files = await Globbing.GetAsync(matcherParameters).ConfigureAwait(false);

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
