using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using TempApp.Json.Appsettings;
using Dapper;

namespace TempApp;

internal partial class Program
{
    static void Main(string[] args)
    {

        var config = ConfigurationRoot();

        string connection = Appsetting.FromConfig(config).ConnectionStrings.MainConnection;
        IDbConnection db = new SqlConnection(connection);


        const string statement =
            """
            SELECT WineId
                ,Name
                ,WineType
            FROM dbo.Wine
            """;

        var wines = db.Query<Wine>(statement).AsList();
        Console.ReadLine();
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

