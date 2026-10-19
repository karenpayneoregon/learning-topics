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


}
