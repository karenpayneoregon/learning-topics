using Spectre.Console;
using System.Text.Json;
using UpperCaseFirstCharConverterApp.JsonConverters;
using UpperCaseFirstCharConverterApp.Models;

namespace UpperCaseFirstCharConverterApp.Classes;

internal class Samples
{
    public static void GlobalExample()
    {

        SpectreConsoleHelpers.PrintPink();

        string json =
            /*lang=json*/
            """
            [
              {
                "Id": 1,
                "FirstName": "jose",
                "LastName": "fernandez",
                "BirthDate": "1985-01-01"
              },
              {
                "Id": 2,
                "FirstName": "Miguel",
                "LastName": "loPez",
                "BirthDate": "1970-12-04"
              },
              {
                "Id": 3,
                "FirstName": "angel",
                "LastName": "PEREZ",
                "BirthDate": "1980-09-11"
              }
            ]
            """;

        AnsiConsole.MarkupLine("[bold yellow]Original JSON:[/]");
        SpectreConsoleHelpers.PresentJson(json);

        Console.WriteLine("\n");

        AnsiConsole.MarkupLine("[bold blue]Deserialized and Serialized JSON (with capitalized names):[/]");
        var people = JsonSerializer.Deserialize<List<Person>>(json, GlobalJsonOptions);
        var json1 = JsonSerializer.Serialize(people, GlobalJsonOptions);

        SpectreConsoleHelpers.PresentJson(json1);
    }

    private static readonly JsonSerializerOptions GlobalJsonOptions = new()
    {
        Converters = { new UpperCaseFirstCharConverter() },
        WriteIndented = true
    };

    /// <summary>
    /// Demonstrates the deserialization and serialization of a JSON string containing customer data,
    /// while applying transformations to capitalize specific properties.
    /// </summary>
    /// <remarks>
    /// This method processes a JSON string representing a list of customers, deserializes it into a list of 
    /// <see cref="Customer"/> objects, and then serializes it back to JSON.
    /// During this process, the <see cref="Customer.FirstName"/> and 
    /// <see cref="Customer.LastName"/> properties are transformed to have 
    /// their first characters capitalized using a custom JSON converter.
    /// </remarks>
    public static void PropertiesExample()
    {

        SpectreConsoleHelpers.PrintPink();

        string json =
            /*lang=json*/
            """
            [
              {
                "Id": 1,
                "FirstName": "jose",
                "LastName": "fernandez",
                "State": "CA",
                "BirthDate": "1985-01-01"
              },
              {
                "Id": 2,
                "FirstName": "Miguel",
                "LastName": "loPez",
                "State": "NY",
                "BirthDate": "1970-12-04"
              },
              {
                "Id": 3,
                "FirstName": "angel",
                "LastName": "PEREZ",
                "State": "TX",
                "BirthDate": "1980-09-11"
              }
            ]
            """;

        AnsiConsole.MarkupLine("[bold yellow]Original JSON:[/]");
        SpectreConsoleHelpers.PresentJson(json);

        Console.WriteLine("\n");

        AnsiConsole.MarkupLine("[bold blue]Deserialized and Serialized JSON (with capitalized names):[/]");
        var people = JsonSerializer.Deserialize<List<Customer>>(json, Options);
        var json1 = JsonSerializer.Serialize(people, Options);

        SpectreConsoleHelpers.PresentJson(json1);
    }

    private static JsonSerializerOptions Options => new() { WriteIndented = true };

}