namespace GlobbingApp1.Models;

public class FileMatchItem(string sender)
{
    public string? Folder { get; init; } = Path.GetDirectoryName(sender);
    public string FileName { get; init; } = Path.GetFileName(sender);
    public  string FullName => $"{Folder}\\{FileName}";
    public override string ToString() => $"{Folder}\\{FileName}";
}