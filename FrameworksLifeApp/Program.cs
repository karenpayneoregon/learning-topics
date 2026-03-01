using FrameworksLifeApp.Classes;
using FrameworksLifeApp.Classes.Core;
using FrameworksLifeApp.Interfaces;
using FrameworksLifeApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;

namespace FrameworksLifeApp;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                // Use manual registration to avoid dependency on AddHttpClient extension method
                services.AddSingleton<IDotNetReleaseMetadataService>(sp =>
                {
                    HttpClient client = new()
                    {
                        BaseAddress = new Uri("https://dotnetcli.blob.core.windows.net/dotnet/release-metadata/"),
                        Timeout = TimeSpan.FromSeconds(15)
                    };

                    return new DotNetReleaseMetadataService(client);
                });
            })
            .Build();

        var svc = host.Services.GetRequiredService<IDotNetReleaseMetadataService>();
        var channels = await svc.GetChannelsAsync();

        // Split
        static bool IsEol(string? phase) =>
            phase is not null && phase.Equals("eol", StringComparison.OrdinalIgnoreCase);

        var supported = channels.Where(c => !IsEol(c.SupportPhase)).ToList();
        var eol = channels.Where(c => IsEol(c.SupportPhase)).ToList();

        PrintSection("SUPPORTED (Active/Maintenance)", supported);
        Console.WriteLine();
        PrintSection("END OF LIFE (EOL)", eol);
        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    private static void PrintSection(string title, IReadOnlyList<DotNetChannelInfo> items)
    {
        // Title
        AnsiConsole.MarkupLine($"[bold]{Markup.Escape(title)}[/]");

        if (items.Count == 0)
        {
            AnsiConsole.MarkupLine("[grey](none)[/]");
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.HotPink)
            .Expand();

        table.AddColumn(new TableColumn("[IndianRed]Channel[/]"));
        table.AddColumn(new TableColumn("[IndianRed]Type[/]"));
        table.AddColumn(new TableColumn("[IndianRed]Phase[/]"));
        table.AddColumn(new TableColumn("[IndianRed]EOL[/]"));
        table.AddColumn(new TableColumn("[IndianRed]Latest SDK[/]"));
        table.AddColumn(new TableColumn("[IndianRed]Latest Runtime[/]"));
        table.AddColumn(new TableColumn("[IndianRed]Latest ASP.NET Core[/]"));

        foreach (var c in items.OrderByDescending(x => Version.Parse(x.ChannelVersion)))
        {
            var eol = c.EolDate?.ToString("yyyy-MM-dd") ?? "(unknown)";

            table.AddRow(
                c.ChannelVersion ?? "(unknown)",
                c.ReleaseType ?? "(?)",
                c.SupportPhase ?? "(unknown)",
                eol,
                c.LatestSdk ?? "(unknown)",
                c.LatestRuntime ?? "(unknown)",
                c.LatestAspNetCoreRuntime ?? "(n/a)"
            );
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine(); // spacing between sections
    }

}
