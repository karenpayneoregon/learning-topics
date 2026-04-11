namespace ProcessOrdersApp.Classes.Configuration;

/// <summary>
/// Represents the configuration settings for file operations within the application.
/// This class provides a singleton instance to manage file-related settings, such as file names.
/// </summary>
public sealed class FileSettings
{
    private static readonly Lazy<FileSettings> Lazy = new(() => new FileSettings());
    public static FileSettings Instance => Lazy.Value;

    /// <summary>
    /// Gets or sets the name of the file used in file operations.
    /// </summary>
    /// <remarks>
    /// This property is utilized to specify or retrieve the file name for various operations 
    /// within the application, such as importing or exporting data.
    /// </remarks>
    public string? FileName { get; set; }
}