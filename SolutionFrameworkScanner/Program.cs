#define RUN1
using System.Diagnostics;
using ConsoleHelperLibrary.Classes;
using SolutionFrameworkScanner.Classes;
using SolutionFrameworkScanner.Classes.Core;
using SolutionFrameworkScanner.Models;
using Spectre.Console;

namespace SolutionFrameworkScanner;

internal partial class Program
{
    static void Main(string[] args)
    {
        // Hardcode or pass via command line
        var solutionPath = args.Length > 0 ? args[0] : "C:\\Solution.sln";

        if (!File.Exists(solutionPath))
        {
            AnsiConsole.MarkupLine("[red bold]Solution file not found. Press Enter to exit.[/]");
            return;
        }

        List<ProjectFrameworkInfo> projects = Scanner.ReadSolution(solutionPath);

        if (projects.Count == 0)
        {
            Console.WriteLine("No projects found.");
            return;
        }

        AnsiConsole.MarkupLine($"[cyan bold]Solution:[/] {Path.GetFileName(solutionPath)}");
        Console.WriteLine(new string('-', 60));

        foreach (ProjectFrameworkInfo project in projects.OrderBy(p => p.ProjectName))
        {
            var frameworks = project.AllFrameworks.Any()
                ? string.Join(", ", project.AllFrameworks)
                : "None found";

            AnsiConsole.MarkupLine($"[cyan bold]{project.ProjectName}[/]");
            Console.WriteLine($"  Path: {project.ProjectPath}");
            Console.WriteLine($"  Framework(s): {frameworks}");
            Console.WriteLine();
        }

        var net9Projects = projects
            .Where(p => p.AllFrameworks.Contains("net9.0", StringComparer.OrdinalIgnoreCase))
            .ToList();
        
        List<GroupedItems> groupedByFramework =
            projects
                .SelectMany(p => p.AllFrameworks.Select(f => new GroupedProject(f, p)))
                .GroupBy(x => x.Framework)
                .OrderBy(g => g.Key)
                .Select(g => new GroupedItems(g.Key, g
                    .OrderBy(x => x.Project.ProjectName)
                    .Select(x => new GroupedItem(x.Project.ProjectName, x.Project.ProjectPath))
                    .ToList()))
                .ToList();

        JsonOperations.ToJson(groupedByFramework);
        
        var report = JsonOperations.FromJson();
        
        FilterAndGroupFrameworks(projects, groupedByFramework);

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Filters and groups projects by their target frameworks.
    /// </summary>
    /// <param name="projects">
    /// A list of <see cref="ProjectFrameworkInfo"/> objects representing the projects to be filtered and grouped.
    /// </param>
    /// <param name="grouped">
    /// A list of <see cref="GroupedItems"/> representing projects already grouped by their frameworks.
    /// </param>
    /// <remarks>
    /// This method processes the provided projects to filter those targeting specific frameworks 
    /// (e.g., "net9.0") and organizes them into a dictionary grouped by framework names.
    /// </remarks>
    private static void FilterAndGroupFrameworks(List<ProjectFrameworkInfo> projects, List<GroupedItems> grouped)
    {
        
        SpectreConsoleHelpers.PrintPink();
        
        var net9 = projects.Where(x => x.TargetFramework == "net9.0").ToList();
        var all = grouped.Select(x => x.Framework).ToList();

        Dictionary<string, List<string>> groupedDictionary =
            projects
                .SelectMany(p => p.AllFrameworks.Select(f => new GroupedProject(f, p)))
                .GroupBy(x => x.Framework)
                .ToDictionary(
                    g => g.Key,
                    g => g
                        .OrderBy(x => x.Project.ProjectName)
                        .Select(x => x.Project.ProjectName)
                        .ToList()
                );
        
        foreach (var (key, _) in groupedDictionary.OrderBy(x => x.Key))
        {
            Console.WriteLine($"{key}");
        }
    }
}

