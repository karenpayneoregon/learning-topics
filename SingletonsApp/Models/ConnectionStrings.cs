namespace SingletonsApp.Models;

/// <summary>
/// Represents a collection of connection strings for different environments.
/// </summary>
/// <remarks>
/// This class is used to store and manage connection strings for production, 
/// development, and staging environments.
/// </remarks>
public class ConnectionStrings
{
    public required string ProductionConnection { get; set; }
    public required string DevelopmentConnection { get; set; }
    public required string StagingConnection { get; set; }
}