namespace SolutionFrameworkScanner.Models;

/// <summary>
/// Represents a project grouped by its associated framework.
/// </summary>
/// <remarks>
/// This class encapsulates information about a project and its corresponding framework, 
/// providing functionality to compare and hash grouped projects.
/// </remarks>
public class GroupedProject(string framework, ProjectFrameworkInfo project)
{
    public string Framework { get; } = framework;
    public ProjectFrameworkInfo Project { get; } = project;

    public override bool Equals(object? value)
    {
        return value is GroupedProject other && EqualityComparer<string>.Default.Equals(other.Framework, Framework) && EqualityComparer<ProjectFrameworkInfo>.Default.Equals(other.Project, Project);
    }

    public override int GetHashCode()
    {
        var hash = 0x7a2f0b42;
        hash = -1521134295 * hash + EqualityComparer<string>.Default.GetHashCode(Framework);
        return -1521134295 * hash + EqualityComparer<ProjectFrameworkInfo>.Default.GetHashCode(Project);
    }
}