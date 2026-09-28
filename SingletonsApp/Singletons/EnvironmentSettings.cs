using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SingletonsApp.Classes.Configuration;
using SingletonsApp.Models;

namespace SingletonsApp.Singletons;

/// <summary>
/// Represents a singleton class for managing environment settings specific to the application.
/// </summary>
/// <remarks>
/// This class is designed to handle environment-specific configurations, such as determining 
/// whether the application is running in a development, staging, or production environment. 
/// It ensures a single shared instance is used throughout the application, adhering to the singleton pattern.
/// </remarks>
public sealed class EnvironmentSettings
{
    private static readonly Lazy<EnvironmentSettings> Lazy = new(() => new EnvironmentSettings());
    public static EnvironmentSettings Instance => Lazy.Value;

    public AppEnvironment Environment { get; init; }
    
    public HelpDesk HelpDesk { get; init; }

    /// <summary>
    /// Sets the database connection string for the current environment.
    /// </summary>
    /// <value>
    /// A string representing the database connection string specific to the application's environment.
    /// </value>
    /// <remarks>
    /// This property is initialized based on the application's environment (Development, Staging, or Production).
    /// The value is determined during the singleton's initialization process and reflects the appropriate
    /// connection string for the active environment.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the property is set after the singleton instance has been initialized.
    /// </exception>
    public string DatabaseConnectionString { get; init; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnvironmentSettings"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor is private to enforce the singleton pattern. It initializes the environment settings
    /// by determining whether the application is running in a development or production environment.
    ///
    /// Note colors for Spectre.Console: [yellow bold]Development[/] and [green bold]Production[/]
    /// </remarks>
    private EnvironmentSettings()
    {
        
        DataConnections connections = AppSettingsReader.LoadConnectionStringsConnections();
        HelpDesk = AppSettingsReader.LoadHelpDesk();
        
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