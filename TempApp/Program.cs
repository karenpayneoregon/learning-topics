using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;
using TempApp.Classes;
using TempApp.Json.Appsettings;

namespace TempApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        //var list = Lookups.BuildMonths();
        //Work.ReadConfiguration();

        //AnsiConsole.MarkupLine($"[green3_1]    Company[/] {Info.GetCompany()}");
        //AnsiConsole.MarkupLine($"[green3_1]    Product[/] {Info.GetProduct()}");
        //AnsiConsole.MarkupLine($"[green3_1]  Copyright[/] {Info.GetCopyright()}");
        //AnsiConsole.MarkupLine($"[green3_1]    Version[/] {Info.GetVersion()}");
        GetWinesUsingBogusTree();
        Console.ReadLine();
    }


    private static void GetWinesUsingBogus()
    {
        var redWines = new[]
        {
            "Cabernet Sauvignon", "Merlot", "Pinot Noir", "Zinfandel", "Syrah",
            "Malbec", "Tempranillo", "Barbera", "Sangiovese", "Grenache"
        };

        var whiteWines = new[]
        {
            "Chardonnay", "Sauvignon Blanc", "Riesling", "Pinot Grigio", "Gewürztraminer",
            "Viognier", "Chenin Blanc", "Albariño", "Moscato", "Grüner Veltliner"
        };

        var roseWines = new[]
        {
            "Provence Rosé", "White Zinfandel", "Tavel Rosé", "Rosé of Pinot Noir", "Rosé of Syrah",
            "Champagne", "Prosecco", "Cava", "Lambrusco", "Brut Rosé"
        };

        int wineIdCounter = 1;
        var wines = new List<Wine>();

        void AddWines(string[] names, WineType type)
        {
            wines.AddRange(names.Select(name => new Wine { WineId = wineIdCounter++, Name = name, WineType = type }));
        }

        AddWines(redWines, WineType.Red);
        AddWines(whiteWines, WineType.White);
        AddWines(roseWines, WineType.Rose);

        
        // Group by WineType and order each group by Name
        var groupedWines = wines
            .GroupBy(w => w.WineType)
            .OrderBy(g => g.Key); // Optional: orders WineType enums Red -> White -> Rose

        foreach (var group in groupedWines)
        {
            AnsiConsole.MarkupLine($"[cyan]{group.Key}[/]");

            foreach (var wine in group.OrderBy(w => w.Name))
            {
                Console.WriteLine($"  {wine.WineId,-5}{wine.Name}");
            }
        }
    }

    /// <summary>
    /// Generates a hierarchical tree structure of wines categorized by their types (Red, White, and Rosé)
    /// and displays it in the console using the Spectre.Console library.
    /// </summary>
    /// <remarks>
    /// The method creates a predefined list of wines for each type (Red, White, and Rosé),
    /// assigns unique IDs to each wine, and organizes them into a tree structure.
    /// The wines are grouped by their type and sorted alphabetically within each group.
    /// </remarks>
    private static void GetWinesUsingBogusTree()
    {
        var redWines = new[]
        {
            "Cabernet Sauvignon", "Merlot", "Pinot Noir", "Zinfandel", "Syrah",
            "Malbec", "Tempranillo", "Barbera", "Sangiovese", "Grenache"
        };

        var whiteWines = new[]
        {
            "Chardonnay", "Sauvignon Blanc", "Riesling", "Pinot Grigio", "Gewürztraminer",
            "Viognier", "Chenin Blanc", "Albariño", "Moscato", "Grüner Veltliner"
        };

        var roseWines = new[]
        {
            "Provence Rosé", "White Zinfandel", "Tavel Rosé", "Rosé of Pinot Noir", "Rosé of Syrah",
            "Champagne", "Prosecco", "Cava", "Lambrusco", "Brut Rosé"
        };

        int wineIdCounter = 1;
        var wines = new List<Wine>();

        void AddWines(string[] names, WineType type)
        {
            wines.AddRange(names.Select(name => new Wine { WineId = wineIdCounter++, Name = name, WineType = type }));
        }

        AddWines(redWines, WineType.Red);
        AddWines(whiteWines, WineType.White);
        AddWines(roseWines, WineType.Rose);

        var tree = new Tree("[deeppink3]Wine[/]")
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


    private static void GetAllWines()
    {
        var config = ConfigurationRoot();

        var connection = Appsetting.FromConfig(config).ConnectionStrings.MainConnection;
        IDbConnection db = new SqlConnection(connection);
        
        const string statement =
            """
            SELECT WineId
                ,Name
                ,WineType
            FROM dbo.Wine
            """;

        var wines = db.Query<Wine>(statement).AsList();





        foreach (var wine in wines)
        {
            
        }
    }

    private static IConfigurationRoot ConfigurationRoot()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        return config;
    }
}

public class Wine
{
    public int WineId { get; set; }
    public string Name { get; set; }
    public WineType WineType { get; set; }
}
public enum WineType
{
    Red = 1,
    White = 2,
    Rose = 3
}

