using Spectre.Console;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using VsWhereDataApp.Classes;

namespace VsWhereDataApp;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        
        try
        {
            RootCommand rootCommand = new("Visual Studio details");
            rootCommand.SetHandler(MainOperation.Display);

            var commandLineBuilder = new CommandLineBuilder(rootCommand);

            commandLineBuilder.AddMiddleware(async (context, next) => { await next(context); });

            commandLineBuilder.UseDefaults();
            var parser = commandLineBuilder.Build();

            await parser.InvokeAsync(args);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex, new ExceptionSettings
            {
                Format = ExceptionFormats.ShortenEverything | ExceptionFormats.ShowLinks,
                Style = new ExceptionStyle
                {
                    Exception = new Style().Foreground(Color.Grey),
                    Message = new Style().Foreground(Color.White),
                    NonEmphasized = new Style().Foreground(Color.Cornsilk1),
                    Parenthesis = new Style().Foreground(Color.Cornsilk1),
                    Method = new Style().Foreground(Color.Red),
                    ParameterName = new Style().Foreground(Color.Cornsilk1),
                    ParameterType = new Style().Foreground(Color.Red),
                    Path = new Style().Foreground(Color.Red),
                    LineNumber = new Style().Foreground(Color.Cornsilk1),
                }
            });
        }

    }
}
