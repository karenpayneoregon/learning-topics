using Spectre.Console;
using Spectre.Console.Json;
using WindowsTimeApp.Classes;

namespace WindowsTimeApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        var uptime = WindowsCode.GetSystemUptime();
        Console.WriteLine(uptime);
        Console.WriteLine();

        var json = new JsonText(WindowsCode.GetSystemUptimeAsJson())
            .BracesColor(Color.Red)
            .BracketColor(Color.Green)
            .ColonColor(Color.White)
            .CommaColor(Color.Cyan1)
            .StringColor(Color.GreenYellow)
            .NumberColor(Color.White)
            .BooleanColor(Color.Red)
            .MemberColor(Color.DeepPink1)
            .NullColor(Color.Green);

        AnsiConsole.Write(new Panel(json).Header("Up time")
                .Collapse()
                .BorderColor(Color.White));

        SpectreConsoleHelpers.ExitPrompt();
        
    }
}
