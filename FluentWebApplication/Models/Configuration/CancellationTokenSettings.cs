namespace FluentWebApplication.Models.Configuration;

/// <summary>
/// Represents the configuration settings for enabling or disabling the use of a cancellation token with a specified timeout.
/// </summary>
/// <remarks>
/// This class is designed to map configuration settings from the <c>appsettings.json</c> file under the 
/// <see cref="SectionName"/> section. It provides properties to control whether a cancellation token is used
/// and to specify the timeout duration.
/// </remarks>
public sealed class CancellationTokenSettings
{
    public const string SectionName = "CancellationTokenSettings";

    public bool Use { get; init; }
    public int Timeout { get; init; } // milliseconds
}

