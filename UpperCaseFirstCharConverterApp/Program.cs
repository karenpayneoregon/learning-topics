using System.Text.Json;
using UpperCaseFirstCharConverterApp.Classes;
using static UpperCaseFirstCharConverterApp.Classes.SpectreConsoleHelpers;

namespace UpperCaseFirstCharConverterApp;
internal partial class Program
{
    static void Main(string[] args)
    {
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
                "LastName": "lopez",
                "BirthDate": "1970-12-04"
              },
              {
                "Id": 3,
                "FirstName": "angel",
                "LastName": "perez",
                "BirthDate": "1980-09-11"
              }
            ]
            """;
        
        var people = JsonSerializer.Deserialize<List<Person>>(json, Options);
        var json1 = JsonSerializer.Serialize(people, Options);
        
        PresentJson(json1);
        ExitPrompt();
    }

    private static JsonSerializerOptions Options => new() { WriteIndented = true };

}
