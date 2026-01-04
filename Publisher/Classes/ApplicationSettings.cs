
using Publisher.Models;
using System;
using System.Collections.Generic;

namespace Publisher.Classes;

public class ApplicationSettings
{
    private static readonly Lazy<ApplicationSettings> Lazy = new(() => new ApplicationSettings());

    /// <summary>
    /// Access point to methods and properties
    /// </summary>
    public static ApplicationSettings Instance => Lazy.Value;

    /// <summary>
    /// Gets or sets the file path to the NuGet executable.
    /// </summary>
    /// <remarks>
    /// This property is populated from the <c>appsettings.json</c> configuration file.
    /// It is used to specify the location of the NuGet CLI tool required for package management operations.
    /// </remarks>
    public string NuGetExecutable { get; init; }
        
    /// <summary>
    /// Gets or sets the local directory path where NuGet packages are stored.
    /// </summary>
    /// <remarks>
    /// This property is populated from the <c>PackageLocation</c> field in the application's configuration file.
    /// </remarks>
    public string LocalPackagesLocation { get; init; }

    /// <summary>
    /// Gets or sets the list of class projects.
    /// </summary>
    /// <remarks>
    /// This property is populated from the "ClassProjects" section of the configuration file.
    /// Each item in the list represents a project with its associated path.
    /// </remarks>
    public List<ClassProjects> ClassProjectsList { get; init; }
        
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationSettings"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor is private to enforce the singleton pattern. It loads configuration
    /// settings from the <c>appsettings.json</c> file using the <see cref="ConfigurationLoader"/> class.
    /// The loaded settings are used to initialize the <see cref="NuGetExecutable"/>,
    /// <see cref="LocalPackagesLocation"/>, and <see cref="ClassProjectsList"/> properties.
    /// </remarks>
    private ApplicationSettings()
    {
            
        var settings = ConfigurationLoader.Load();
            
        NuGetExecutable = settings.NuGetExecutable;
        LocalPackagesLocation = settings.PackageLocation;
        ClassProjectsList = settings.ClassProjects;
    }
}