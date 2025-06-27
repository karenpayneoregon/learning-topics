using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using TempApp.Json.Appsettings;
using Dapper;
using TempApp.Classes;

namespace TempApp;

internal partial class Program
{
    static void Main(string[] args)
    {
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

