using System.Collections.Generic;

namespace Publisher.Models;

/// <summary>
/// Represents the configuration settings for the application.
/// </summary>
/// <remarks>
/// The <see cref="Configuration"/> class encapsulates various settings required for the application, 
/// such as the path to the NuGet executable, the location of packages, and a list of associated class projects.
/// This class is typically populated by deserializing configuration data from an external source, 
/// such as a JSON file.
/// </remarks>
public class Configuration
{
    public string PackageLocation { get; set; }

    public List<ClassProjects> ClassProjects { get; set; }
}