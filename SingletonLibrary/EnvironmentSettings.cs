using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SingletonLibrary.Classes;
using SingletonLibrary.Models;

namespace SingletonLibrary;

#nullable disable

public sealed class EnvironmentSettings
{
    private static readonly Lazy<EnvironmentSettings> Lazy = new(() => new EnvironmentSettings());
    public static EnvironmentSettings Instance => Lazy.Value;

    public string MainConnection { get; set; }

    public AppEnvironment Environment { get; init; }

    public string DatabaseConnectionString { get; init; } = null!;
    
    public static IConfigurationRoot Configuration { get; private set; }

    private EnvironmentSettings()
    {
        Configuration = DataOperations.ConfigurationBuilder().Build();
        MainConnection = Configuration.GetConnectionString("MainConnection");

        DataConnections connections = AppSettingsReader.LoadConnectionStringsConnections();

        using IHost host = Host.CreateDefaultBuilder().Build();

        IHostEnvironment environment = host.Services.GetRequiredService<IHostEnvironment>();

        if (environment.CheckIsDevelopmentEnvironment())
        {
            Environment = AppEnvironment.Development;
            DatabaseConnectionString = connections.ConnectionStrings.DevelopmentConnection;
        }

        if (environment.CheckIsStagingEnvironment())
        {
            Environment = AppEnvironment.Staging;
            DatabaseConnectionString = connections.ConnectionStrings.StagingConnection;
        }

        if (environment.CheckIsProductionEnvironment())
        {
            Environment = AppEnvironment.Production;
            DatabaseConnectionString = connections.ConnectionStrings.ProductionConnection;
        }
    }
}