namespace GetGlobalNuGetPackages.Models;

/// <summary>
/// Represents a NuGet package with its name and version information.
/// </summary>
public class Package
{
    public string Name { get; set; }
    public string Version { get; set; }
}