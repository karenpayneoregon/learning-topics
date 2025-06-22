namespace PromptFilesExamplesApp.Models;
/// <summary>
/// Represents the connection strings used by the application.
/// </summary>
/// <remarks>
/// This class is a model for storing the application's connection strings, such as the main and secondary database connections.
/// It is typically used in conjunction with configuration binding to populate its properties from a configuration source.
/// </remarks>
public class ConnectionStrings
{
    /// <summary>
    /// Gets or sets the main database connection string.
    /// </summary>
    /// <remarks>
    /// This property holds the connection string for the primary database used by the application.
    /// It is typically populated from the application's configuration during startup.
    /// </remarks>
    public string MainConnection { get; set; }
    /// <summary>
    /// Gets or sets the secondary database connection string.
    /// </summary>
    /// <remarks>
    /// This property holds the connection string for the secondary database used by the application.
    /// It is typically populated from the application's configuration during startup.
    /// </remarks>
    public string SecondaryConnection { get; set; }
}
