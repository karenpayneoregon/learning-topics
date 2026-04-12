namespace ProcessOrdersApp.Classes.Configuration;
#nullable disable
/// <summary>
/// Represents a singleton class that provides access to known database connection strings.
/// </summary>
/// <remarks>
/// This class is designed to centralize the management of database connection strings
/// used throughout the application. It ensures that connection strings are accessed
/// in a thread-safe manner and provides a single point of reference for database connectivity.
/// </remarks>
public sealed class DataConnections
{
    private static readonly Lazy<DataConnections> Lazy = new(() => new DataConnections());
    public static DataConnections Instance => Lazy.Value;

    /// <summary>
    /// Gets or sets the primary database connection string.
    /// </summary>
    /// <remarks>
    /// This property provides access to the main database connection string used throughout the application.
    /// It is intended to be set during application initialization and accessed in a thread-safe manner.
    /// </remarks>
    public string MainConnection { get; set; }
}
