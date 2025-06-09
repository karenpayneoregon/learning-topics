using static System.Diagnostics.Debugger;

namespace PrintMembersSamples.Classes;

/// <summary>
/// Represents the application settings for the <c>PrintMembersSamples</c> namespace.
/// </summary>
/// <remarks>
/// This class is a singleton, ensuring that only one instance of <see cref="Appsettings"/> exists throughout the application.
/// It provides a centralized configuration point for controlling application behavior, such as whether to reveal sensitive information.
/// </remarks>
public sealed class Appsettings
{
    private static readonly Lazy<Appsettings> Lazy = new(() => new Appsettings());
    public static Appsettings Instance => Lazy.Value;
    /// <summary>
    /// Gets or sets a value indicating whether sensitive information should be revealed.
    /// </summary>
    /// <value>
    /// <c>true</c> if sensitive information should be revealed; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    /// When set to <c>true</c>, sensitive information such as usernames and passwords will be displayed in full.
    /// When set to <c>false</c>, sensitive information will be masked or redacted for security purposes.
    /// The default value is determined by whether a debugger is attached.
    /// </remarks>
    public bool RevealSensitiveInformation { get; set; }

    private Appsettings()
    {
        RevealSensitiveInformation = IsAttached;
    }
}