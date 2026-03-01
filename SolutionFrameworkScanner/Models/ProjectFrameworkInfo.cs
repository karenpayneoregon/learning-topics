namespace SolutionFrameworkScanner.Models;

/// <summary>
/// Represents information about a project and its associated target frameworks.
/// </summary>
/// <remarks>
/// This class provides details such as the project name, path, and the frameworks it targets. 
/// It also includes functionality to retrieve all associated frameworks, whether specified as a single target framework or multiple target frameworks.
/// </remarks>
public sealed class ProjectFrameworkInfo
{
    public string ProjectName { get; init; } = "";
    public string ProjectPath { get; init; } = "";
    /// <summary>
    /// Gets the primary target framework of the project.
    /// </summary>
    /// <remarks>
    /// This property represents the single target framework specified in the project file, if any.
    /// If the project targets multiple frameworks, this property will be <c>null</c>.
    /// Use <see cref="TargetFrameworks"/> or <see cref="AllFrameworks"/> for handling projects
    /// with multiple target frameworks.
    /// </remarks>
    public string? TargetFramework { get; init; }
    /// <summary>
    /// Gets the list of target frameworks specified for the project.
    /// </summary>
    /// <value>
    /// A read-only list of strings representing the target frameworks defined in the project file.
    /// </value>
    /// <remarks>
    /// This property contains multiple target frameworks if the project specifies them using the 
    /// <c>TargetFrameworks</c> element. If no target frameworks are explicitly defined, this property will be empty.
    /// </remarks>
    public IReadOnlyList<string> TargetFrameworks { get; init; } = [];

    public IReadOnlyList<string> AllFrameworks =>
        TargetFrameworks.Count > 0
            ? TargetFrameworks
            : string.IsNullOrWhiteSpace(TargetFramework) ? Array.Empty<string>() : new[] { TargetFramework! };
}