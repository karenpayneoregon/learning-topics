namespace SolutionFrameworkScanner.Models;

/// <summary>
/// Represents a collection of projects grouped by a specific framework.
/// </summary>
/// <remarks>
/// This class is used to organize and manage projects based on their target frameworks.
/// Each instance contains the framework name and a list of associated projects.
/// </remarks>
public class GroupedItems(string framework, List<GroupedItem> projects)
{
    public string Framework { get; } = framework;
    public List<GroupedItem> Projects { get; } = projects;

    public override bool Equals(object? value)
    {
        return value is GroupedItems other && EqualityComparer<string>.Default.Equals(other.Framework, Framework) && EqualityComparer<List<GroupedItem>>.Default.Equals(other.Projects, Projects);
    }

    public override int GetHashCode()
    {
        var hash = 0x7a2f0b42;
        hash = -1521134295 * hash + EqualityComparer<string>.Default.GetHashCode(Framework);
        return -1521134295 * hash + EqualityComparer<List<GroupedItem>>.Default.GetHashCode(Projects);
    }

    public override string ToString() => Framework;

}