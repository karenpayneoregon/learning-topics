using GlobbingApp1.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GlobbingApp1.Classes;
/// <summary>
/// Provides methods for processing files using globbing patterns.
/// </summary>
/// <remarks>
/// This class contains functionality to handle file matching operations based on include and exclude patterns
/// defined in a configuration file. It utilizes globbing techniques to filter files within a specified folder
/// and outputs the results.
/// </remarks>
internal class GlobbingSamples
{

    /// <summary>
    /// Asynchronously processes duplicate files in the OneDrive folder and removes them.
    /// </summary>
    /// <remarks>
    /// This method retrieves a list of duplicate files from the user's OneDrive folder
    /// (specifically, the "My Documents" directory) using the <see cref="Globbing.GetDuplicatesTask"/> method.
    /// It attempts to delete each duplicate file found. If a file cannot be deleted, it logs the file's path
    /// to the console using Spectre.Console markup.
    /// </remarks>
    /// <returns>
    /// A task that represents the asynchronous operation of processing duplicate files.
    /// </returns>
    /// <exception cref="Exception">
    /// Thrown if an error occurs while attempting to delete a file.
    /// </exception>
    public static async Task ProcessOneDriveDuplicates()
    {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var list = await Globbing.GetDuplicatesTask(folder);

        if (list.Count >0)
        {
            AnsiConsole.MarkupLine($"[bold green]Found {list.Count} duplicate files.[/]");

        }else
        {
            AnsiConsole.MarkupLine($"[bold green]No duplicate files found in {folder}.[/]");
            return;
        }

        foreach (var (index, item) in list.Index())
        {
            try
            {
                if (File.Exists(item.FullName))
                {
                    File.Delete(item.FullName);
                }
                else
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


    }
    /// <summary>
    /// Processes files in a specified folder using globbing patterns defined in a configuration file.
    /// </summary>
    /// <remarks>
    /// This method reads globbing options from a JSON configuration file, retrieves files from a specified folder
    /// based on include and exclude patterns, and outputs the matched file paths to the console.
    /// If the folder does not exist, a message is displayed in the console.
    /// </remarks>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task ProcessGlobbingFiles()
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
