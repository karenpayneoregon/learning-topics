namespace GlobbingApp2.Classes;

public class FileMatchItem(string sender)
{
    public string? Folder { get; init; } = Path.GetDirectoryName(sender);
    public string FileName { get; init; } = Path.GetFileName(sender);
    public  string FullName => $"{Folder}\\{FileName}";
    public long Size { get; set; }
    /// <summary>
    /// Gets the size of the file in a human-readable format, such as "KB", "MB", or "GB".
    /// </summary>
    /// <value>
    /// A string representing the file size, formatted with the appropriate size suffix.
    /// </value>
    public string SizeInReadableFormat => Size.ToFileSize();

    public override string ToString() => $"{Folder}\\{FileName}";
}

