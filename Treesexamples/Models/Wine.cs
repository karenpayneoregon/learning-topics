namespace TreesExamples.Models;

/// <summary>
/// Represents a wine with properties such as ID, name, and type.
/// </summary>
/// <remarks>
/// This class is part of the <c>TreesExamples.Models</c> namespace and is used to model wine data.
/// It includes properties for identifying the wine, its name, and its type.
/// </remarks>
public class Wine
{
    public int WineId { get; set; }
    public required string Name { get; set; }
    public WineType WineType { get; set; }
}
public enum WineType
{
    Red = 1,
    White = 3,
    Rose = 2
}