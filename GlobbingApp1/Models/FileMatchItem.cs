namespace GlobbingApp1.Models;

/// <summary>
/// Represents a file match item, encapsulating details about a file's folder, name, and full path.
/// </summary>
/// <remarks>
/// This class is designed to provide a structured representation of a file, including its folder path,
/// file name, and full path. It is particularly useful in scenarios involving file system operations
/// and pattern-based file matching.
/// </remarks>
public class FileMatchItem(string sender)
{
    public string? Folder { get; init; } = Path.GetDirectoryName(sender);
    public string FileName { get; init; } = Path.GetFileName(sender);
    public  string FullName => $"{Folder}\\{FileName}";
    public override string ToString() => $"{Folder}\\{FileName}";
}