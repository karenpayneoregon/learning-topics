using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace TempApp.Classes;
internal static class Work
{
    public static void ReadConfiguration()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.karen.payne.json", optional: false, reloadOnChange: true)
            .Build();

        var helpDesk = config.GetSection(nameof(HelpDesk)).Get<HelpDesk>();

        Console.WriteLine($"Phone: {helpDesk.Phone}");
        Console.WriteLine($"Email: {helpDesk.Email}");
    }
}
