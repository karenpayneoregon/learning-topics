namespace AddInMemoryCollectionSample.Models;

/// <summary>
/// Represents the configuration settings for a company, including details such as 
/// name, address, city, and state.
/// </summary>
/// <remarks>
/// This class is used to map configuration data from various sources, such as 
/// in-memory collections or JSON files, into strongly-typed properties.
/// </remarks>
public class CompanySettings
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}