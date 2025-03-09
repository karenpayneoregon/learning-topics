using Microsoft.Extensions.Configuration;
using ConsoleConfigurationLibrary.Models;
using CustomIConfigurationSourceSample.Models;

namespace CustomIConfigurationSourceSample.Classes;

public sealed class AppConfiguration
{
    private static readonly Lazy<AppConfiguration> _lazyInstance = new(() => new AppConfiguration());
    public static AppConfiguration Instance { get; } = _lazyInstance.Value;

    /// <summary>
    /// Gets the main connection string used to connect to the database.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the main connection string retrieved from the configuration.
    /// </value>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the main connection string is missing in the configuration.
    /// </exception>
    public string MainConnection { get; }

    /// <summary>
    /// Gets the HelpDesk configuration details, including contact phone number and email address.
    /// </summary>
    /// <remarks>
    /// This property provides access to the HelpDesk configuration, which includes the phone number and email address.
    /// These values are retrieved from the application's configuration source and default to "Unknown" if not specified.
    /// </remarks>
    public HelpDesk HelpDesk { get; }


    /// <summary>
    /// Initializes a new instance of the <see cref="AppConfiguration"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor sets up the application's configuration by using a custom configuration source,
    /// <see cref="DatabaseConfigurationSource"/>, to retrieve configuration data. It ensures that
    /// essential configuration values, such as the main database connection string and help desk contact
    /// information, are properly loaded and initialized. Default values are assigned to the help desk
    /// information if they are not provided in the configuration.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the main database connection string is missing in the configuration.
    /// </exception>
    private AppConfiguration()
    {
        var builder = new ConfigurationBuilder();
        builder.Add(new DatabaseConfigurationSource());
        var configuration = builder.Build();

        var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();
        MainConnection = connectionStrings.MainConnection;

        string phone = configuration.GetValue<string>(nameof(HelpDesk.Phone));
        string email = configuration.GetValue<string>(nameof(HelpDesk.Email));

        HelpDesk = new HelpDesk
        {
            Phone = phone ?? "Unknown",
            Email = email ?? "Unknown"
        };
    }
}
