using FluentWebApplication.Models.Configuration;

namespace FluentWebApplication.Classes;

using Microsoft.Extensions.Configuration;

public sealed class AppConfiguration
{
    private static readonly Lazy<AppConfiguration> _instance = new(() => new AppConfiguration());

    public static AppConfiguration Instance => _instance.Value;

    /// <summary>
    /// Gets a value indicating to set a timeout in <see cref="EntityCoreWarmupService"/>.
    /// </summary>
    /// <value>
    /// true to set a timeout in <see cref="EntityCoreWarmupService"/>; otherwise, false.
    /// </value>
    /// <remarks>
    /// This property is initialized based on the configuration settings defined in the 
    /// <c>appsettings.json</c> file under the <see cref="CancellationTokenSettings.SectionName"/> section.
    /// </remarks>
    public bool Use { get; }

    /// <summary>
    /// Gets the timeout duration, in milliseconds, used for cancellation token operations for <see cref="EntityCoreWarmupService"/>..
    /// </summary>
    /// <value>
    /// An integer representing the timeout duration in milliseconds.
    /// </value>
    /// <remarks>
    /// This property retrieves its value from the <c>Timeout</c> field in the 
    /// <c>UseCancellationTokenTimed</c> section of the application configuration.
    /// </remarks>
    public int Timeout { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppConfiguration"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor is private to enforce the singleton pattern. It reads configuration settings
    /// from the "appsettings.json" file and initializes the <see cref="Enabled"/> and <see cref="Timeout"/> properties
    /// based on the <see cref="CancellationTokenSettings"/> section.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the configuration section <see cref="CancellationTokenSettings.SectionName"/> is missing or invalid.
    /// </exception>
    private AppConfiguration()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        var settings = configuration
                           .GetSection(CancellationTokenSettings.SectionName)
                           .Get<CancellationTokenSettings>();

        Use = settings!.Use;
        Timeout = settings.Timeout;
    }
}

