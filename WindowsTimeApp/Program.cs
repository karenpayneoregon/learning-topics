using Spectre.Console;
using Spectre.Console.Json;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using WindowsTimeApp.Classes;

namespace WindowsTimeApp;
internal partial class Program
{
    static async Task Main(string[] args)
    {

        RootCommand rootCommand = new("Windows up time");
        rootCommand.SetHandler(MainOperations.ShowTime);

        var commandLineBuilder = new CommandLineBuilder(rootCommand);

        commandLineBuilder.AddMiddleware(async (context, next) => { await next(context); });

        commandLineBuilder.UseDefaults();
        var parser = commandLineBuilder.Build();

        await parser.InvokeAsync(args);
    }

    private static void Old()
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
    }
}
