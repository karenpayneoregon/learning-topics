namespace SolutionFrameworkScanner.Models;

/// <summary>
/// Represents an item grouped by a specific framework, containing details about a project.
/// </summary>
/// <remarks>
/// This class is used to encapsulate project information, such as its name and path, 
/// and is typically utilized in scenarios where projects are grouped by frameworks.
/// </remarks>
public class GroupedItem(string projectName, string projectPath)
{
    public string ProjectName { get; } = projectName;
    public string ProjectPath { get; } = projectPath;

    public override bool Equals(object? value)
    {
        return value is GroupedItem other && EqualityComparer<string>.Default.Equals(other.ProjectName, ProjectName) && EqualityComparer<string>.Default.Equals(other.ProjectPath, ProjectPath);
    }

    public override int GetHashCode()
    {
        var hash = 0x7a2f0b42;
        hash = -1521134295 * hash + EqualityComparer<string>.Default.GetHashCode(ProjectName);
        return -1521134295 * hash + EqualityComparer<string>.Default.GetHashCode(ProjectPath);
    }

    public override string ToString() => ProjectName;

}