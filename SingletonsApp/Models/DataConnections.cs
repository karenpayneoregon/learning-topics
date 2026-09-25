namespace SingletonsApp.Models;

/// <summary>
/// Represents the data connections configuration for the application.
/// </summary>
/// <remarks>
/// This class is used to encapsulate the connection strings required for different environments,
/// such as production, development, and staging. It serves as a model for deserializing
/// configuration data from external sources like JSON files.
/// </remarks>
public class DataConnections
{
    public required ConnectionStrings ConnectionStrings { get; set; }
}