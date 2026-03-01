using FrameworksLifeApp.Classes;
using FrameworksLifeApp.Classes.Core;
using FrameworksLifeApp.Interfaces;
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
                    var client = new System.Net.Http.HttpClient
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

        // Print
        PrintSection("SUPPORTED (Active/Maintenance)", supported);
        Console.WriteLine();
        PrintSection("END OF LIFE (EOL)", eol);
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    static void PrintSection(string title, IReadOnlyList<DotNetChannelInfo> items)
    {
        Console.WriteLine(title);
        Console.WriteLine("Channel | Type | Phase        | EOL        | Latest SDK     | Latest Runtime  | Latest ASP.NET Core");
        Console.WriteLine(new string('-', 100));

        foreach (var c in items.OrderByDescending(x => Version.Parse(x.ChannelVersion)))
        {
            var eol = c.EolDate?.ToString("yyyy-MM-dd") ?? "(unknown)";

            Console.WriteLine(
                $"{c.ChannelVersion,-7} | " +
                $"{(c.ReleaseType ?? "(?)"),-4} | " +
                $"{(c.SupportPhase ?? "(unknown)"),-12} | " +
                $"{eol,-10} | " +
                $"{(c.LatestSdk ?? "(unknown)"),-13} | " +
                $"{(c.LatestRuntime ?? "(unknown)"),-13} | " +
                $"{(c.LatestAspNetCoreRuntime ?? "(n/a)")}"
            );
        }

        if (items.Count == 0)
            Console.WriteLine("(none)");
    }

}
