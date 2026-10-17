using Spectre.Console;
using TreesExamples.Classes.Core;
using TreesExamples.Models;

namespace TreesExamples.Classes;

/// <summary>
/// Provides operations related to wine management, including generating and displaying
/// a hierarchical tree structure of wines categorized by their types.
/// </summary>
/// <remarks>
/// This class is designed to work with wine data, organizing it into a structured format
/// and utilizing the Spectre.Console library for visual representation in the console.
/// </remarks>
internal class WineOperations
{
    /// <summary>
    /// Generates a hierarchical tree structure of wines categorized by their types (Red, White, and Rosé)
    /// and displays it in the console using the Spectre.Console library.
    /// </summary>
    /// <remarks>
    /// The method creates a predefined list of wines for each type (Red, White, and Rosé),
    /// assigns unique IDs to each wine, and organizes them into a tree structure.
    /// The wines are grouped by their type and sorted alphabetically within each group.
    /// </remarks>
    public static void GetWinesTree()
    {


        SpectreConsoleHelpers.PrintPink();

        string[] redWines =
        [
            "Cabernet Sauvignon", "Merlot", "Pinot Noir", "Zinfandel", "Syrah",
            "Malbec", "Tempranillo", "Barbera", "Sangiovese", "Grenache"
        ];

        string[] whiteWines =
        [
            "Chardonnay", "Sauvignon Blanc", "Riesling", "Pinot Grigio", "Gewürztraminer",
            "Viognier", "Chenin Blanc", "Albariño", "Moscato", "Grüner Veltliner"
        ];

        string[] roseWines =
        [
            "Provence Rosé", "White Zinfandel", "Tavel Rosé", "Rosé of Pinot Noir", "Rosé of Syrah",
            "Champagne", "Prosecco", "Cava", "Lambrusco", "Brut Rosé"
        ];

        int wineIdCounter = 1;
        var wines = new List<Wine>();

        void AddWines(string[] names, WineType type)
        {
            wines.AddRange(names.Select(name => new Wine { WineId = wineIdCounter++, Name = name, WineType = type }));
        }

        AddWines(redWines, WineType.Red);
        AddWines(whiteWines, WineType.White);
        AddWines(roseWines, WineType.Rose);

        var tree = new Tree("[deeppink3]Wines[/]")
            .Style(Style.Parse("dim"));

        var types = tree.AddNode("[yellow]Types[/]");

        // Group by WineType and order each group by Name
        var groupedWines = wines
            .GroupBy(w => w.WineType)
            .OrderBy(g => g.Key); // Optional: orders WineType enums Red -> White -> Rose

        foreach (var group in groupedWines)
        {
            var groupNode = types.AddNode($"[deeppink3]{group.Key}[/]");

            foreach (var wine in group.OrderBy(w => w.Name))
            {
                groupNode.AddNode($"[DeepSkyBlue3]{wine.Name}[/]");
            }
        }

        AnsiConsole.Write(tree);
    }
}