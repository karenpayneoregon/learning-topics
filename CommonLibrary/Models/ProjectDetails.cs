#nullable disable

namespace CommonLibrary.Models;

/// <summary>
/// Represents the details of a project, including its name, company, copyright information, and version.
/// </summary>
public class ProjectDetails
{
    public string Copyright { get; set; }
    public string ProjectName { get; set; }
    public string Company { get; set; }
    public Version Version { get; set; } 
}
