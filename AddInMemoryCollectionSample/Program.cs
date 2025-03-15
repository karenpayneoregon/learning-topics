using AddInMemoryCollectionSample.Classes;
using Spectre.Console.Json;

namespace AddInMemoryCollectionSample
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Examples.Conventional();

            Examples.DryRun();

            Examples.DryRun2();

            Examples.DryRun3();

            Examples.ReadFromAppsettingsFile();

            AnsiConsole.MarkupLine("[yellow]Code to create appsettings.json[/]");
            var jsonString = AppSettingsCreator.CreateAppSettingsJson();

            // colorize json data
            var json = new JsonText(jsonString)
                .MemberColor(Color.Aqua)
                .BracketColor(Color.Green)
                .ColonColor(Color.Blue)
                .CommaColor(Color.Red)
                .StringColor(Color.Green)
                .NumberColor(Color.Blue)
                .BooleanColor(Color.Red)
                .StringColor(Color.White)
                .NullColor(Color.Green);

            AnsiConsole.Write(
                new Panel(json)
                    .Header("")
                    .Collapse()
                    .BorderColor(Color.White));

            Examples.Combination();

            
            Examples.CompanySettingsBasic();

            
            Examples.CompanySettings();

            Examples.DryRun5();

            SpectreConsoleHelpers.ExitPrompt();
        }
    }
}