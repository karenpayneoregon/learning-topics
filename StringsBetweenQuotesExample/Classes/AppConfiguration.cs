using Microsoft.Extensions.Configuration;
using StringsBetweenQuotesExample.Models;

namespace StringsBetweenQuotesExample.Classes;

public sealed class AppConfiguration
{
    private static readonly Lazy<AppConfiguration> Lazy = new(() => new AppConfiguration());
    public static AppConfiguration Instance => Lazy.Value;

    public string MainConnection { get; set; }
    public HelpDesk HelpDesk { get; set; }

    // set in project properties
    public string Server { get; set; }
    private static IConfigurationRoot Configuration { get;  set; }

    public T ReadSection<T>(string sectionName) => Configuration.GetSection(sectionName).Get<T>();

    private AppConfiguration()
    {
        Configuration = LoadConfiguration();

        MainConnection = Configuration.GetConnectionString("MainConnection");
        HelpDesk = Configuration.GetSection(nameof(HelpDesk)).Get<HelpDesk>();
        Server = Environment.GetEnvironmentVariable("Server");
    }

    private static IConfigurationRoot LoadConfiguration() =>
        new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables("Server")
            .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "HelpDesk:Phone", "555-555-1234" },
                    { "HelpDesk:Email", "ServiceDesk@SomeCompany.net" }
                })
            .Build();
}